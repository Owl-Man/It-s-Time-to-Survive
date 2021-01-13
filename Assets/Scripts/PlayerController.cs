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

    public Image[] Lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    public int health;
    public int numberOfLives;
    
    private void Awake() 
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
    }
}
