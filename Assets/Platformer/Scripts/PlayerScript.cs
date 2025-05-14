using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{
    //I like to make variables for all my components even
        //if I'm not sure if I'll use them in code
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Collider2D Coll;
    public ParticleSystem PS;
    public AudioSource AS;
    public GameObject Player;
    public GameObject startPoint;

    public TextMeshPro ScoreText;
    
    //My personal stats
    public float Speed = 5;
    public float JumpPower = 10;
    public float Gravity = 3;
    
    //Variables I use to track my state
    public bool OnGround = false;
    public bool FacingLeft = false;
    
    // For preventing Wall Jumping - Arben
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    
    //My Sound Effects
    public AudioClip JumpSFX;


    public int Score = 0;
    
    void Start()
    {
        UpdateScore();
         
        //Set our rigidbody's gravity to match our stats 
        RB.gravityScale = Gravity;
    }

    void Update()
    {
        OnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        
        Vector2 vel = RB.linearVelocity;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        { 
            //If I hit right, move right
            vel.x = Speed;
            //If I hit right, mark that I'm not facing left
            FacingLeft = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        { 
            //If I hit left, move right
            vel.x = -Speed;
            //If I hit left, mark that I'm facing left
            FacingLeft = true;
        }
        else
        {  //If I hit neither, come to a stop
            vel.x = 0;
        }

        //If I hit Space and can jump, jump
        if (Input.GetKey(KeyCode.Space) && CanJump())
        { 
            vel.y = JumpPower;
            //Emit 5 dust cloud particles
            PS.Emit(5);
            //Play my jump sound
            AS.PlayOneShot(JumpSFX);
        }

        //Here I actually feed the Rigidbody the movement I want
        RB.linearVelocity = vel;
        //Use my FacingLeft variable to make my sprite face the right way
        SR.flipX = FacingLeft;

        //If I fall into the void...
        if (transform.position.y < -20)
        {
            Player.transform.position = startPoint.transform.position;
        }

    }
    public bool CanJump()
    {
        return OnGround;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        if (coin != null)
        {
            coin.GetBumped();
            Score++;
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }
}
