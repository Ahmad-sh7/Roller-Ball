using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour
{

    Rigidbody rigidBody;
    float movingValue = 3.5f, jumpingValue = 5f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput * movingValue, 0f, verticalInput * movingValue);
        rigidBody.AddForce(movement * 5f, ForceMode.Force);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpingValue, rigidBody.velocity.z); 
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }

}
