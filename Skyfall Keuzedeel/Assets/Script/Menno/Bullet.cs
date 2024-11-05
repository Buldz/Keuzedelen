using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    private Vector3 movementDir;
    

    // Start is called before the first frame update
    void Start()
    {
        movementDir = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDir * speed * Time.deltaTime;

        
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        // Enemy takes damage //
            Enemy3D enemy = collision.transform.GetComponent<Enemy3D>();
            if (enemy != null)
            {
               enemy.TakeDamage(damage);
            }

            Player3D player3D = collision.transform.GetComponent<Player3D>();
            if (player3D != null)
            {
                player3D.TakeDamage(damage);
            }
    }
}
