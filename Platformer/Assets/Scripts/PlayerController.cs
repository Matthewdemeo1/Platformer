using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        countText.text = "";
        livesText.text = "";
        SetAllText ();

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
     Application.Quit();

     if (Input.GetKeyDown(KeyCode.Space))
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
    if (Input.GetKeyUp(KeyCode.Space))
    {
        musicSource.Stop();
    }
    if (Input.GetKeyDown(KeyCode.P))
    {
        musicSource.clip = musicClipTwo;
        musicSource.Play();
    }
    if (Input.GetKeyUp(KeyCode.P))
    {
        musicSource.Stop();
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
        musicSource.loop = true;
    }
    if (Input.GetKeyUp(KeyCode.L))
    {
        musicSource.loop = false;
    }
    if (count >= 4)
    {
        musicSource.clip = musicClipTwo;
        musicSource.Play();
    }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.collider.tag == "Ground"){
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

void OnTriggerEnter2D(Collider2D other)
{
         if (other.gameObject.CompareTag("Pick Up"))
         {
             other.gameObject.SetActive(false);
             count++; 
             SetAllText();
         }
 
         else if (other.gameObject.CompareTag("Enemy"))
         {
             other.gameObject.SetActive(false);
             count--; 
             lives--;
             SetAllText();
         }
     }

void SetAllText()
{
    countText.text = "Score: " + count.ToString();
    livesText.text = "Lives: " + lives.ToString();
    if (count >= 4)
    {  
        winText.text = "You Win!";

    }
    else if (lives == 0)
    {
        loseText.text = "You Lose!";
    }
}

}