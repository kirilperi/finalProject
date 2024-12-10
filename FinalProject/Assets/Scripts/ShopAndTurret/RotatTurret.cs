using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform turretHead; // ������ �� ����������� ����� ������
    public Transform firePoint; // �����, �� ������� �������� ����������
    public GameObject bulletPrefab; // ������ ����
    public float rotationSpeed = 5f; // �������� �������� ������
    public float detectionRange = 10f; // ������ ����������� ������
    public float fireRate = 1f; // �������� �������� (��������� � �������)

    private Transform target; // ������� ����
    private float fireCooldown = 0f; // ����� �� ���������� ��������

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            RotateTowardsTarget();
            ShootAtTarget();
        }

        // ��������� �������
        fireCooldown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance && distanceToEnemy <= detectionRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - turretHead.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void ShootAtTarget()
    {
        // ���������, ������ �� ������ ��������
        if (fireCooldown <= 0f)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // ������� ����
            fireCooldown = 1f / fireRate; // ���������� �������
        }
    }
}
