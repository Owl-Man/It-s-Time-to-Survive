using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIBase : MonoBehaviour, IEnemyAI
{
    [Header("Values")]
    public int Damage = 1;
    public int health = 1;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    private bool isDying;

    public GameObject EnemyObject;

    public float rayDistance = 1.5f;
    public float stoppingDistance;
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;

    public BoxCollider2D collider;

    public Animator animator;
    public Transform player;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [HideInInspector] public LinkManager links;

    private PlayerController playerController;
    private Indicators indicators;
    [HideInInspector] public Values values;

    [Header("AnimationKeys")]
    public string DeathAnimationKey;
    public string HitAnimationKey;
    public string AttackAnimationKey;
    public string IdleAnimationKey;

    bool chill, angry, goback = false;

    private void Awake()
    {
        links = GameObject.FindGameObjectWithTag("LinkManager").GetComponent<LinkManager>();
        
        player = links.PlayerObject.transform;
        playerController = links.playerController;

        indicators = links.indicators;
        values = links.values;

        Physics2D.queriesStartInColliders = false;
        collider.enabled = true;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            Angrying();
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goback = true;
            angry = false;
        }

        if (PlayerPrefs.GetInt("isRedMoonDay") == 1) Angrying();

        if (chill) Chill();

        else if (angry) Angry();

        else if (goback) GoBack();

        if (health <= 0 && isDying == false) StartCoroutine(Dead());
    }

    private void Angrying()
    {
        angry = true;
        goback = false;
        chill = false;
    }

    private void Chill()
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

    private void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 5;

        SpriteFlipUpdate();
    }

    private void GoBack()
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

    private void OnTriggerStay2D(Collider2D other) //Попытка атаки
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                AttackPlayer();
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public virtual void AttackPlayer() //Нанесение урона
    {
        indicators.health -= Damage;
        indicators.HealthDiagnostic();

        if (AttackAnimationKey != "none") animator.Play(AttackAnimationKey);

        timeBtwAttack = startTimeBtwAttack;
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
        isDying = true;
        animator.Play(DeathAnimationKey);
        collider.enabled = false;
        values.ChangesKillsValue(1);
        ChangeEXPValue();
        yield return new WaitForSeconds(0.8f);
        BeforeDie();
        Destroy(EnemyObject);
    }

    public abstract void BeforeDie();

    public abstract void ChangeEXPValue();
}