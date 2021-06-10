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
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public GameObject attackHitBox;

    [HideInInspector] public WeaponItem weaponScript;

    private Slot slotScript;

    public Image attackButtonSprite;

    [Header("Other")]

    public Animator animator;
    public Joystick joystick;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public InventorySystem inventory;

    private void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (moveInput.x == 0)
        {
            animator.SetBool("isRun", false);
        }
        else if (moveInput.x > 0)
        {
            animator.SetBool("isRun", true);
            sprite.flipX = false;
        }
        else if (moveInput.x < 0) 
        {
            animator.SetBool("isRun", true);
            sprite.flipX = true;
        }
    }

    public void OnAttackButtonClick()
    {
        if (animator.GetBool("isDead") == true) return;

        if (isAttacking == true) return;
        else isAttacking = true;

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
        if (animator.GetBool("isDead") == true) return;
        
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
