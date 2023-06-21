using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCollider : MonoBehaviour
{
    public GameObject[] boats;
    public GameObject[] particles;
    public GameObject[] bulletcreator; 
    
    // Start is called before the first frame update
    void Start()
       
    {
        particles[1].SetActive(false);
        particles[0].SetActive(false);

        particles[2].SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bot1"))
        {
            
            Debug.Log("something");
            // Disable the collided game object
            boats[0].SetActive(false);
            particles[0].SetActive(true);
            bulletcreator[0].SetActive(false); 
            // Activate the particle effect at the collision position


        }

        if (other.CompareTag("bot2"))
        {
            Debug.Log("something");
           
            // Disable the collided game object
            boats[1].SetActive(false);
            particles[1].SetActive(true);
            bulletcreator[1].SetActive(false);
            // Activate the particle effect at the collision position


        }

        if (other.CompareTag("bot3"))
        {
            Debug.Log("something");
            // Disable the collided game object


            // Activate the particle effect at the collision position
            

            boats[2].SetActive(false);
            particles[2].SetActive(true);
            bulletcreator[2].SetActive(false);

        }
    }
}
