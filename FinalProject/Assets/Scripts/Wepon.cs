using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public enum WeaponType
{
    Revolver,
    shotgun
}
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject muzzleEffects;
    [SerializeField] private Transform barrel;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private float range = 50f;
    [SerializeField] private float fireDelay=0.25f;
    [SerializeField] private float shotPower=0.3f;
    [SerializeField] private float shakeDuration = 0.1f;
    public static List<Weapon> WeaponList = new();
    public WeaponType WeaponType => weaponType;
    private Animator _revolverController;
    private Transform _cam;
    private AudioSource _audioSource;
    private Vector3 holeSizeDefualt;
    private CameraShake shaker;
    [SerializeField] private TextMeshProUGUI ammoInfo;
    private const int magazineSize=7;
    public int remainingAmmo
    {
        get;
        set;
    }
    private int currentAmmo = 7;


    private void Start()
    {
        _cam = Camera.main.transform;
        _audioSource = GetComponent<AudioSource>();
        _revolverController = GetComponent<Animator>();
        shaker = GetComponent<CameraShake>();
        _revolverController.speed *= 3;
        holeSizeDefualt = bulletHole.transform.localScale;
        remainingAmmo = 21;
        WeaponList.Add(this);
        UpdateAmmoInfo();

    }

    private void Update()
    {
        

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (Physics.Raycast(_cam.position, _cam.TransformDirection(Vector3.forward), out var hit, range))
        {

            StartCoroutine(Fire(hit));
            
        }
    }
    public void UpdateAmmoInfo()
    {
        ammoInfo.text = $"Ammo {currentAmmo}/{remainingAmmo}";
    }
    private IEnumerator Fire(RaycastHit hit)
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoInfo();


        }
        else if (remainingAmmo>0)
        {
            Reload();
            yield break;

        }else
        {
            yield break;
        }
        _revolverController.SetTrigger("Shot1");
        yield return new WaitForSeconds(fireDelay);
        shaker.Shake(shotPower, shakeDuration);
        var scaleFactor = Random.Range(0.8f, 1.5f);
            if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
            {
                EnemyShot(hit.collider);
            }
            else
            {
            var bulletHoleRotattion = Quaternion.LookRotation(hit.normal) * Quaternion.Euler(0, 0, Random.Range(0, 360));
               var bulletHoleInstance= Instantiate(bulletHole, hit.point + (hit.normal * 0.001f), bulletHoleRotattion);
            bulletHoleInstance.transform.localScale = holeSizeDefualt * scaleFactor;

            Destroy(bulletHoleInstance, 15f);
            }

            Instantiate(bulletImpact, hit.point + (hit.normal * 0.001f),
                Quaternion.FromToRotation(Vector3.up, hit.normal));

            Instantiate(muzzleEffects, barrel.position,
                barrel.rotation);

            _audioSource.PlayOneShot(shotSound);
            
        }
    
    private void Reload()
    {
        var ammoToReload = Math.Min(magazineSize - currentAmmo, remainingAmmo);
        currentAmmo += ammoToReload;
        remainingAmmo -= ammoToReload;
        UpdateAmmoInfo();


    }
    private void EnemyShot(Collider hitCollider)
    {
        var enemy = hitCollider.GetComponent<Enemy>();
        if (enemy.IsDead)
        {
            return;
        }

        enemy.Dead();
    }
}