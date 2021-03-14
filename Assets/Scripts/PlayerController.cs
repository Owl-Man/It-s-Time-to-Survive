using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    public Joystick joystick;
    public GameObject gun;

    private Rigidbody2D rb;
    SpriteRenderer sprite;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator animator;

    //private PostProcessVolume post_process_volume;
    //private Bloom bloom;

    public Image[] Lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    public Image[] Satiety;
    public Sprite fullSatiety;
    public Sprite emptySatiety;

    public int health;
    public int numberOfLives;

    public int satiety;
    public int numberOfSatiety;

    private void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        //post_process_volume = GetComponent<PostProcessVolume>();
        //post_process_volume.profile.TryGetSettings(out bloom);

        gun.SetActive(true);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (moveInput.x == 0)
        {
            animator.SetBool("isRun", false);
            gun.SetActive(true);
        }
        else if (moveInput.x > 0)
        {
            animator.SetBool("isRun", true);
            gun.SetActive(false);
            sprite.flipX = false;
        }
        else if (moveInput.x < 0) 
        {
            animator.SetBool("isRun", true);
            gun.SetActive(false);
            sprite.flipX = true;
        }
    }

    private void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;

//<-----------------------------------------HEALTH-------------------------------------->

        if (health > numberOfLives)
        {
            health = numberOfLives;
        }

        for (int i = 0; i < Lives.Length; i++)
        {
            if (i < health)
            {
                Lives[i].sprite = fullLive;
            }
            else
            {
                Lives[i].sprite = emptyLive;
            }
            if (i < numberOfLives)
            {
                Lives[i].enabled = true;
            }
            else
            {
                Lives[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            //StartCoroutine(DiePlayer());
        }

        if (health < 0) 
        {
            health = 0;
        }
//<------------------------------SATIETY----------------------->

        if (satiety > numberOfSatiety) 
        {
            satiety = numberOfSatiety;
        }

        for (int i = 0; i < Satiety.Length; i++)
        {
            if (i < satiety)
            {
                Satiety[i].sprite = fullSatiety;
            }
            else
            {
                Satiety[i].sprite = emptySatiety;
            }
            if (i < numberOfSatiety)
            {
                Satiety[i].enabled = true;
            }
            else
            {
                Satiety[i].enabled = false;
            }
        }

        if (satiety <= 0) 
        {
            StartCoroutine(SatietyDying());
        }

        if (satiety < 0) 
        {
            satiety = 0;
        }
    }


    IEnumerator SatietyDying()
    {
        yield return new WaitForSeconds(3.5f);
        health--;
    }
}
