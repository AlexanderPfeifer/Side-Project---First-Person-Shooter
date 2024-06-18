using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private TextMeshProUGUI ammoText;
    private int currentAmmo;
    [SerializeField] private int maxAmmo = 16;
    private bool reload;
    private float currentReloadTime;
    [SerializeField] private float maxReloadTime = 2;

    [Header("Enemy")]
    [SerializeField] private LayerMask enemyLayer;
    private HealthSystem enemyHealthSystem;

    [Header("Weapon")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private int weaponDamage;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentReloadTime = 0;
    }

    private void Update()
    {
        //When no ammo is left, then reloads for some seconds
        if (reload)
        {
            currentReloadTime += Time.deltaTime;
            
            if (currentReloadTime >= maxReloadTime)
            {
                currentAmmo = maxAmmo;
                ammoText.text = new string(currentAmmo + "/" + maxAmmo);
                currentReloadTime = 0;
                reload = false;
            }
        }
        
        //Checks if raycast hits enemy
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out var enemyHit, float.MaxValue, enemyLayer))
        {
            if (enemyHit.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                enemyHealthSystem = healthSystem;
            }
        }
        else
        {
            enemyHealthSystem = null;
        }
    }

    //substracts ammo, sets the ui accordingly and lets enemy take damage when left click is pressed
    void OnShoot(InputValue inputValue)
    {
        if (Time.timeScale <= 0 || reload)
            return;
        
        currentAmmo--;
        
        ammoText.text = new string(currentAmmo + "/" + maxAmmo);

        if (currentAmmo <= 0)
        {
            reload = true;
        }

        if (enemyHealthSystem != null)
        {
            enemyHealthSystem.TakeDamage(weaponDamage);
            enemyHealthSystem.transform.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
