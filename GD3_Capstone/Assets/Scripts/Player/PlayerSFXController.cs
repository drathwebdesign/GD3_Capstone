using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{


    public int storyProgress;
    public int graveyardProgress;
    public int houseProgress;
    public int cabinProgress;
    public int mannequinProgress;

    public float VolumeQuiet = 0.3f;
    public float VolumeLoud = 1;

    [SerializeField] AudioClip narration1;
    [SerializeField] AudioClip itStinks;
    [SerializeField] AudioClip somethingTerrible;  
    [SerializeField] AudioClip someoneWatching;
    [SerializeField] AudioClip iSenseAPresence;

    [SerializeField] AudioClip mannequin1;
    [SerializeField] AudioClip strangelyCompelled;
    


    [SerializeField] AudioClip graveyardNarration1;
    [SerializeField] AudioClip graveyardNarration2;

    [SerializeField] AudioClip cabinNarration1;

    [SerializeField] AudioClip houseNarration1;
    [SerializeField] AudioClip houseNarrationFinal;

    [SerializeField] AudioClip pianoMusic;

    [SerializeField] AudioClip mainHouse1;
    [SerializeField] AudioClip mainHouse2;
    [SerializeField] AudioClip mainHouse3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        storyProgress = 0;
        graveyardProgress = 0;
        SoundFXManager.Instance.PlaySoundFXClip(1,pianoMusic, transform, 0.03f);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // spawnpoint triggers

    private void OnTriggerEnter(Collider other)
    {
        //heading right

        if (other.transform.name == "(1)SpawnTrigger(0)")
        {
            if (storyProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, narration1, transform, VolumeQuiet);
                storyProgress++;
            }
        }

            if (other.transform.name == "(1)TriggerRight(1)")
        { 
            if  (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, itStinks, transform, VolumeQuiet);
                storyProgress++;
            }

        }

//        if (other.transform.name == "(1)TriggerRight(2)")
//        {
//            if (storyProgress == 2)
//            {
//                SoundFXManager.Instance.PlaySoundFXClip(1, somethingTerrible, transform, VolumeQuiet);
//                storyProgress++;
//            }

//        }

        if (other.transform.name == "SpawntoCabinBridgeTrigger")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, someoneWatching, transform, VolumeLoud);
                storyProgress++;
            }
        }

        //heading left

        if (other.transform.name == "(1)TriggerLeft(1)")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, iSenseAPresence, transform, VolumeQuiet);
                storyProgress++;
            }
        }

        if (other.transform.name == "(1)TriggerLeft(2)")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, iSenseAPresence, transform, VolumeQuiet);
                storyProgress++;
            }
        }

        //heading backwards

        if (other.transform.name == "BackBridgeTrigger")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, somethingTerrible, transform, VolumeQuiet);
                storyProgress++;
            }
        }

        if (other.transform.name == "(1)TriggerBack(1)")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, iSenseAPresence, transform, VolumeQuiet);
                storyProgress++;
            }
        }

        //crossing the bridge

        if (other.transform.name == "(1)BridgeTrigger(1)")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, somethingTerrible, transform, VolumeQuiet);
                storyProgress++;
            }
        }

        //        if (other.transform.name == "SpawnToMainBridgeTrigger")
        //        {
        //            if (storyProgress == 2)
        //            {
        //                SoundFXManager.Instance.PlaySoundFXClip(1, someoneWatching, transform, VolumeLoud);
        //                storyProgress++;
        //            }
        //        }

        //mannequin triggers

        if (other.transform.name == "MannequinHouse" && mannequinProgress == 0)
        {
                SoundFXManager.Instance.PlaySoundFXClip(1, mannequin1, transform, 1f);
                mannequinProgress++;
            
        }

        if ((other.transform.name == "LeglessTriggerBox"|| other.transform.name == "ArmlessTriggerBox" || other.transform.name == "HeadlessTriggerBox") && mannequinProgress == 1)
        {
            SoundFXManager.Instance.PlaySoundFXClip(1, strangelyCompelled, transform, 1f);
            mannequinProgress++;

        }

        //graveyard triggers

        if (other.transform.name == "GraveyardTrigger" && cabinProgress == 0)
        {
            if (graveyardProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, graveyardNarration1, transform, VolumeQuiet);
                graveyardProgress++;
            }
        }

        if (other.transform.name == "GraveyardTrigger")
        {
            if (graveyardProgress >0 && cabinProgress>0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, graveyardNarration2, transform, VolumeQuiet);
                graveyardProgress++;
            }
        }

        // cabin triggers

        if (other.transform.name == "CabinTrigger")
        {
            if (cabinProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, cabinNarration1, transform, VolumeQuiet);
                cabinProgress++;
            }
        }

        // House Triggers


        if (other.transform.name == "HouseTrigger")
        { 
            if (houseProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1,houseNarration1, transform, VolumeQuiet);
                houseProgress++;
            }         

        }

        if (other.transform.name == "MainHouseEntranceTrigger")
        {
           
                SoundFXManager.Instance.PlaySoundFXClip(1, houseNarrationFinal, transform, VolumeQuiet);
                
           

        }

        if (other.transform.name == "Box Volume")
        {
            SoundFXManager.Instance.PlaySoundFXClip(1, mainHouse1, transform, VolumeQuiet);
            SoundFXManager.Instance.PlaySoundFXClip(1, mainHouse2, transform, VolumeQuiet);
            SoundFXManager.Instance.PlaySoundFXClip(1, mainHouse3, transform, VolumeQuiet);
        }
    }
} 