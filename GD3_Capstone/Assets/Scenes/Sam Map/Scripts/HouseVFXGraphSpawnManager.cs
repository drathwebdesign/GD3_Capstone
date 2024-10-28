using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.VFX; 

public class HouseVFXGraphSpawnManager : MonoBehaviour
{
    public Collider houseCollider;
    public VisualEffect houseVFX; // Reference to the VFX Graph component

    private void Awake()
    {
        houseCollider = GetComponent<Collider>();
        if (houseVFX != null)
        {
            houseVFX.Stop(); // Ensure the VFX is initially off
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOnVFXGraph();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurnOffVFXGraph();
        }
    }

    void TurnOnVFXGraph()
    {
        if (houseVFX != null)
        {
            houseVFX.Play(); // Start the VFX effect
        }
    }

    void TurnOffVFXGraph()
    {
        if (houseVFX != null)
        {
            houseVFX.Stop(); // Stop the VFX effect
        }
    }
}
