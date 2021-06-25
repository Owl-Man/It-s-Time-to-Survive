using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIBase : MonoBehaviour
{
    [Header("Values")]

    public int damage = 1;
    public int health = 1;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float GiveEXP;

    [SerializeField] public bool isDying;
    public bool SpriteFlipBool;

    public GameObject EnemyObject;

    public GameObject AngryEmotion;
    public GameObject LoseTargetEmotion;

    public float rayDistance = 1.5f;
    public float stoppingDistance;
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool movingRight;

    [Header("Components")]

    public BoxCollider2D collider;

    public Animator animator;
    public Transform player;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    [Header("References")]

    public LinkManager links;

    private PlayerController playerController;
    private Indicators indicators;
    private BloodScript bloodCntrl;

    public Enemy EnemyData;

    [HideInInspector] public Values values;

    [Header("AnimationKeys")]

    public string DeathAnimationKey;
    public string HitAnimationKey;
    public string AttackAnimationKey;
    public string IdleAnimationKey;

    bool chill, angry, goback = false;

    private void Awake()
    {
        if (links == null) links = GameObject.FindGameObjectWithTag("LinkManager").GetComponent<LinkManager>();

        player = links.PlayerObject.transform;
        playerController = links.playerController;

        indicators = links.indicators;
        values = links.values;

        bloodCntrl = links.bloodCntrl;

        damage = EnemyData.damage;
        health = EnemyData.health;
        startTimeBtwAttack = EnemyData.attackSpeed;
        GiveEXP = EnemyData.EXP;

        Physics2D.queriesStartInColliders = false;
        collider.enabled = true;
    }

    private void Update()
    {
        if (isDying == false) 
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
    }

    private void Angrying()
    {
        angry = true;
        goback = false;
        chill = false;

        AngryEmotion.SetActive(true);
        LoseTargetEmotion.SetActive(false);
    }

    private void Chill()
    {
        LoseTargetEmotion.SetActive(false);
        AngryEmotion.SetActive(false);

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
            sprite.flipX = SpriteFlipBool;
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            sprite.flipX = !SpriteFlipBool;
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

        LoseTargetEmotion.SetActive(true);
        AngryEmotion.SetActive(false);

        SpriteFlipUpdate();
    }

    private void SpriteFlipUpdate()
    {
        if (movingRight) sprite.flipX = SpriteFlipBool;

        else sprite.flipX = !SpriteFlipBool;
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
        indicators.health -= damage;
        indicators.HealthDiagnostic();

        if (AttackAnimationKey != "") animator.Play(AttackAnimationKey);

        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AttackAnimationKey != "") animator.Play(IdleAnimationKey);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Получение урона
    {
        if (other.CompareTag("WeaponHitBox"))
        {
            if (HitAnimationKey != "") animator.Play(HitAnimationKey);
            health -= playerController.weaponScript.weapon.damage;
        }
    }

    IEnumerator Dead()
    {
        isDying = true;

        if (DeathAnimationKey != "") animator.Play(DeathAnimationKey);

        collider.enabled = false;

        values.ChangesKillsValue(1);

        values.ChangeEXPValue(GiveEXP);

        AngryEmotion.SetActive(false);
        LoseTargetEmotion.SetActive(false);

        if (bloodCntrl != null) bloodCntrl.InstantiateBlood(transform);

        yield return new WaitForSeconds(0.8f);
        BeforeDie();
        Destroy(EnemyObject);
    }
    
    public virtual void BeforeDie() { }
}