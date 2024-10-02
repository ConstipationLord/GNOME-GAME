using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyInCircle : MonoBehaviour
{
    public float speed = 5.0f;          // Speed of the bat's flight
    public float changeDirectionTime = 2.0f;  // Time interval for changing direction
    private Vector3 targetDirection;    // The current direction the bat is flying towards
    private float timeToChangeDirection;
    void Start()
    {
        // Start by choosing a random direction
        ChooseNewDirection();
    }

    void Update()
    {
        // Move the bat in the current direction
        FlyTowardsDirection();

        // Decrease the timer for changing direction
        timeToChangeDirection -= Time.deltaTime;

        // If it's time to change direction, pick a new random direction
        if (timeToChangeDirection <= 0)
        {
            ChooseNewDirection();
        }
    
    }

    void ChooseNewDirection()
    {
        // Get a random direction within a unit sphere, but flatten it to keep the height constant
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0;  // Keep the Y position constant (no up/down movement)

        // Set the target direction and normalize it
        targetDirection = randomDirection.normalized;

        // Set the time for the next change of direction
        timeToChangeDirection = changeDirectionTime;
    }

    void FlyTowardsDirection()
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + targetDirection * speed * Time.deltaTime;

        // Keep the bat at the specified height
        newPosition.y = transform.position.y;

        // Apply the new position
        transform.position = newPosition;

        // Rotate the bat to face the direction it's heading
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}