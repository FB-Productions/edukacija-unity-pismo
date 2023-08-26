using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    [Header("Gun Stats")]
    public bool isBought;
    public bool canHoldButton;
    bool shooting;
    public float fireRate = 0.1f;
    public float fireRateMax = 0.1f;
    public float damage = 10f;
    public float speed = 20f;
    public int ammo = 20;
    int ammoMax;
    public float reloadTime = 1f;
    public float reloadTimer;
    public Image crosshair;
    public TextMeshProUGUI ammoText;
    public float aimBloom;
    [Header("Shoot checks")]
    bool canShoot = true;
    bool hasToReload;
    bool isReloading;
    [Header("Cameras")]
    public Camera mainCam;
    public Camera aimCam;

    private void Awake()
    {
        ammoMax = ammo;
    }

    private void Start()
    {
        reloadTimer = reloadTime;

        EnableCrosshair();

        UpdateAmmoText();
    }

    private void Update()
    {
        if (canHoldButton)
        {
            shooting = Input.GetButton("Fire1");
        }
        else
        {
            shooting = Input.GetButtonDown("Fire1");
        }
        
        if (canShoot && shooting && !hasToReload)
        {
            ShootBullet();
        }
        else if (hasToReload && shooting)
        {
            isReloading = true;
        }

        if (Input.GetButton("Fire2"))
        {
            mainCam.enabled = false;
            aimCam.enabled = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            mainCam.enabled = true;
            aimCam.enabled = false;
        }

        if (Input.GetButtonDown("Reload"))
        {
            isReloading = true;
        }

        if (isReloading)
        {
            canShoot = false;

            reloadTimer -= Time.deltaTime;

            ammoText.text = "Reload";

            if (reloadTimer <= 0f)
            {
                ReloadGun();
                reloadTimer = reloadTime;
            }
        }

        if (fireRate <= 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        fireRate -= Time.deltaTime;
    }

    void ShootBullet()
    {
        float x = Screen.width / 2;
        float y = Screen.height / 2;

        float xAcc = Random.Range(x - aimBloom, x + aimBloom);
        float yAcc = Random.Range(y - aimBloom, y + aimBloom);

        var ray = Camera.main.ScreenPointToRay(new Vector3(xAcc, yAcc, 0)); // iz kamere cilja na tocku na ekranu (u nasem slucaju u centar ekrana gdje pucamo), ali sa malo "blooma" da bude manje precizno (ala Halo Infinite)
        
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>(); // interesira nas velocity metka
        bulletRB.velocity = speed * ray.direction;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = damage;
        }

        ammo--;

        if (ammo <= 0)
        {
            canShoot = false;
            hasToReload = true;
        }

        fireRate = fireRateMax;

        UpdateAmmoText();
    }

    void ReloadGun()
    {
        ammo = ammoMax;
        canShoot = true;
        isReloading = false;
        hasToReload = false;

        UpdateAmmoText();
    }

    public void BulletHitEnemy(EnemyAI enemy)
    {
        if (enemy != null)
        {
            enemy.ReceiveDamage(damage);
        }
    }

    public void EnableCrosshair()
    {
        crosshair.enabled = true;
    }

    public void UpdateAmmoText()
    {
        ammoText.text = ammo + "/" + ammoMax;
    }
}
