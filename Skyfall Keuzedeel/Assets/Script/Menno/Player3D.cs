using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3D : MonoBehaviour
{
    [Header("Attributes")]
    public float currentHealth;
    [SerializeField] private float maxHealth = 100.0f;

    [Header("Movement")]
    public float speed = 12.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;

    [Header("Other")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private CharacterController controller;

    public Text healthText;

    //Private Variables
    private Vector3 velocity;
    private bool isGrounded;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        weapon = GetComponentInChildren<Weapon>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth.ToString();
        isGrounded = controller.isGrounded;

        Die();

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
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public bool GiveHealth(float amount)
    {
        if (currentHealth >= maxHealth) return false;

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            float healthToGive = Mathf.Min(amount, maxHealth - currentHealth);
            currentHealth += healthToGive;
        }

        return true;
    }

    private void Die()
    {
        if (currentHealth <= 0f)
        {
            Destroy(this.gameObject);
        }
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
