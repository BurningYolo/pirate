using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 10f;
    private float currentHealth;
    public RestartButton restartbutton;
    public SpinAnimationy spinanimation;
    public GameObject particle_before_exploding;
    public GameObject player;
    public GameObject particle_explosion;
    public CanvasManager canvasmanager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        healthSlider.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            spinanimation.enabled = true;
            Vector3 particleOffset = new Vector3(0f, 2f, 0f);
            GameObject particle1 = Instantiate(particle_before_exploding, player.transform.position + particleOffset, Quaternion.identity);
            particle1.transform.parent = player.transform;

            Invoke("DestroyBoat", 2f);
        }
    }

    void DestroyBoat()
    {
        player.SetActive(false);
        GameObject particle2 = Instantiate(particle_explosion, player.transform.position, Quaternion.identity);
        Invoke("Restarto", 2f);
    }

    void Restarto()
    {

        canvasmanager.lost = true;
        

    }
}
