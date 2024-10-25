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
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        MovementCheck();
        AttackCheck();
    }
    public void MovementCheck()
    {
        //if enemy is outside of player range move towards the player
        if (Vector3.Distance(transform.position, playerFow.transform.position) > playerFow.viewRadius + positionOffsetToPlayer)
        {
            agent.Move(playerFow.transform.position);
        }
        //if enemy is too close to the player move him towards to scare player
        else
            agent.Move(playerFow.transform.forward * -1);
    }
    public void AttackCheck()
    {
        //if player gets in attack range attack the player and kill him
        if(Vector3.Distance(transform.position, playerFow.transform.position) <= attackRange)
        {
            //waaaagh
            agent.Move(playerFow.transform.position);
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
