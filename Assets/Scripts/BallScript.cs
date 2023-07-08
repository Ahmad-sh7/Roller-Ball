using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] TextMeshProUGUI scoreText, timeText, bestTimeText;
    private bool isEmitting = false;
    AudioScript audioScript;

    Rigidbody rigidBody;
    float movingValue = 1.5f, jumpingValue = 5f;
    private bool isGrounded = true, godMode = false, goalReachedFlag = false;
    private int jumpCount = 0, collectedItem, maxScore = 7;
    private float playtime = 0f, bestPlaytime = 0f;

    void Start()
    {
        bestPlaytime = PlayerPrefs.GetFloat("BestPlaytime" + SceneManager.GetActiveScene().buildIndex, Mathf.Infinity);
        if (bestPlaytime != Mathf.Infinity)
        {
            bestTimeText.text = "Best Playtime: " + bestPlaytime.ToString("F2") + " s";
            bestTimeText.color = Color.green;
            bestTimeText.gameObject.SetActive(true);
        }

        audioScript = GameObject.Find("Audio Object").GetComponent<AudioScript>();

        collectedItem = 0;
        scoreText.text = string.Format("Collected: {0} / 30", collectedItem);
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
        if (!goalReachedFlag)
        {
            playtime += Time.deltaTime;
            timeText.text = "Playtime: " + playtime.ToString("F2") + " s";
        }

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
            audioScript.JumpSFX();
            rigidBody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
            isGrounded = false;
            jumpCount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Goal"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Goal") && collectedItem == maxScore)
        {
            ReachedGoal();
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

    public void UpdateScore()
    {
        audioScript.CollectItemSFX();
        collectedItem += 1;
        Debug.Log("Current Score: " + collectedItem);
        scoreText.text = string.Format("Collected: {0} / 30", collectedItem);

        if (collectedItem == maxScore)
            scoreText.color = Color.green;
    }   

    private void ReachedGoal()
    {
        goalReachedFlag = true;
        if (playtime < bestPlaytime)
        {
            PlayerPrefs.SetFloat("BestPlaytime" + SceneManager.GetActiveScene().buildIndex, playtime);
            bestPlaytime = playtime;

            bestTimeText.text = "Best Playtime: " + bestPlaytime.ToString("F2") + " s";
            bestTimeText.color = Color.green;
            bestTimeText.gameObject.SetActive(true);
        }
            
        audioScript.GoalSFX();
        Invoke("ReloadMenuScene", 3f);
    }
}
