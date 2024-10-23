using UnityEngine;
using UnityEngine.PostProcessing;

public class TriggerManager : NewMonoBehaviourScript
{
    public int counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// when 'player' enters a trigger zone, find player rotation and trigger an event depending on the counter number
    public void TriggerEnter()
    {
        transform playerRotation = other.transform.rotation;

        Debug.Log("rotation" + playerRotation);
        if (counter == 0)
        {
            
            counter++;
        }
    }
}

