using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private Rigidbody2D rbody;
    public float speed;
    public Text score;
    public Text life;
    public Text win;
    private int scoreValue = 0;
    private int lifeValue = 3;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public Transform warp;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicCliptwo;
    Animator anim;
    private bool facingRight = true;


    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();
        life.text = lifeValue.ToString();
        win.text = ("");
    }
    void Awake() 
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = lifeValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(scoreValue == 4)
        {
            win.text =("You Win Created by Gage Langlais to transistion into level 2 either jump and hit R at the same time you hit the ground, or run up to the wall and hit R as soon as you hit the wall.");
            musicSource.clip = musicClipOne;
            musicSource.Play();

            if(Input.GetKey(KeyCode.R))
            {
                transform.position=new Vector2(warp.position.x,warp.position.y);
                lifeValue = 3;
                life.text = lifeValue.ToString();
                win.text =("");
                if (scoreValue == 4)
                {
                    win.text =("You win created by Gage Langlais");
                }
            }

            if (transform.position.x >= 149f )
            {
                musicSource.clip = musicCliptwo;
                musicSource.Play();
                win.text =("");
            }

        

        }

        if (scoreValue == 8)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            win.text =("You win created by Gage Langlais");
        }

        else if (lifeValue == 0)
        {
            Destroy(gameObject);
            win.text =("Game Over Created By Gage Langlais");
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
            if(Input.GetKey("right"))
            {
                anim.SetInteger("State", 1);
            }
            if(Input.GetKey("left"))
            {
                anim.SetInteger("State", 1);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey("right"))
            {
                anim.SetInteger("State", 2);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey("left"))
            {
                anim.SetInteger("State", 2);
            }


        }
        if (collision.collider.tag == "Walls")
        {
            anim.SetInteger("State", 0);
            if(Input.GetKey("right"))
            {
                anim.SetInteger("State", 1);
            }
        }


    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }



}