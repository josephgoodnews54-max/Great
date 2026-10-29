# Unity 3D Shooting Game — Complete Code


## `Assets/Scripts/PlayerController.cs`

```csharp
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpHeight = 1.5f;
    public float gravity = -19.62f;

    [Header("Mouse Look")]
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 85f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveDirection * speed + Vector3.up * velocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }
}
```

## `Assets/Scripts/Shooting.cs`

```csharp
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
```

## `Assets/Scripts/Bullet.cs`

```csharp
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();

        if (target != null)
            target.TakeDamage(damage);

        Destroy(gameObject);
    }
}
```

## `Assets/Scripts/Target.cs`

```csharp
using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 100;
    public int scoreValue = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
```

## `Assets/Scripts/GameManager.cs`

```csharp
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText;
    private int score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
```
