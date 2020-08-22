using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private Animator enemy_Anim;
    private NavMeshAgent navAgent;

    private Transform playerTarget;
    public float moveSpeed = 1;

    public float attackDistance = 1f;
    public float chasePlayerAfterAttackDistance = 1f;

    private float waitBeforeAttackTime = 2f;
    private float attackTimer;

    private EnemyState enemyState;

    // Start is called before the first frame update
    void Awake()
    {
        enemy_Anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start()
    {
        enemyState = EnemyState.CHASE;
        attackTimer = waitBeforeAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.CHASE)
        {
            ChasePlayer();
        }
        if (enemyState == EnemyState.ATTACK)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = moveSpeed;

        if (navAgent.velocity.sqrMagnitude == 0)
        {
            enemy_Anim.SetInteger(parameterAnim.condition, 0);
        }
        else
        {
            enemy_Anim.SetInteger(parameterAnim.condition, 1);
        }

        if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            enemyState = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        
        
        attackTimer += Time.deltaTime;
        if (attackTimer > waitBeforeAttackTime)
        {
            enemy_Anim.SetInteger(parameterAnim.condition, 2);
            attackTimer = 0f;
        }
        
        if (Vector3.Distance(transform.position, playerTarget.transform.position) > attackDistance + chasePlayerAfterAttackDistance)
        {
            navAgent.isStopped = false;
            enemyState = EnemyState.CHASE;
        }
        
    }
}
