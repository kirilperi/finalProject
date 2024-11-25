using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    public GameObject shopInventory;
    public KeyCode showShop; 
    
    // Start is called before the first frame update
    void Start()
    {
        
        shopInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(showShop))
        {
            if(shopInventory.activeSelf)
            {
                shopInventory.SetActive(false);
            }
            else
            {
                shopInventory.SetActive(true);
            }
        }
        

         
    }
}
