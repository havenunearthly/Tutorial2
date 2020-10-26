using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private int scoreValue = 0;
    private int lives;
    private bool facingRight = true;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        lives = 3;
        livesText.text = "Lives: " + lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
{
Application.Quit();
}
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
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
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
           if(scoreValue == 6)
           {
            transform.position = new Vector2(50.0f, 0.0f);
            lives = 3;
            livesText.text = "Lives: " + lives.ToString(); 
           }
        }
        if(scoreValue >= 12){
            winText.text = "You win! Game created by Josephine Rodriguez";
         musicSource.clip = musicClipOne;
         musicSource.loop = false;
         musicSource.Stop();
         musicSource.clip = musicClipTwo;
         musicSource.Play();    
        }

        if(collision.collider.tag == "Enemies")
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject); 
        }
        if(lives == 0)
        {
            winText.text = "You lose!";
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 1);
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            else
            {
                anim.SetInteger("State", 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
