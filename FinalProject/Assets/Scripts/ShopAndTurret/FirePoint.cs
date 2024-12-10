using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // ������ ����
    public Transform firePoint;      // ����� ��������
    public float bulletSpeed = 10f;  // �������� ����
    public float fireRate = 1f;      // ������� �������� (��������� � �������)

    private float fireCooldown = 0f; // ����� �� ���������� ��������

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        // �������������� �������� (����� �������� �� Input.GetKeyDown ��� ������ ��������)
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // ����� �������
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // �������� ����
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // �������� ���� ��������
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}