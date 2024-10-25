using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShakeManager : MonoBehaviour
{
    public List<GameObject> shackObjects;

    public float forceStrength = 5f;
    public float torqueStrength = 10f;
    public Light shackLight;
    public float flickerInterval = 0.1f;
    
    public Animator doorAnimator;
    public float doorOpenDistance = 1f;

    public GameObject mannequin;
    public Animator mannequinAnimator;
    public string headTurningAnimation = "HeadTurn";

    private bool playerInShack = false;
    private bool flickerActive = false;
    private Transform playerTransform;

    [SerializeField] AudioSource shackAudio;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        mannequin.SetActive(false);
        shackAudio = gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < doorOpenDistance)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInShack = true;
            StartFlickering();
            ActivateMannequin();
            PlayShackAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInShack = false;

            StopFlickering();
            DeactivateMannequin();
            StopShackAudio();
        }
    }
    IEnumerator FlickerLight()
    {
        while (flickerActive)
        {
            shackLight.enabled = !shackLight.enabled;

            yield return new WaitForSeconds(flickerInterval);
        }

        shackLight.enabled = true;
    }

    void StartFlickering()
    {
        if (!flickerActive)
        {
            flickerActive = true;
            StartCoroutine(FlickerLight());
        }
    }

    void StopFlickering()
    {
        flickerActive = false;
        StopCoroutine(FlickerLight());
        shackLight.enabled = false;
    }

    void OpenDoor()
    {
        doorAnimator.SetBool("isOpen", true);
    }

    void CloseDoor()

    {
        doorAnimator.SetBool("isOpen", false);
    }

    void ActivateMannequin()

    {
        mannequin.SetActive(true);
        mannequinAnimator.Play(headTurningAnimation);
    }

    void DeactivateMannequin()
    {
        mannequin.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (playerInShack)
        {
            StartRandomMovement();
        }
    }

    void StartRandomMovement()
    {
        foreach (GameObject obj in shackObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            {
                Vector3 constantForce = new Vector3
                (
                    Random.Range(-1f,1f),
                    Random.Range(-1f,1f),
                    Random.Range(-1f,1f))
                * forceStrength;
                rb.AddForce(constantForce);

                Vector3 constantTorque = new Vector3
                (
                    Random.Range(-1f,1f),
                    Random.Range(-1f,1f),
                    Random.Range(-1f,1f))
                * torqueStrength;
                rb.AddTorque(constantTorque);
            }
        }
    }
  void PlayShackAudio()
    {
        if (shackAudio != null && !shackAudio.isPlaying)
        {
            shackAudio.Play();
        }
    }

    void StopShackAudio()
    {
        if (shackAudio != null && shackAudio.isPlaying)
        {
            shackAudio.Stop();
        }
    }
}
