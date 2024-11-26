using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float health = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Player3D player = collision.gameObject.GetComponent<Player3D>();

        if (player != null)
        {
            //destroys pickup after collided
            Destroy(this.gameObject);

            //Gives player health
            player.TakeHealth(health);
        }
    }
}
