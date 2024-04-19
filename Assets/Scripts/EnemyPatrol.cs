using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject zone;

    public Animator enemyAnimator;

    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public float rotationSpeed = 2f;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isMoving)
        {
            if (transform.position == patrolPoints[targetPoint].position)
            {
                //enemyAnimator.SetInteger("Idle", 1);

                StartCoroutine(NextPointHold());
            }
            else
            {
                enemyAnimator.SetInteger("Idle", 0);
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
                transform.LookAt(patrolPoints[targetPoint]);

                SmoothLookAt(patrolPoints[targetPoint].position);
            }
        }
    }

    IEnumerator NextPointHold()
    {
        isMoving = true;
        enemyAnimator.SetInteger("Idle", 1);
        yield return new WaitForSeconds(4);
        
        increaseTargetVal();
        isMoving = false;
        //yield return new WaitForSeconds(2);

    }

    void SmoothLookAt(Vector3 targetPosition)
    {
        // Get the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the rotation to look at the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void increaseTargetVal()
    {
        targetPoint++;
        if(targetPoint >= patrolPoints.Length) 
        { 
            targetPoint = 0; 
        }
    }
}
