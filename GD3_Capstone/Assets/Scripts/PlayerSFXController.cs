using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public int storyProgress;
    public int graveyardProgress;
    public int houseProgress;
    public int cabinProgress;


    [SerializeField] AudioClip narration1;
    [SerializeField] AudioClip narration2;
    [SerializeField] AudioClip narration3;

    [SerializeField] AudioClip graveyardNarration1;
    [SerializeField] AudioClip graveyardNarration2;

    [SerializeField] AudioClip cabinNarration1;

    [SerializeField] AudioClip houseNarration1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        storyProgress = 0;
        graveyardProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // spawnpoint triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "(1)SpawnTrigger(0)")
        {
            if (storyProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(narration2, transform, 1f);
                storyProgress++;
            }
        }

            if (other.transform.name == "(1)TriggerRight(1)")
        { 
            if  (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(narration2, transform, 1f);
                storyProgress++;
            }

        }

        if (other.transform.name == "(1)TriggerRight(2)")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(narration3, transform, 1f);
                storyProgress++;
            }

        }

        if (other.transform.name == "(1)TriggerLeft(1)")
        {
            if (storyProgress == 3)
            {
                SoundFXManager.Instance.PlaySoundFXClip(narration2, transform, 1f);
                storyProgress++;
            }
        }

        //graveyard triggers

        if (other.transform.name == "GraveyardTrigger" && cabinProgress == 0)
        {
            if (graveyardProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(graveyardNarration1, transform, 1f);
                graveyardProgress++;
            }
        }

        if (other.transform.name == "GraveyardTrigger")
        {
            if (graveyardProgress >0 && cabinProgress>0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(graveyardNarration2, transform, 1f);
                graveyardProgress++;
            }
        }

        // cabin triggers

        if (other.transform.name == "CabinTrigger")
        {
            if (cabinProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(cabinNarration1, transform, 1f);
                cabinProgress++;
            }
        }

        // House Triggers


        if (other.transform.name == "HouseTrigger")
        {
            if (houseProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(houseNarration1, transform, 1f);
                houseProgress++;
            }
        }
    }
} 