using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    public float jumpForce = 20f;
    public Sprite flexedSprite;
    public Sprite defaultSprite;
    public float animationDuration = 0.2f;
    public AudioSource JumpSound;
    public AudioClip JumpClip;

    private SpriteRenderer spriteRenderer;
    private bool isAnimating = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        JumpSound.PlayOneShot(JumpClip);
        
        if (other.CompareTag("Player") && !isAnimating)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                StartCoroutine(AnimatePad());
            }
        }
    }

    private System.Collections.IEnumerator AnimatePad()
    {
        isAnimating = true;
        spriteRenderer.sprite = flexedSprite;
        yield return new WaitForSeconds(animationDuration);
        spriteRenderer.sprite = defaultSprite;
        isAnimating = false;
    }
}