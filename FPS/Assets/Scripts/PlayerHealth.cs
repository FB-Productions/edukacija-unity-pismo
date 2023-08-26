using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject mainMenuCamera;
    public GameObject deathScreen;
    public PostProcessVolume hurtPostFX;
    GameObject fpsController;

    private void Start()
    {
        currentHealth = maxHealth;
        fpsController = gameObject;
        healthBar.value = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        healthBar.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        mainMenuCamera.SetActive(true);
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        fpsController.SetActive(false);
        Debug.Log("Death");
    }

    private void Update()
    {
        float hpPercent = Mathf.Clamp01(currentHealth / maxHealth); // ogranicimo vrijednost od 0 do 1 za postotak healtha koji imamo
        float weight = Mathf.Lerp(1, 0, hpPercent); // efektivno obrce trenutnu vrijednost hp-a s npr. 0 na 1, 0.25 na 0.75 i sl. zbog nasih namjera za postprocessing efekt (kao hpPercent = 1 - hpPercent;)

        hurtPostFX.weight = weight;
        
        //Debug.Log(currentHealth);
    }
}
