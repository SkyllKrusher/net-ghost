using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float baseForwardSpeed = 10f;
    [SerializeField]
    private float topSpeedTime = 3f;
    [SerializeField]
    private float jumpHeight = 10f;
    [SerializeField]
    private float jumpCancelTime = 3f;
    [SerializeField]
    private GameController gameController;

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
        // Debug.Log(gameController.GameSpeed);
        StartCoroutine(LerpForwardSpeed(transform.position.z, baseForwardSpeed * gameController.GameSpeed));
    }
    private void Update()
    {
        if (!gameController.IsGameRunning)
            return;

        MoveForward();
        UpdateSyncStateData();
    }

    private void UpdateSyncStateData()
    {
        StateSync.Instance.UpdateState(transform.position);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            HandleJump();
        if (context.canceled)
            StartCoroutine(JumpCancelSmooth(rb.linearVelocity.y));
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

    private void HandleJump()
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

    private IEnumerator LerpForwardSpeed(float startZ, float endZ)
    {
        float timeElapsed = 0;
        while (timeElapsed < topSpeedTime)
        {
            float t = timeElapsed / topSpeedTime;
            lerpedForwardSpeed = Mathf.Lerp(startZ, endZ, t);
            Debug.Log("Lerped Speed : " + lerpedForwardSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerpedForwardSpeed = endZ;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
            if (collision.gameObject.GetComponent<Obstacle>() != null)
            {
                ObstacleCollision(collision.gameObject.GetComponent<Obstacle>());
            }
    }

    private void ObstacleCollision(Obstacle collidedObstacle)
    {
        PlayerDeath();
        gameController.LoseGame();
        collidedObstacle.OnHit();
    }

    private void PlayerDeath()
    {
        // gameObject.GetComponent<MeshRenderer>().enabled = (false);
    }

    public void CollectPoint()
    {
        gameController.CollectPoint();
    }
}
