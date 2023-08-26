using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public int score;
    public string[] insultTexts1;
    public string[] insultTexts2;
    public string[] insultTexts3;
    public string[] insultTexts4;
    public GameObject shopMenu;
    public GameObject player;
    public GameObject menuCam;
    bool isShopping;
    float timeScaleOld;
    public GameObject buyGun2Button;
    public GameObject buyGun3Button;
    public PostProcessVolume enemyDiePostFX;

    public Transform[] spawnPoints; // every position where our enemies can spawn
    public GameObject[] enemies; // the enemy prefabs go here
    public GameObject[] weapons; // the player's weapons go here
    public Image[] crosshairs; // all the crosshairs from the ui go here

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 3); // Spawn enemies every 3 seconds

        /*for (int i = 0; i < weapons.Length; i++)    // for (int i = 1; i < weapons.Length; i++)
        {                                           // {
            weapons[i].SetActive(false);            //      weapons[i].SetActive(false);
        }                                           // }
        weapons[0].SetActive(true);                 //

        for (int i = 0; i < crosshairs.Length; i++)
        {
            crosshairs[i].enabled = false;
        }*/

        SwitchWeapon(0);

        AddScore(0);
    }

    void SpawnEnemy()
    {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, enemies.Length);

        var id = Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        //id.GetComponent<AICharacterControl>().target = FindObjectOfType<FirstPersonController>().gameObject.GetComponent<Transform>(); // potrebno samo za AICharacterControl enemy od standard assetsa
        //id.transform.localScale *= 0.5f;
    }

    void SwitchWeapon(int indexActive)
    {
        if (weapons[indexActive].GetComponent<GunController>().isBought)
        {
            for (int i = 0; i < crosshairs.Length; i++)
            {
                crosshairs[i].enabled = false;
            }
            weapons[indexActive].GetComponent<GunController>().EnableCrosshair();
            weapons[indexActive].GetComponent<GunController>().UpdateAmmoText();

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[indexActive].SetActive(true);
        }
        else
        {
            Debug.Log("You need to buy this weapon to use it. Open the shop with 'I'.");
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score + "$";

        /*string insult;
        if (score == 0)
        {
            insult = insultTexts1[Random.Range(0, insultTexts1.Length)];
        }
        else if (score < 100)
        {
            insult = insultTexts2[Random.Range(0, insultTexts1.Length)];
        }
        else if (score < 1000)
        {
            insult = insultTexts3[Random.Range(0, insultTexts1.Length)];
        }
        else
        {
            insult = insultTexts4[Random.Range(0, insultTexts1.Length)];
        }*/
        endScoreText.text = "You got " + score + " this time!\n"/* + insult*/;
    }

    public void CloseShopMenu()
    {
        isShopping = false;
        shopMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = timeScaleOld;
        player.SetActive(true);
        menuCam.SetActive(false);
    }
    public void BuyWeapon(int weaponIndex, int price, GameObject buyGunButton)
    {
        if (score >= price)
        {
            weapons[weaponIndex].GetComponent<GunController>().isBought = true;
            AddScore(-price);
            buyGunButton.GetComponent<Button>().interactable = false;
            buyGunButton.GetComponentInChildren<TextMeshProUGUI>().text = "Consoomed";
            buyGunButton.GetComponent<AudioSource>().Play();
        }
    }

    public void BuyGun2()
    {
        BuyWeapon(1, 50, buyGun2Button);
    }

    public void BuyGun3()
    {
        BuyWeapon(2, 100, buyGun3Button);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SwitchWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            SwitchWeapon(4);
        }

        if (Input.GetButtonDown("Shop"))
        {
            if (!isShopping)
            {
                isShopping = true;
                shopMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                timeScaleOld = Time.timeScale;
                Time.timeScale = 0f;
                player.SetActive(false);
                menuCam.SetActive(true);
                Transform playerCamTransform = player.GetComponent<FirstPersonController>().transform;
                menuCam.transform.position = playerCamTransform.position;
                menuCam.transform.rotation = playerCamTransform.rotation;
                menuCam.transform.localScale = playerCamTransform.localScale;
            }
            else
            {
                CloseShopMenu();
            }
            
        }
    }
}
