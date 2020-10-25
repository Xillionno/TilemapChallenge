using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text endscreen;
    public Text score;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    public AudioSource musicSource;
    public AudioClip victory;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        endscreen.text = "";
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMotement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMotement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D))

        {
            anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.D))

        {

            anim.SetInteger("State", 0);

        }
        if (Input.GetKeyDown(KeyCode.A))

        {
            anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.A))

        {

            anim.SetInteger("State", 0);

        }
        if (Input.GetKeyDown(KeyCode.W))

        {
            anim.SetInteger("State", 2);

        }

        if (Input.GetKeyUp(KeyCode.W))

        {

            anim.SetInteger("State", 0);

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(scoreValue == 4)
        { endscreen.text = "You win! Game created by Emily Woods";
            musicSource.clip = victory;
            musicSource.Play();
            Destroy(this);
        }

        if(collision.collider.tag == "Enemy")
        { 
            livesValue -= 1;
            lives.text = "Lives : " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(livesValue <= 0)
        {
            endscreen.text = "You lose! Game Created by Emily Woods";
            Destroy(this);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                
            }
        }
    }
}
