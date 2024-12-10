using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Префаб пули
    public Transform firePoint;      // Точка выстрела
    public float bulletSpeed = 10f;  // Скорость пули
    public float fireRate = 1f;      // Частота стрельбы (выстрелов в секунду)

    private float fireCooldown = 0f; // Время до следующего выстрела

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Автоматическая стрельба (можно заменить на Input.GetKeyDown для ручной стрельбы)
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // Сброс таймера
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Создание пули
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Придание пули скорости
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}