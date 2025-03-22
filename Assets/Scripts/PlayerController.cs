using System.Collections;
using UnityEngine;
// using Unity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 10f;
    [SerializeField]
    private float topSpeedTime = 3f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float jumpCancelTime = 3f;

    private Rigidbody rb;
    private float lerpedForwardSpeed;
    private float lerpedYSpeed;
    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        StartCoroutine(LerpForwardPos(0, forwardSpeed));
    }
    private void Update()
    {
        // if()
        MoveForward();
        JumpInput();
        // transform.position = new(transform.position.x, lerpedHeight, transform.position.z);
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(JumpCancelSmooth(rb.linearVelocity.y));
        }
    }

    private IEnumerator JumpCancelSmooth(float startVelocity)
    {
        float timeElapsed = 0;
        while (timeElapsed < jumpCancelTime)
        {
            float t = timeElapsed / jumpCancelTime;
            lerpedYSpeed = Mathf.Lerp(startVelocity, -10, t);
            // Debug.Log("Lerped Speed : " + lerpedYSpeed);
            rb.linearVelocity = new(rb.linearVelocity.x, lerpedYSpeed, rb.linearVelocity.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerpedYSpeed = 0;
    }

    private void Jump()
    {
        Debug.Log("Jump Pressed");
        if (!isGrounded)
        {
            return;
        }
        isGrounded = false;
        rb.AddForce(new(0, jumpHeight), ForceMode.Impulse);

        // float yPos = transform.position.y;
        // StartCoroutine(LerpJump(yPos, yPos + jumpHeight));
    }

    private void MoveForward()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z += lerpedForwardSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }

    private IEnumerator LerpForwardPos(float start, float end)
    {
        float timeElapsed = 0;
        while (timeElapsed < topSpeedTime)
        {
            float t = timeElapsed / topSpeedTime;
            lerpedForwardSpeed = Mathf.Lerp(start, end, t);
            Debug.Log("Lerped Speed : " + lerpedForwardSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerpedForwardSpeed = end;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
