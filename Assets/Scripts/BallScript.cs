using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticles;
    private bool isEmitting = false;

    Rigidbody rigidBody;
    float movingValue = 1.5f, jumpingValue = 5f;
    private bool isGrounded = true, godMode = false;
    private int jumpCount = 0;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -10 && !godMode)
        {
            Invoke("ReloadMenuScene", 2f);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(horizontalInput) > 0f || Mathf.Abs(verticalInput) > 0f;

        if (isMoving && isGrounded && !isEmitting && !godMode)
        {
            dustParticles.Play();
            isEmitting = true;
        }
        else if ((!isMoving || !isGrounded) && isEmitting)
        {
            dustParticles.Stop();
            isEmitting = false;
        }

        if (godMode)
        {
            HandleGodModeMovement(horizontalInput, verticalInput);
        }
        else
        {
            Vector3 movement = new Vector3(horizontalInput * movingValue, 0f, verticalInput * movingValue);
            rigidBody.AddForce(movement * 5f, ForceMode.Force);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !godMode)
            Jump();

        if (Input.GetKeyDown(KeyCode.G))
            ToggleGodMode();

        if (Input.GetKeyDown(KeyCode.R))
            ReloadMenuScene();
    }

    private void ReloadMenuScene()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }


    private void Jump()
    {
        if (isGrounded || jumpCount < 2)
        {
            rigidBody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
            isGrounded = false;
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }


    private void HandleGodModeMovement(float moveHorizontal, float moveVertical)
    {
        float moveUpDown = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            moveUpDown = 1f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            moveUpDown = -1f;
        }

        Vector3 movement = new Vector3(moveHorizontal, moveUpDown, moveVertical);
        rigidBody.MovePosition(rigidBody.position + movement * 5f * Time.fixedDeltaTime);
    }

    private void ToggleGodMode()
    {
        godMode = !godMode;
        rigidBody.isKinematic = godMode;

        if (!godMode)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }
}
