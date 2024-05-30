
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 15f;

    [SerializeField]
    private float chasingSpeed = 20f;

    [SerializeField]
    private float chaseDistance = 10f;

    [SerializeField]
    private Transform modelTransform;

    [SerializeField]
    private float walk_y_offset = 1.25f;

    [SerializeField]
    private float idle_y_offset = 1.15f;

    [SerializeField]
    private float chase_y_offset = 0f;


    private Transform playerPos;
    private Vector3 randomPos;
    private float speedMovement;
    private string state;
    private Rigidbody enemyRb;
    private Animator enemyAnim;

    private int speedAnim = 0;

    private float idleStateTimer;

    private float x_rangeL = -66.4f;
    private float x_rangeR = 84.94f;

    private float z_rangeU = -72f;
    private float z_rangeD = +14f;

    private float y_pos = 1.13f;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speedMovement = 0;
        state = EnemyState.IDLE_STATE;
        idleStateTimer = Random.Range(0f, 10f);
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        enemyAnim.SetInteger("speed_int", 0);

        StartCoroutine(StartToMoveState(idleStateTimer));
       
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos == null)
           return;

        EnemyAI();
        
        if (Vector3.Distance(transform.position, playerPos.position) < chaseDistance)
          {
                state = EnemyState.CHASE_STATE;
                
          }

        modelTransform.localRotation = new Quaternion(0, 0, 0, 0);
       
    }

    private void EnemyAI()
    {
        
        if (state == EnemyState.WALK_STATE)
        {
            Vector3 direction = (randomPos - transform.position).normalized;
            transform.LookAt(randomPos);
            enemyRb.velocity = direction * speedMovement;

            enemyAnim.SetInteger("speed_int", 1);
      
            transform.position = new Vector3(transform.position.x, walk_y_offset, transform.position.z);
 
            if (Vector3.Distance(transform.position, randomPos) < 0.5f)
                StartIdleState();

        }
        else if (state == EnemyState.CHASE_STATE)
        {
            Vector3 lookDirection = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);

            transform.LookAt(lookDirection);
            enemyRb.velocity = transform.forward * chasingSpeed;

            enemyAnim.SetInteger("speed_int", 2);

        } else if(state == EnemyState.IDLE_STATE)
        {
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0, transform.rotation.w);
            enemyRb.velocity = Vector3.zero;

           transform.position = new Vector3(transform.position.x, idle_y_offset, transform.position.z);

         //  modelTransform.localPosition = new Vector3(modelTransform.localPosition.x, 0f, modelTransform.localPosition.z);

        }


    }

    IEnumerator StartToMoveState(float timer)
    {
        yield return new WaitForSeconds(timer);

        state = EnemyState.WALK_STATE;
        speedMovement = walkSpeed;
        randomPos = RandomPos();
    }

    private void StartIdleState()
    {
        speedMovement = 0f;
        state = EnemyState.IDLE_STATE;
        idleStateTimer = Random.Range(0f, 25f);
        enemyAnim.SetInteger("speed_int", 0);
        StartCoroutine(StartToMoveState(idleStateTimer));
    }

    private Vector3 RandomPos()
    {
        float pos_X = Random.Range(x_rangeL, x_rangeR);
        float pos_Z = Random.Range(z_rangeU, z_rangeD);

        return new Vector3(pos_X, transform.position.y, pos_Z);
    }

 
}
