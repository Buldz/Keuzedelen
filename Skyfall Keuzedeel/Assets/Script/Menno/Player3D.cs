using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour
{
    [Header("Attributes")]
    public float health = 100.0f;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Other")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private CharacterController controller;

    //Private Variables
    private Vector3 velocity;
    private bool isGrounded;
    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        Move();
        Reload();
        if (weapon.currentAmmo > 0)
        {
            Shoot();
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
        if (Input.GetKeyDown("r"))
        {
            StartCoroutine(weapon.Reload());
            return;
        }
    }

    private void Shoot()
    {
        // Player shoot //
        if (Input.GetButton("Fire1") && Time.time >= weapon.nextTimeToFire)
        {
            weapon.nextTimeToFire = Time.time + 1f / weapon.fireRate;
            weapon.Shoot();

            //Debug.Log(currentAmmo);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
