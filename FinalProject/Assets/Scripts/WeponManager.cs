using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponManager : MonoBehaviour
{
    [SerializeField] private Weapon[] wepons;
    [SerializeField] private GameObject weponInventory;
    public Weapon _selectedWeapon { get; private set; }
    private bool _state;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeaponBox(!_state);
        }
    }
    public void SelectWeapon(Weapon selectedWepon)
    {
        foreach(var wepon in wepons)
        {
            wepon.gameObject.SetActive(wepon == selectedWepon);
        }
        _selectedWeapon = selectedWepon;
    }


    public void ToggleWeaponBox(bool state)
    {
        weponInventory.SetActive(state);
        _state = state;
        Time.timeScale = state ? 0 : 1;
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;

    }
}
