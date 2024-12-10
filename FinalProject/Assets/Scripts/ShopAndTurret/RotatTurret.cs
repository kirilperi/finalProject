using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform turretHead; // Ссылка на вращающуюся часть турели
    public Transform firePoint; // Точка, из которой выстрелы появляются
    public GameObject bulletPrefab; // Префаб пули
    public float rotationSpeed = 5f; // Скорость поворота турели
    public float detectionRange = 10f; // Радиус обнаружения врагов
    public float fireRate = 1f; // Скорость стрельбы (выстрелов в секунду)

    private Transform target; // Текущая цель
    private float fireCooldown = 0f; // Время до следующего выстрела

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            RotateTowardsTarget();
            ShootAtTarget();
        }

        // Уменьшаем кулдаун
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
        // Проверяем, готова ли турель стрелять
        if (fireCooldown <= 0f)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Создаем пулю
            fireCooldown = 1f / fireRate; // Сбрасываем кулдаун
        }
    }
}
