using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurretLvl1 : MonoBehaviour
{
    public Vector2Int GredSize = new Vector2Int(10, 10);
    private Turret[,] grid;
    private Turret buildTurret;
    private Camera mainCa; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildTurret != null)
        {
            var groundFloor = new Plane(Vector2.up, Vector2.zero);
            Ray ray=mainCa.ScreenPointToRay(Input.mousePosition);

            if (groundFloor.Raycast(ray,out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);
               

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                buildTurret.transform.position = new Vector3(x, 0, y);

                if (Input.GetMouseButton(0))
                {
                    buildTurret=null;
                }
            }
        }

    }
    private void Awake()
    {
        grid = new Turret[GredSize.x, GredSize.y];
        mainCa = Camera.main;
    }
    public void StartPlacingTurret(Turret turretPrf)
    {
        if(buildTurret != null)
        {
            Destroy(buildTurret);
        }
            
        buildTurret = Instantiate(turretPrf);
    }
}
