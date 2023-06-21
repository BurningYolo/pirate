using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealSplash : MonoBehaviour
{
    public string sceneName; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashtoMain(3f));

    }

    private System.Collections.IEnumerator SplashtoMain(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
