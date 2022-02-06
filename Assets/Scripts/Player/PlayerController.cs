using System.Collections;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header ("Values")]
    
        [SerializeField] private float speed, reloadAttacking;
        private int _attackCombo = 1;
        private bool _isAttacking;
        private Vector2 _moveInput, _moveVelocity;

        public GameObject attackHitBox;

        [HideInInspector] public WeaponItem weaponScript;
        private Slot _slotScript;

        public Image attackButtonSprite;

        [Header("References")]

        [SerializeField] private Animator animator;
        [SerializeField] private Joystick joystick;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private InventorySystem inventory;
        [SerializeField] private BoxCollider2D rightHit, leftHit;
        
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsSwordEquip = Animator.StringToHash("isSwordEquip");
        private static readonly int IsBowEquip = Animator.StringToHash("isBowEquip");

        private void Start() => Time.timeScale = 1f;

        private void Update()
        {
            if (_moveInput.x == 0)
            {
                animator.SetBool(IsRun, false);
            }
            else if (_moveInput.x > 0)
            {
                animator.SetBool(IsRun, true);
                sprite.flipX = false;

                rightHit.enabled = true;
                leftHit.enabled = false;
            }
            else if (_moveInput.x < 0)
            {
                animator.SetBool(IsRun, true);
                sprite.flipX = true;

                rightHit.enabled = false;
                leftHit.enabled = true;
            }
        }

        private void FixedUpdate()
        {
            _moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            _moveVelocity = _moveInput.normalized * speed;

            rb.MovePosition(rb.position + _moveVelocity * Time.fixedDeltaTime);
        }

        public void OnAttackButtonClick()
        {
            if (animator.GetBool(IsDead)) return;

            if (_isAttacking) return;
        
            _isAttacking = true;    

            StartCoroutine(Attacking());
        }

        private IEnumerator Attacking() 
        {
            if (_attackCombo <= 5) animator.Play("SwordAttack" + _attackCombo);
            else _attackCombo = 1;

            attackHitBox.SetActive(true);
            yield return new WaitForSeconds(reloadAttacking);
            _attackCombo++;
            animator.Play("Swordidle");
            attackHitBox.SetActive(false);
            _isAttacking = false;
        }

        public void BringWeaponState(bool state) 
        {
            if (animator.GetBool(IsDead)) return;
        
            if (state) 
            {
                inventory.AttackButton.SetActive(true);
                _slotScript = inventory.slotScripts[inventory.idSlotThatUsed];
                _slotScript.GetChild();
                weaponScript = _slotScript.Child.GetComponent<WeaponItem>();

                attackButtonSprite.sprite = weaponScript.weapon.sprite;
            }
            else 
            {
                inventory.AttackButton.SetActive(false);

                animator.SetBool(IsSwordEquip, false);
                animator.SetBool(IsBowEquip, false);

                return;
            }

            if (_slotScript.Child.CompareTag("Weapon"))
            {
                animator.SetBool(IsSwordEquip, state);
                animator.SetBool(IsBowEquip, !state);
            }

            if (_slotScript.Child.CompareTag("Bow"))
            {
                animator.SetBool(IsBowEquip, state);
                animator.SetBool(IsSwordEquip, !state);
            }
        }

    }
}
