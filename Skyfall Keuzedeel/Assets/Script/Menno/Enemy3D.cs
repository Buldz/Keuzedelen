/* CURRENT BUGS
    - Enemy isnt hittable sometimes, don't know why yet
    |- Because of hasBeenHit bool??? - volgensmij niet
 */

using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Enemy3D : MonoBehaviour
{
    [Header("Basic settings")]
    public float currentHealth;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private Weapon weapon;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Advanced settings")]
    [SerializeField] private float detetctRadius = 10.0f;
    [SerializeField] private float attackRadius = 10.0f;
    [SerializeField] private float visionInRadians = 2.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private Transform playerRef;
    [SerializeField] private NavMeshAgent navMeshAgent;

    // Private Variables //
    private float degreesToRandians;
    private Vector3 spawnLocation;
    public bool hasBeenHit = false;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnLocation = transform.position;
        // Find weapon as the enemy children //
        weapon = GetComponentInChildren<Weapon>();

        // Find player reference //
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;

        // Find NavMeshAgent component //
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Reload();
        DetectRange();
        AttackRange();

        if (hasBeenHit == true) Chase();
    }

    private void Shoot()
    {
        if (weapon.currentAmmo > 0 && Time.time >= weapon.nextTimeToFire)
        {
            weapon.nextTimeToFire = Time.time + 1f / weapon.fireRate;
            weapon.Shoot();
        }
    }

    private void Reload()
    {
        // Checks if player is reloading //
        if (weapon.isReloading)
        {
            return;
        }

        // Player reload //
        if (weapon.currentAmmo == 0)
        {
            StartCoroutine(weapon.Reload());
            return;
        }
    }

    public void TakeDamage(float amount)
    {
        hasBeenHit = true;
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Turns off the enemy //
        this.gameObject.SetActive(false);
    }

    private void DetectRange()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detetctRadius, targetMask);

        if (rangeChecks.Length == 0) return;

        Transform target = rangeChecks[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the radius
        if (distanceToTarget > detetctRadius)
        {
            return; // Exit if the player is outside the detection radius
        }

        degreesToRandians = Vector3.Angle(transform.forward, directionToTarget) * Mathf.Deg2Rad;
        if (degreesToRandians < visionInRadians)
        {
            // Perform a raycast to check for obstructions
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                //Debug.Log("Player detected!");
                Chase();
            }
            else
            {
                hasBeenHit = false;
                navMeshAgent.destination = spawnLocation;
            } 
        }
    }

    private void AttackRange()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, attackRadius, targetMask);

        if (rangeChecks.Length == 0) return;

        Transform target = rangeChecks[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Check if the player is within the radius
        if (distanceToTarget > attackRadius)
        {
            return; // Exit if the player is outside the detection radius
        }

        degreesToRandians = Vector3.Angle(transform.forward, directionToTarget) * Mathf.Deg2Rad;
        if (degreesToRandians < visionInRadians)
        {
            // Perform a raycast to check for obstructions
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                //Debug.Log("Player detected!");
                Shoot();
            }
        }
    }

    private void Chase()
    {
        // Enemy rotation //
        var targetRotation = Quaternion.LookRotation(playerRef.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Enemy moves to player ref position //
        navMeshAgent.destination = playerRef.position;
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere at the enemy's position with the specified detect radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detetctRadius);

        // Draw a sphere at the enemy's position with the specified attack radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        // Draw a line representing the line of sight
        if (playerRef != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }
}