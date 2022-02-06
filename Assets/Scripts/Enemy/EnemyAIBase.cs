using System.Collections;
using Instruments;
using MonoCacheSystem;
using UnityEngine;

namespace EnemySystem
{
    public abstract class EnemyAIBase : MonoCache
    {
        [Header("Values")]
        private int _health;
        private float _timeAttackReloading;

        [SerializeField] private bool isDying, SpriteFlipBool;
    
        private bool _movingRight;
        private bool _chill, _angry, _goBack;

        [SerializeField] private GameObject EnemyObject;

        [SerializeField] private GameObject AngryEmotion, LoseTargetEmotion;

        [SerializeField] private float stoppingDistance;
        [SerializeField] private float speed = 4;
        [SerializeField] private float angrySpeed = 5;

        [SerializeField] private int positionOfPatrol;

        [SerializeField] private Transform point;

        private Vector3 _enemyPosition, _playerPosition;

        [Header("Components")]
        [SerializeField] private BoxCollider2D collider;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer sprite;

        private LinkManager _link;

        [Header("References")]
        [SerializeField] private Enemy EnemyData;

        [Header("AnimationKeys")]
        [SerializeField] private string DeathAnimationKey;
        [SerializeField] private string HitAnimationKey;
        [SerializeField] private string AttackAnimationKey;
        [SerializeField] private string IdleAnimationKey;

        private void Start()
        {
            _enemyPosition = transform.position;
            _link = LinkManager.Instance;
            _playerPosition = _link.playerObject.transform.position;

            _health = EnemyData.health;

            Physics2D.queriesStartInColliders = false;
            collider.enabled = true;
        }

        public override void OnTick()
        {
            if (isDying == false)
            {
                if (Vector2.Distance(_enemyPosition, point.position) < positionOfPatrol && !_angry)
                {
                    _chill = true;
                }
                if (Vector2.Distance(_enemyPosition, _playerPosition) < stoppingDistance)
                {
                    GetAngry();
                }
                if (Vector2.Distance(_enemyPosition, _playerPosition) > stoppingDistance)
                {
                    _goBack = true;
                    _angry = false;
                }

                if (_chill) Chill();
                else if (_angry) Angry();
                else if (_goBack) GoBack();

                if (_health <= 0) StartCoroutine(Dead());
            }
        }

        private void GetAngry()
        {
            _angry = true;
            _goBack = false;
            _chill = false;

            AngryEmotion.SetActive(true);
            LoseTargetEmotion.SetActive(false);
        }

        private void Chill()
        {
            LoseTargetEmotion.SetActive(false);
            AngryEmotion.SetActive(false);

            if (_enemyPosition.x > point.position.x + positionOfPatrol)
            {
                _movingRight = false;
            }
            else if (_enemyPosition.x < point.position.x - positionOfPatrol)
            {
                _movingRight = true;
            }
            if (_movingRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                sprite.flipX = SpriteFlipBool;
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                sprite.flipX = !SpriteFlipBool;
            }

            _enemyPosition = transform.position;
        }

        private void Angry()
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPosition, angrySpeed * Time.deltaTime);
            _enemyPosition = transform.position;
        
            SpriteFlipUpdate();
        }

        private void GoBack()
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
            _enemyPosition = transform.position;
        
            LoseTargetEmotion.SetActive(true);
            AngryEmotion.SetActive(false);

            SpriteFlipUpdate();
        }

        private void SpriteFlipUpdate() => sprite.flipX = _movingRight ? SpriteFlipBool : !SpriteFlipBool;

        private void OnTriggerStay2D(Collider2D other) //Атака
        {
            if (other.CompareTag("Player"))
            {
                if (_timeAttackReloading <= 0)
                {
                    AttackPlayer();
                }
                else
                {
                    _timeAttackReloading -= Time.deltaTime;
                }
            }
        }

        public virtual void AttackPlayer() //Нанесение урона
        {
            _link.indicators.health -= EnemyData.damage;
            _link.indicators.HealthDiagnostic();

            if (AttackAnimationKey != "") animator.Play(AttackAnimationKey);

            _timeAttackReloading = EnemyData.attackSpeed;
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
                _health -= _link.playerController.weaponScript.weapon.damage;
            }
        }

        private IEnumerator Dead()
        {
            isDying = true;

            if (DeathAnimationKey != "") animator.Play(DeathAnimationKey);

            collider.enabled = false;

            _link.values.ChangesKillsValue(1);

            _link.values.ChangeEXPValue(EnemyData.EXP);

            AngryEmotion.SetActive(false);
            LoseTargetEmotion.SetActive(false);

            _link.bloodCntrl.InstantiateBlood(transform);

            yield return new WaitForSeconds(0.8f);
            BeforeDie();
            Destroy(EnemyObject);
        }

        public virtual void BeforeDie() { }
    }
}
