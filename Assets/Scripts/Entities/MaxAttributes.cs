// Attribute Script
// Author: Trey Hall
// Description:
// Handles getting and modifying attributes related to the player such as health, mana, and other rpg attributes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlots { HEAD, TORSO, LEGS, BOOTS, ARMS, WEAPON_MAIN, WEAPON_OFF };
public class MaxAttributes : Entity
{
    public float speed = 16f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");


        if (Input.GetButtonDown("attack") && currentState != EntityState.ATTACK)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("interact") && (currentState == EntityState.IDLE || currentState == EntityState.WALK))
        {

        }
        else if ((currentState == EntityState.WALK || currentState == EntityState.IDLE) && velocity != Vector2.zero)
        {
            myRigidBody.position += velocity * speed * Time.deltaTime;

            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    

    public override string ToString()
    {
        return $"Health: {currentHealth.initialValue}\nMana: {currentMana.initialValue}\nStrength: {strength}\nDexterity: {dexterity}\nConstitution: {constitution}\nCharisma: {charisma}\nWisdom: {wisdom}\nIntelligence: {intelligence}";
    }
}

