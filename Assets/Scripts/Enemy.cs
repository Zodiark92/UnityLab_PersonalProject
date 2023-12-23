
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

    private Transform playerPos;
    private Vector3 randomPos;
    private float speedMovement;
    private string state;
    private Rigidbody enemyRb;


    private float idleStateTimer;

    private float x_rangeL = -47f;
    private float x_rangeR = 47f;

    private float z_rangeU = -47f;
    private float z_rangeD = +44f;

    private float y_pos = 1f;

    // Start is called before the first frame update
    void Start()
    {
        speedMovement = 0;
        state = EnemyState.IDLE_STATE;
        idleStateTimer = Random.Range(0f, 10f);

        StartCoroutine(StartToMoveState(idleStateTimer));
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody>();
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

        
       
    }

    private void EnemyAI()
    {
        
        if (state == EnemyState.WALK_STATE)
        {
            Vector3 direction = (randomPos - transform.position).normalized;
            //  transform.Translate(direction * speedMovement * Time.deltaTime);
            enemyRb.velocity = direction * speedMovement;

            if (Vector3.Distance(transform.position, randomPos) < 0.5f)
                StartIdleState();

        }
        else if (state == EnemyState.CHASE_STATE)
        {
            
            transform.LookAt(playerPos.position);
            enemyRb.velocity = transform.forward * chasingSpeed;
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
        StartCoroutine(StartToMoveState(idleStateTimer));
    }

    private Vector3 RandomPos()
    {
        float pos_X = Random.Range(x_rangeL, x_rangeR);
        float pos_Z = Random.Range(z_rangeU, z_rangeD);

        return new Vector3(pos_X, y_pos, pos_Z);
    }

 
}
