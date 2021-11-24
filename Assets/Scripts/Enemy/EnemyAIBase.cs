using System.Collections;
using UnityEngine;

public abstract class EnemyAIBase : MonoCache
{
    [Header("Values")]
    private int health;
    private float timeBtwAttack;

    [SerializeField] private bool isDying;
    [SerializeField] private bool SpriteFlipBool;
    
    private bool movingRight;
    private bool chill, angry, goback;

    [SerializeField] private GameObject EnemyObject;

    [SerializeField] private GameObject AngryEmotion, LoseTargetEmotion;

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float speed = 4;
    [SerializeField] private float angrySpeed = 5;

    [SerializeField] private int positionOfPatrol;

    [SerializeField] private Transform point;

    [Header("Components")]
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer sprite;

    [Header("References")]
    [SerializeField] private Enemy EnemyData;

    [Header("AnimationKeys")]
    [SerializeField] private string DeathAnimationKey;
    [SerializeField] private string HitAnimationKey;
    [SerializeField] private string AttackAnimationKey;
    [SerializeField] private string IdleAnimationKey;

    private void Start()
    {
        player = LinkManager.instance.PlayerObject.transform;

        health = EnemyData.health;

        Physics2D.queriesStartInColliders = false;
        collider.enabled = true;
    }

    public override void OnTick()
    {
        if (isDying == false)
        {
            if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && !angry)
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
        transform.position = Vector2.MoveTowards(transform.position, player.position, angrySpeed * Time.deltaTime);

        SpriteFlipUpdate();
    }

    private void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

        LoseTargetEmotion.SetActive(true);
        AngryEmotion.SetActive(false);

        SpriteFlipUpdate();
    }

    private void SpriteFlipUpdate() => sprite.flipX = movingRight ? SpriteFlipBool : !SpriteFlipBool;

    private void OnTriggerStay2D(Collider2D other) //Атака
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
        Debug.Log(LinkManager.instance.indicators);

        LinkManager.instance.indicators.health -= EnemyData.damage;
        LinkManager.instance.indicators.HealthDiagnostic();

        if (AttackAnimationKey != "") animator.Play(AttackAnimationKey);

        timeBtwAttack = EnemyData.attackSpeed;
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
            health -= LinkManager.instance.playerController.weaponScript.weapon.damage;
        }
    }

    IEnumerator Dead()
    {
        isDying = true;

        if (DeathAnimationKey != "") animator.Play(DeathAnimationKey);

        collider.enabled = false;

        LinkManager.instance.values.ChangesKillsValue(1);

        LinkManager.instance.values.ChangeEXPValue(EnemyData.EXP);

        AngryEmotion.SetActive(false);
        LoseTargetEmotion.SetActive(false);

        LinkManager.instance.bloodCntrl.InstantiateBlood(transform);

        yield return new WaitForSeconds(0.8f);
        BeforeDie();
        Destroy(EnemyObject);
    }

    public virtual void BeforeDie() { }
}
