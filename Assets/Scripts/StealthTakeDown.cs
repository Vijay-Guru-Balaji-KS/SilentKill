using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthTakeDown : MonoBehaviour
{
    public KeyCode takedownKey = KeyCode.E; // Key to trigger takedown
    public float takedownRange = 1.0f; // Range for triggering takedown
    public float takedownAngleThreshold = 45.0f; // Angle within which the player is considered "behind"

    Animator animator;
    bool isTakedownActive;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(takedownKey) && !isTakedownActive)
        {
            // Check for enemies within range
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, takedownRange, LayerMask.GetMask("Enemy")); // Replace with your enemy layer name if different

            foreach (Collider enemyCollider in enemiesInRange)
            {
                GameObject enemy = enemyCollider.gameObject;

                // Check if player is behind the enemy
                if (IsPlayerBehindEnemy(enemy))
                {
                    StartTakedown(enemy);
                    break; // Stop checking further enemies if one takedown is initiated
                }
            }
        }
    }

    bool IsPlayerBehindEnemy(GameObject enemy)
    {
        Vector3 enemyToPlayer = enemy.transform.position - transform.position;
        enemyToPlayer.y = 0; // Ignore vertical distance for behind check

        float angle = Vector3.Angle(enemyToPlayer, transform.forward);
        return angle > 180.0f - takedownAngleThreshold && angle < 180.0f + takedownAngleThreshold;
    }

    void StartTakedown(GameObject enemy)
    {
        // Look at the enemy before performing takedown
        transform.LookAt(enemy.transform);

        animator.SetTrigger("Takedown"); // Trigger takedown animation
        isTakedownActive = true;

        // Disable enemy AI or movement during takedown (optional)
        // ... (code to disable enemy)
    }

    public void EndTakedown() // Called by the animation event (explained later)
    {
        isTakedownActive = false;

        // Re-enable enemy AI or movement after takedown (optional)
        // ... (code to re-enable enemy)
    }

}
