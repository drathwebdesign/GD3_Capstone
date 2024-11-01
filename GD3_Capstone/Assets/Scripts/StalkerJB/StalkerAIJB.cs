using UnityEngine;
using UnityEngine.AI;

public class StalkerAIJB : MonoBehaviour {
    [SerializeField] AudioClip[] roarSoundClips;
    private float cooldownTimeRoar = 10f;
    private float timeSinceLastRoar = 0f;
    private Transform playerTransform;
    private Animator animator;
    private bool isRoaring = false;
    private bool hasStartedMoving = false;
    private NavMeshAgent navMeshAgent;

    private float initialMoveDelay = 7f;
    private float currentMoveDelay = 0f;

    [SerializeField] private float lifetime = 30f; // Lifetime in seconds
    private float timeAlive = 0f;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SoundFXManager.Instance.PlayRandomSoundFXClip(0, roarSoundClips, transform, 1f);
    }

    void Update() {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime) {
            Destroy(gameObject); // Destroy the stalker after lifetime expires
            return;
        }

        if (!hasStartedMoving) {
            currentMoveDelay += Time.deltaTime;
            if (currentMoveDelay >= initialMoveDelay) {
                hasStartedMoving = true;
            } else {
                return;
            }
        }


        Roar();

        if (isRoaring || playerTransform == null) {
            navMeshAgent.isStopped = true;
            return;
        } else {
            navMeshAgent.isStopped = false;
        }

        navMeshAgent.SetDestination(playerTransform.position);

        bool isWalking = navMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }

    void Roar() {
        timeSinceLastRoar += Time.deltaTime;

        if (timeSinceLastRoar >= cooldownTimeRoar && !isRoaring) {
            isRoaring = true;
            timeSinceLastRoar = 0f;
            SoundFXManager.Instance.PlayRandomSoundFXClip(1, roarSoundClips, transform, 1f);
            animator.SetTrigger("isRoaring");
            animator.SetBool("isWalking", false);
            Invoke("EndRoar", 2f);
        }
    }

    void EndRoar() {
        isRoaring = false;
    }
}