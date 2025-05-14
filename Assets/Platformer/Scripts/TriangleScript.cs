using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TriangleScript : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject Player;
    public AudioSource CrunchSound;
    public AudioClip CrunchClip;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((gameObject.CompareTag("Triangle")) || (gameObject.CompareTag("Saw")))
        {
            CrunchSound.PlayOneShot(CrunchClip);
            Player.transform.position = startPoint.transform.position;
        }
    }
}
