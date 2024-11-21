using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        

    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
    }

    void SetTarget()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject target in targets)
        {
            if (target == null) continue; // Skip null objects

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = target;
            }
        }
        enemyAgent.destination = closest.transform.position;
    }
}
