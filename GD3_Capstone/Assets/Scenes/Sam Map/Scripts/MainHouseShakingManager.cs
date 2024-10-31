using NUnit.Framework;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHouseShakingManager : MonoBehaviour
{
    public List<GameObject> houseObjects;

    public float forceStrength = 5f;
    public float torqueStrength = 10f;
    public Collider Collider;

    private bool playerInHouse = false;
    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInHouse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInHouse = false; 
        
        }
    }

    private void FixedUpdate()
    {
        if (playerInHouse)
        {
            StartRandomMovement();
        }
    }

    void StartRandomMovement()
    {
        foreach (GameObject obj in houseObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            {
                Vector3 constantForce = new Vector3
                (
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f))
                * forceStrength;
                rb.AddForce(constantForce);

                Vector3 constantTorque = new Vector3
                (
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f))
                * torqueStrength;
                rb.AddTorque(constantTorque);
            
            }
        }
        
    }
}
