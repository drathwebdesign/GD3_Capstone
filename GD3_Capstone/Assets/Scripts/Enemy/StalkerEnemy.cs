using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class StalkerEnemy : MonoBehaviour
{
    public float attackRange = 10f;
    public float appereanceDelay = 1f;
    public float dissapearDelay = 1f;
    public float positionOffsetToPlayer = 5f;
    public bool isActive = true;

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
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            MovementCheck();
            AttackCheck();
        }
    }
    public void MovementCheck()
    {
        float distance = Vector3.Distance(playerFow.transform.position, transform.position);

        //always look towards the player
        transform.LookAt(playerFow.transform);

        //if enemy is outside of player range move towards the player
        if (distance > playerFow.viewRadius + positionOffsetToPlayer)
        {
            Move(-playerFow.transform.forward, agent.speed);
        }
        //if enemy is too close to the player move him towards to scare player
        else if (distance < playerFow.viewRadius + positionOffsetToPlayer)
        {
            Move(playerFow.transform.forward, agent.speed);
        }

    }
    public void Move(Vector3 direction, float speed)
    {
        agent.Move(direction * speed * Time.deltaTime);
    }
    public void AttackCheck()
    {
        //if player gets in attack range attack the player and kill him
        if (Vector3.Distance(transform.position, playerFow.transform.position) <= attackRange)
        {
            //waaaagh
            agent.Move(-playerFow.transform.forward * agent.speed * Time.deltaTime);
        }
    }
    public void Appear()
    {
        StartCoroutine(SetEnemyEnabledDelay(true, appereanceDelay));
    }
    public void Dissapear()
    {
        StartCoroutine(SetEnemyEnabledDelay(false, dissapearDelay));
    }
    IEnumerator SetEnemyEnabledDelay(bool enemyEnabled, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(enemyEnabled);
    }
}
