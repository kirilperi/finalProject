using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door; // Reference to the door
    public Vector3 openPosition; // Position the door moves to when open
    public float openSpeed = 2f; // Speed of the door opening

    private Vector3 closedPosition; // Position of the door when closed
    private bool isOpening = false;

    void Start()
    {
        // Store the initial position of the door
        if (door != null)
        {
            closedPosition = door.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Enemy"
        if (other.CompareTag("Enemy") && !isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        // Smoothly move the door to the open position
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            door.position = Vector3.Lerp(closedPosition, openPosition, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }
        door.position = openPosition; // Ensure it reaches the exact open position
    }
}
