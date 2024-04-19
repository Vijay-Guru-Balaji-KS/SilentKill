using UnityEngine;

public class CubeThrow : MonoBehaviour
{
    public float throwAngle = 45f;
    public float throwForce = 10f;
    public int lineSegments = 50; // Adjust the number of line segments for smoother trajectory

    private bool isThrown = false;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private LineRenderer lineRenderer;

    private void Start()
    {
        initialPosition = transform.position;

        // Add Line Renderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegments;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isThrown)
        {
            // Start holding left mouse click and dragging
            StartDrag();
        }

        if (Input.GetMouseButtonDown(1) && !isThrown)
        {
            // Start holding left mouse click
            initialPosition = transform.position;
            UpdateTrajectoryPreview();
        }

        if (isDragging)
        {
            // Update the throw angle during the drag
            UpdateThrowAngle();
        }

        if (Input.GetMouseButtonUp(1) && !isThrown)
        {
            // Release left mouse click and throw the sphere
            isDragging = false;
            lineRenderer.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Right-click to throw the sphere without trajectory preview
            ThrowSphere();
        }

        if (isThrown && Input.GetMouseButtonDown(0))
        {
            // Reset isThrown flag when right-clicking after the sphere has been thrown
            isThrown = false;
        }

    }

    private void StartDrag()
    {
        // Start dragging and store the initial position
        isDragging = true;
        initialPosition = Input.mousePosition;
    }

    private void UpdateThrowAngle()
    {
        // Calculate throw angle based on mouse movement
        float angle = Mathf.Atan2(Input.mousePosition.y - initialPosition.y, Input.mousePosition.x - initialPosition.x) * Mathf.Rad2Deg;

        // Adjust angle to be within 0 to 180 degrees
        throwAngle = Mathf.Clamp(angle, 0f, 180f);

        // Update trajectory preview while dragging
        UpdateTrajectoryPreview();
    }

    private void UpdateTrajectoryPreview()
    {
        lineRenderer.positionCount = lineSegments;
        // Calculate and update line renderer positions for trajectory preview
        float timeInterval = 0.1f;
        float currentTime = 0f;

        for (int i = 0; i < lineSegments; i++)
        {
            Vector3 position = CalculatePosition(currentTime);
            lineRenderer.SetPosition(i, position);

            currentTime += timeInterval;
        }

        lineRenderer.enabled = true;
    }

    private Vector3 CalculatePosition(float time)
    {
        float radianAngle = throwAngle * Mathf.Deg2Rad;
        float x = throwForce * Mathf.Cos(radianAngle) * time;
        float y = throwForce * Mathf.Sin(radianAngle) * time - 0.5f * Mathf.Abs(Physics.gravity.y) * time * time;

        return initialPosition + new Vector3(x, y, 0f);
    }

    private void ThrowSphere()
    {
        isDragging = false;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (rb != null)
        {
            // Calculate initial velocity based on desired throw angle
            float radianAngle = throwAngle * Mathf.Deg2Rad;
            Vector3 throwDirection = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0f);
            Vector3 throwVelocity = throwDirection * throwForce;

            // Release the sphere and apply the calculated velocity
            rb.isKinematic = false;
            rb.velocity = throwVelocity;

            // Disable the trajectory preview
            lineRenderer.enabled = false;

            isThrown = true;

            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset the sphere's position when it collides with something
        if (isThrown)
        {
            Debug.Log("Collision occurred. Resetting sphere.");
            ResetSphere();
        }
    }

    private void ResetSphere()
    {
        isDragging = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Reset position and velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            // Reset position to initial position
            transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);


            isThrown = false;
        }
    }
}
