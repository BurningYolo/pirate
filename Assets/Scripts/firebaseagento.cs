using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firebaseagento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            FirebaseInitialize.instance.LogEvent1("Game_is_started");
            Debug.Log("Firebase connected in Start");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
