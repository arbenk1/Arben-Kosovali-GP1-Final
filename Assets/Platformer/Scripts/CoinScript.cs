using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioSource coinSound;
    public AudioClip coinClip;

    public void GetBumped()
    {
        Destroy(gameObject);
        coinSound.PlayOneShot(coinClip);
    }
    
}
