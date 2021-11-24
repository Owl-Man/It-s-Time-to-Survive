using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header ("Values")]
    
    public float speed;
    public float reloadAttacking;
    private int AttackCombo = 1;
    private bool isAttacking;
    
    private Vector2 moveInput, moveVelocity;

    public GameObject attackHitBox;

    [HideInInspector] public WeaponItem weaponScript;

    private Slot slotScript;

    public Image attackButtonSprite;

    [Header("References")]

    public Animator animator;
    public Joystick joystick;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public InventorySystem inventory;

    public BoxCollider2D rightHit, leftHit;

    private void Start() => Time.timeScale = 1f;

    private void FixedUpdate()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (moveInput.x == 0)
        {
            animator.SetBool("isRun", false);
        }
        else if (moveInput.x > 0)
        {
            animator.SetBool("isRun", true);
            sprite.flipX = false;

            rightHit.enabled = true;
            leftHit.enabled = false;
        }
        else if (moveInput.x < 0) 
        {
            animator.SetBool("isRun", true);
            sprite.flipX = true;

            rightHit.enabled = false;
            leftHit.enabled = true;
        }
    }

    public void OnAttackButtonClick()
    {
        if (animator.GetBool("isDead")) return;

        if (isAttacking) return;
        
        isAttacking = true;    

        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking() 
    {
        if (AttackCombo <= 5) animator.Play("SwordAttack" + AttackCombo);
        else AttackCombo = 1;

        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(reloadAttacking);
        AttackCombo++;
        animator.Play("Swordidle");
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    public void BringWeaponState(bool state) 
    {
        if (animator.GetBool("isDead")) return;
        
        if (state == true) 
        {
            inventory.AttackButton.SetActive(true);
            slotScript = inventory.slotScripts[PlayerPrefs.GetInt("IdSlotThatUsed")];
            slotScript.GetChild();
            weaponScript = slotScript.Child.GetComponent<WeaponItem>();

            attackButtonSprite.sprite = weaponScript.weapon.sprite;
        }
        else 
        {
            inventory.AttackButton.SetActive(false);

            animator.SetBool("isSwordEquip", false);
            animator.SetBool("isBowEquip", false);

            return;
        }

        if (slotScript.Child.CompareTag("Weapon"))
        {
            animator.SetBool("isSwordEquip", state);
            animator.SetBool("isBowEquip", !state);
        }

        if (slotScript.Child.CompareTag("Bow"))
        {
            animator.SetBool("isBowEquip", state);
            animator.SetBool("isSwordEquip", !state);
        }
    }

}
