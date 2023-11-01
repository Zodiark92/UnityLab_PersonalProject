using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    private Rigidbody playerRb;

    private Vector2 playerMouseInput;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();

    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 localDirection = transform.TransformDirection(horizontalInput, 0f, verticalInput);
        playerRb.velocity = localDirection * speed;
    }

    private void RotatePlayer()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
      
        transform.Rotate(Vector3.up * mouseXInput * rotationSpeed * Time.deltaTime);
    }
}
