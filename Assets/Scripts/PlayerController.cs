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

    public int health;
    public int numberOfLives;

    public Button PickUpButton;
    public bool MayItemPickUp;

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
    }

    //public void OnPickUpButtonClick() 
    //{
    //    MayItemPickUp = true;
    //}

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    PickUpButton.interactable = false;

    //    if (other.CompareTag("Apple") && MayItemPickUp == true)
    //    {
    //        InventorySystem.AddItem("Apple");
    //        MayItemPickUp = false;
    //        Destroy(other.gameObject, 0.1f);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other) 
    //{
    //    PickUpButton.interactable = true;
    //}
}
