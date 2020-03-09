using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text winText;
    public Text livesText;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    private int scoreValue = 0;
    private int lives = 3;

    Animator anim;

    private bool facingRight = true;
   
    
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();        
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }    

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }


        if (collision.collider.tag == "Enemy")
        {
            collision.collider.gameObject.SetActive(false);
            lives = lives - 1;
            livesText.text = "Lives: " + lives.ToString();
        }


        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Franza Gregoire";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        if (lives == 0)
        {
            winText.text = "YOU LOSE! Game created by Franza Gregoire";
            Destroy(gameObject);
        }

        if (scoreValue == 4) //remember lives reset code here
        {
            gameObject.transform.position = new Vector2(7.0f, 39.32f);
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();

        }
    }
    


    private void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }

            
            }
        
    }
}



