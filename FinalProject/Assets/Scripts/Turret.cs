using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnDrawGizmosSelected()
    {
        for (int x=0; x<size.x; x++)
        {
            for (int y=0; y<size.y; y++)
            {
                Gizmos.color = new Color(33f,1f,0f,0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1,1f,1));
            }
        }
    }
}
