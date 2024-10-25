using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GHLightingManager : MonoBehaviour
{
    public Light light1;
    public Light light2;
    public Light light3;
    public float flickerInterval = 0.1f;
    public float flickerDuration = 3f;

    private bool isFlickering = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            StartFlickeringLights();
        }
    }
    void StartFlickeringLights()
    {
        if (!isFlickering)
        {
            isFlickering=true;
            StartCoroutine(FlickerLights());
        }
    }
    IEnumerator FlickerLights()
    {
        float elapsedTime = 0f;

        while (elapsedTime < flickerDuration)
        {
            light1.enabled = Random.Range(0, 2) == 0;
            light2.enabled = Random.Range(0, 2) == 0;
            light3.enabled = Random.Range(0, 2) == 0;

            yield return new WaitForSeconds(flickerInterval);

            elapsedTime += flickerDuration;
        }

        light1.enabled = true;
        light2.enabled = true;
        light3.enabled = true;

        isFlickering = false;
    }
}

