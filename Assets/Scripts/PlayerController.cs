using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    public Joystick joystick;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public InventorySystem inventory;
    
    private WeaponItem weaponScript;
    private Slot slotScript;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public Animator animator;

    public Image attackButtonSprite;

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

    public void BringWeaponState(bool state) 
    {
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
