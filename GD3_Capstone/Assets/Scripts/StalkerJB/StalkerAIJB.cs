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

    [SerializeField] private float lifetime = 30f;
    private float timeAlive = 0f;

    [SerializeField] private float attackRange = 2f;
    private bool isAttacking = false;

    [SerializeField] private SphereCollider attackCollider; // Reference to the attack collider

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SoundFXManager.Instance.PlayRandomSoundFXClip(0, roarSoundClips, transform, 1f);

        if (attackCollider != null) {
            attackCollider.enabled = false;
            attackCollider.isTrigger = true;
        }
    }

    void Update() {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime) {
            Destroy(gameObject);
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

        CheckAttackRange();
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

    void CheckAttackRange() {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange) {
            if (!isAttacking) {
                isAttacking = true;
                animator.SetTrigger("isAttack");
                navMeshAgent.isStopped = true;

                if (attackCollider != null) {
                    attackCollider.enabled = true; // Enable the attack collider when in range
                }
            }
        } else {
            isAttacking = false;
            navMeshAgent.isStopped = false;

            if (attackCollider != null) {
                attackCollider.enabled = false; // Disable the attack collider when out of range
            }
        }
    }
}