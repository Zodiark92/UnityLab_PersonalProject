using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private Transform modelTransform;

    private Rigidbody playerRb;
    private Animator playerAnim;

    private Vector2 playerMouseInput;

    private int items=0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();

        modelTransform.localRotation = new Quaternion(0,0,0,0);

    }

    private void MovePlayer()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 localDirection = transform.TransformDirection(0f, 0f, verticalInput);
        modelTransform.localPosition = Vector3.zero;
        

        if(verticalInput > 0)
        {
            playerRb.velocity = localDirection * speed;
            playerAnim.SetInteger("speed_int", 1);

        } else
        {
            playerRb.velocity = Vector3.zero;
            playerAnim.SetInteger("speed_int", 0);
        }

    }

    private void RotatePlayer()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
      
        transform.Rotate(Vector3.up * mouseXInput * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            items++;
            Debug.Log("Items found: " + items);
        }
    }
}
