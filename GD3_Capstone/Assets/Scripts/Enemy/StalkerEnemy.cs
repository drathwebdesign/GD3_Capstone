using UnityEngine;

public class StalkerEnemy : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
    }
    public void Dissapear()
    {
        gameObject.SetActive(false);

    }
}
