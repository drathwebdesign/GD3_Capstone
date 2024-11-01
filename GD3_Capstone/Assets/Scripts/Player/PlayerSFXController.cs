using UnityEngine;

public class PlayerSFX : MonoBehaviour
{


    public int storyProgress;
    public int graveyardProgress;
    public int churchProgress;
    public int houseProgress;
    public int cabinProgress;
    public int mannequinProgress;

    public bool finalSceneTriggered;

    public float VolumeQuiet = 0.3f;
    public float VolumeLoud = 1;

    [SerializeField] AudioClip narration1;
    [SerializeField] AudioClip itStinks;
    [SerializeField] AudioClip somethingTerrible;  
    [SerializeField] AudioClip someoneWatching;
    [SerializeField] AudioClip iSenseAPresence;

    [SerializeField] AudioClip mannequin1;
    [SerializeField] AudioClip strangelyCompelled;

    [SerializeField] AudioClip holdOn;



    [SerializeField] AudioClip graveyardNarration1;
    [SerializeField] AudioClip graveyardNarration2;
    [SerializeField] AudioClip theKillerClue;

    [SerializeField] AudioClip cabinNarration1;

    [SerializeField] AudioClip houseNarration1;
    [SerializeField] AudioClip ridOfThisCurse;
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
        finalSceneTriggered = false;
        SoundFXManager.Instance.PlaySoundFXClip(0, pianoMusic, transform, 0.02f);

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
                SoundFXManager.Instance.PlaySoundFXClip(0, narration1, transform, 1f);
                storyProgress++;
            }
        }

        if (other.transform.name == "(1)TriggerRight(1)")
        { 
            if  (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, itStinks, transform, 1f);
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
                SoundFXManager.Instance.PlaySoundFXClip(0, someoneWatching, transform, 1f);
                storyProgress++;
            }
        }

        //heading left

        if (other.transform.name == "(1)TriggerLeft(1)")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, iSenseAPresence, transform, 1f);
                storyProgress++;
            }
        }

        if (other.transform.name == "(1)TriggerLeft(2)")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, iSenseAPresence, transform, 1f);
                storyProgress++;
            }
        }

        //heading backwards

        if (other.transform.name == "BackBridgeTrigger")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, somethingTerrible, transform, 1f);
                storyProgress++;
            }
        }

        if (other.transform.name == "(1)TriggerBack(1)")
        {
            if (storyProgress == 2)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, holdOn, transform, 1f);
                storyProgress++;
            }
        }

        //crossing the bridge

        if (other.transform.name == "(1)BridgeTrigger(1)")
        {
            if (storyProgress == 1)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, somethingTerrible, transform, 1f);
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
                SoundFXManager.Instance.PlaySoundFXClip(0, mannequin1, transform, 1f);
                mannequinProgress++;
            
        }

        if ((other.transform.name == "LeglessTriggerBox"|| other.transform.name == "ArmlessTriggerBox" || other.transform.name == "HeadlessTriggerBox") && mannequinProgress == 1)
        {
            SoundFXManager.Instance.PlaySoundFXClip(0, strangelyCompelled, transform, 1f);
            mannequinProgress++;

        }

        //graveyard triggers

        if (other.transform.name == "GraveyardTrigger")
        {
            if (graveyardProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, graveyardNarration1, transform, 1f);
                graveyardProgress++;
            }
        }

        if (other.transform.name == "GraveyardTrigger")
        {
            if (graveyardProgress >0 && cabinProgress>0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, graveyardNarration2, transform, 1f);
                graveyardProgress++;
            }
        }

        // church triggers

        if (other.transform.name == "ChurchTrigger")
        {
            if (churchProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, theKillerClue, transform, 1f);
                graveyardProgress++;
            }
        }

        // cabin triggers

        if (other.transform.name == "CabinTrigger")
        {
            if (cabinProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0, cabinNarration1, transform, 1f);
                cabinProgress++;
            }
        }

        // House Triggers


        if (other.transform.name == "HouseTrigger")
        { 
            if (houseProgress == 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(0,houseNarration1, transform, 1f);
                houseProgress++;
            }
            if (houseProgress == 1 && mannequinProgress > 0)
            {
                SoundFXManager.Instance.PlaySoundFXClip(1, ridOfThisCurse, transform, VolumeQuiet);
            }

        }

        if (other.transform.name == "MainHouseEntranceTrigger" && finalSceneTriggered == false)
        {
                finalSceneTriggered = true;
                SoundFXManager.Instance.PlaySoundFXClip(0, houseNarrationFinal, transform, 1f);
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse1, transform, 0.1f);
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse2, transform, 0.1f);
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse3, transform, 0.1f);


        }

        if (other.transform.name == "Box Volume")
        {
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse1, transform, 0.1f);
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse2, transform, 0.1f);
            SoundFXManager.Instance.PlaySoundFXClip(0, mainHouse3, transform, 0.1f);
        }
    }
} 