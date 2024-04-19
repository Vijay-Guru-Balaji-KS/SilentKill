using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool disabled = false;   

    [Header("Player Moment")]
    public float playerspeed = 1.9f;
    public float playerSprint = 5f;
    

    [Header("Player Animator and gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Script Camera")]
    public Transform playerCamera;

    [Header("Player Jumping and Velocity")]
    public float jumpRange = 1f;
    public float turntime = 0.1f;
    float turnvelocity;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    AimController aimController;

    [Header("Teleportation refs")]
    public TextMeshProUGUI countref;

    /*[Header("Teleportation refs")]
    public TextMeshProUGUI countref;

    [Header("Lives Counting settings")]
    public LivesCount livesCount;

    [Header("GameOver Panel")]
    public GameObject gameOverPanel;

    public TextMeshProUGUI currentLives;

    public UnityEvent playerout;*/

    // Start is called before the first frame update
    void Start()
    {
        aimController = GetComponent<AimController>();
        //livesCount = GetComponent<LivesCount>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (int.Parse(currentLives.text) == 0)
        {
            gameOverPanel.SetActive(true);
        }*/

        if (!disabled)
        {
            onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

            if (onSurface && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            PlayerMove();
            //Jump();
            Sprint();

            //TrytoTeleport();

            //KillAnimation();
        }
    }

   /* void KillAnimation()
    {
        if()
    }*/

    public void MoveBackToGameWorld()
    {
        SceneManager.LoadScene("Town");
    }

    /*private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("ViewZone"))
        {
            playerout.Invoke();
            //livesCount.GetComponent<LivesCount>().decrementLife();
            Debug.Log("triggering from player");
        }
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.transform.parent.name);
        if (other.gameObject.CompareTag("Zone"))
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                //this.transform.position = other.transform.parent.GetComponent<TakeDownEnemy>().killpos.transform.position;
                //this.transform.LookAt(other.transform.position);
                //this.transform.position = other.transform.GetComponentInChildren<Transform>().FindChild("KillPosition").position;\

                gameObject.transform.LookAt(other.transform.parent);
                other.transform.parent.GetComponentInParent<EnemyPatrol>().enemyAnimator.SetInteger("Idle", 1);
                Destroy(other.transform.parent.GetComponent<EnemyPatrol>().zone);
                other.transform.parent.GetComponentInParent<EnemyPatrol>().enabled = false;

                StartCoroutine(KillWait());

                other.transform.parent.GetComponent<TakeDownEnemy>().eAnimator.SetTrigger("Killed");
                //animator.SetTrigger("Takedown");
                //other.transform.parent.GetComponent<TakeDownEnemy>().enabled = true;
                //animator.ResetTrigger("Takedown");
            }
        }
    }

    IEnumerator KillWait()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("Takedown");
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        Debug.Log("triggering from exit function");
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Ball"))
        {
            aimController.ballcount = 1;
            aimController.teleportcount.text = "1";
        }
    } */



    void PlayerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude > 0.1f)
        {

            //animator.SetBool("Walk", true);
            //animator.SetBool("Running", false);
            //wrote know
            animator.SetBool("Idle", false);

            animator.SetBool("Walk", true);

            //animator.Play("Walk");

            //Atan provides the value in radians then we convert to degrees
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnvelocity, turntime);

            //returns rotation that rotates z degress around z axis , x degree around the x axis and y degrees around the y axis.   
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 movedirection = Quaternion.Euler(0, targetangle, 0) * Vector3.forward;

            characterController.Move(movedirection.normalized * playerspeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            //animator.SetBool("Running", false);
            //animator.Play("Idle");
        }
    }

    /*void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
            //animator.Play("Idle");
        }
    }*/

    void Sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) && onSurface)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            animator.SetBool("Walk", false);
            animator.SetBool("Running", true);

            //animator.Play("Running");


            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                //Atan provides the value in radians then we convert to degrees
                float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnvelocity, turntime);

                //returns rotation that rotates z degress around z axis , x degree around the x axis and y degrees around the y axis.   
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 movedirection = Quaternion.Euler(0, targetangle, 0) * Vector3.forward;

                characterController.Move(movedirection.normalized * playerSprint * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("Running", false);
            //animator.SetBool("Walk", true);
            animator.SetBool("Idle", true);
        }
    }
}
