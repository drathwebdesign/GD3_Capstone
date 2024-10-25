using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public int storyProgress;

    [SerializeField] AudioClip narration1;
    [SerializeField] AudioClip narration2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundFXManager.Instance.PlaySoundFXClip(narration1, transform, 1f);
        storyProgress = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && storyProgress == 1)
        {
            SoundFXManager.Instance.PlaySoundFXClip(narration2, transform, 1f);
            storyProgress++;

        }
    }
} // other.transform.name == "box2"
