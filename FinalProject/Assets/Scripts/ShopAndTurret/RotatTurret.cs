using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatTurret : MonoBehaviour
{
    public Transform enemyTrn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, enemyTrn.position)<=5)
        {
            transform.LookAt(enemyTrn);

        }
        
    }
}
