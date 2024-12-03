//CHANGE - Bullet damage is different than weapon damage
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float damage = 5.0f;
    [SerializeField] private float lifeTime = 30.0f;
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

    void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);

        // Enemy takes damage //
            Enemy3D enemy = collider.transform.GetComponent<Enemy3D>();
            if (enemy != null)
            {
               enemy.TakeDamage(damage);
            }

            Player3D player3D =  collider.transform.GetComponent<Player3D>();
            if (player3D != null)
            {
                player3D.TakeDamage(damage);
            }
    }
}
