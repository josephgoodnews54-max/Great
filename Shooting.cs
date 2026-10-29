using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject bulletPrefab;
    public Transform muzzle;
    public float bulletSpeed = 35f;
    public float fireRate = 0.2f;
    public int magazineSize = 30;
    public float reloadTime = 1.5f;

    private float nextFireTime;
    private int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        currentAmmo = magazineSize;
    }

    private void Update()
    {
        if (isReloading)
            return;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
            StartCoroutine(Reload());
    }

    private void Shoot()
    {
        currentAmmo--;

        GameObject bullet = Instantiate(
            bulletPrefab,
            muzzle.position,
            Quaternion.LookRotation(muzzle.forward)
        );

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = muzzle.forward * bulletSpeed;

        Destroy(bullet, 3f);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}
