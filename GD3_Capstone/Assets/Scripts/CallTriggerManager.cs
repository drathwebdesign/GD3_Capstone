using UnityEngine;
using UnityEngine.Events;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEventEnter;

    private void OnTriggerEnter(Collider other)
    {

        //we want to know that the 'player' is entering the trigger and also what direction they are heading in

        if (other.CompareTag("Player"))
        {

            Debug.Log("hello");
            triggerEventEnter.Invoke();
        }
    }
}
