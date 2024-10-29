using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class GuardHouseManager : MonoBehaviour
{
    public Animator mannequinAnimator1;
    public Animator mannequinAnimator2;
    public string turnAnimation1 = "Mannequin1Turn";
    public string turnAnimation2 = "Mannequin2Turn";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayTurnAnimations();
        }
    }

    void PlayTurnAnimations()
    {
        mannequinAnimator1.Play(turnAnimation1);
        mannequinAnimator2.Play(turnAnimation2);
    }

}
