using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class StalkerEnemy : MonoBehaviour
{
    public float appereanceDelay = 1f;

    [SerializeField] FieldOfView playerFow;
    
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        
    }
    public void MoveTowards()
    {

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
