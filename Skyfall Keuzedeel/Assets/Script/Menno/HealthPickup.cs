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

    void OnTriggerEnter(Collider collider)
    {
        Player3D player = collider.gameObject.GetComponent<Player3D>();
         
        if (player != null && player.GiveHealth(health))
        {
            //destroys pickup after collided
            Destroy(this.gameObject);
        }
    }
}
