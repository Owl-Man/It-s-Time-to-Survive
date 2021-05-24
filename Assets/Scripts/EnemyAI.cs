using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : LinkManager
{
    public int Damage = 1;
    public int health = 1;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float rayDistance = 1.5f;
    public float stoppingDistance;
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;

    public BoxCollider2D collider;
    public GameObject Dron;
    Animator animator;
    Transform player;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    PlayerController playerController;
    Indicators indicators;

    bool chill, angry, goback = false;
    public string DeathAnimationKey;
    public string HitAnimationKey;
    public string AttackAnimationKey;
    public string IdleAnimationKey;

    void Start()
    {
        player = ManagerPlayerObject.transform;
        playerController = ManagerPlayerCntrl;
        indicators = ManagerIndicators;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        Physics2D.queriesStartInColliders = false;
        collider.enabled = true;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goback = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goback = true;
            angry = false;
        }

        if (chill) Chill();

        else if (angry) Angry();

        else if (goback) GoBack();

        if (health <= 0) StartCoroutine(Dead());
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        } 
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            sprite.flipX = false;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            sprite.flipX = true;
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 5;
        
        SpriteFlipUpdate();
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = 4;

        SpriteFlipUpdate();
    }

    private void SpriteFlipUpdate() 
    {
        if (movingRight) sprite.flipX = false;

        else sprite.flipX = true;
    }
    private void OnTriggerStay2D(Collider2D other) //Атака
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                indicators.health -= Damage;

                if (AttackAnimationKey != "none") animator.Play(AttackAnimationKey);

                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (AttackAnimationKey != "none") animator.Play(IdleAnimationKey);
        }    
    }
    private void OnTriggerEnter2D(Collider2D other) //Получение урона
    {
        if (other.CompareTag("WeaponHitBox")) 
        {
            animator.Play(HitAnimationKey);
            health -= playerController.weaponScript.weapon.damage;
        } 
    }
    IEnumerator Dead()
    {
        animator.Play(DeathAnimationKey);
        collider.enabled = false;
        yield return new WaitForSeconds(0.8f);
        Dron.SetActive(false);
    }
}