using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Camera mainCam;

    [SerializeField] private int weaponDamage;
    private int currentAmmo;
    [SerializeField] private int maxAmmo = 16;
    private bool reload;
    private float currentReloadTime;
    [SerializeField] private float maxReloadTime = 2;
    private HealthSystem enemyHealthSystem;

    private void Start()
    {
        currentAmmo = maxAmmo;
        currentReloadTime = 0;
    }

    private void Update()
    {
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
