using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class StalkerEnemy : MonoBehaviour
{
    public float attackRange = 10f;
    public float appereanceDelay = 1f;
    public float positionOffsetToPlayer = 5f;

    [SerializeField] FieldOfView playerFow;
    
    private NavMeshAgent agent;

    private enum EnemyState
    {
        IDLE,
        MOVING,
        ATTACKING,
    }
    private EnemyState enemyState;

    private void Start()
    {
        enemyState = EnemyState.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        MovementCheck();
        AttackCheck();
    }
    public void MovementCheck()
    {
        if(enemyState != EnemyState.ATTACKING)
        //if enemy is outside of player range move towards the player
        if (Vector3.Distance(transform.position, playerFow.transform.position) > playerFow.viewRadius + positionOffsetToPlayer)
        {
            agent.Move(playerFow.transform.position);
        }
        //if enemy is too close to the player move him towards to scare player
        else
        {
            agent.Move(playerFow.transform.forward * -1);
        }

        enemyState = EnemyState.MOVING;
    }
    public void AttackCheck()
    {
        //if player gets in attack range attack the player and kill him
        if(Vector3.Distance(transform.position, playerFow.transform.position) <= attackRange)
        {
            //waaaagh
            agent.Move(playerFow.transform.position);
            enemyState = EnemyState.ATTACKING;
        }
    }
    public void Appear()
    {
        gameObject.SetActive(true);
    }
    public void Dissapear()
    {
        gameObject.SetActive(false);

    }
}
