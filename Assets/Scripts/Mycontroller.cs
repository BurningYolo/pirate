using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mycontroller : MonoBehaviour
{
    public BoatCollision boatcollision ; // Reference to the inactive GameObject with the script

    private void Start()
    {
        // Get the script component from the inactive GameObject
        boatcollision.DeActivateAllParticles(); 
    }
}
