using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammoType
{
    Revolver
}

public class Items : MonoBehaviour
{
    [SerializeField] private ammoType itemType; // Ammo type for this item
    [SerializeField] private int bullets = 7;   // Number of bullets this item provides
    private bool _isPickedUp=false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!_isPickedUp)
        {
            // Find the weapon in the WeaponList that matches the ammo type
            var currentWeapon = Weapon.WeaponList.Find(w => w.WeaponType.ToString().Equals(itemType.ToString()));

            if (currentWeapon != null) // Check if a matching weapon was found
            {
                currentWeapon.remainingAmmo += bullets; // Add bullets to the weapon's ammo
                GetComponent<AudioSource>().Play();
                _isPickedUp = true;
                currentWeapon.UpdateAmmoInfo();
                // Destroy the ammo pickup after it's used
                Destroy(gameObject,0.5f);
                
            }
            else
            {
                Debug.LogWarning($"No matching weapon found for ammo type: {itemType}");
            }
        }
    }
}
