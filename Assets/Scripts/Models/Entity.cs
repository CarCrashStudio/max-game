using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum EntityState { WALK, ATTACK, STAGGER, IDLE, INTERACTING }
public class Entity : MonoBehaviour
{
    public EntityState currentState;

    public byte level = 1;
    public  FloatValue currentHealth;
    public Signal healthSignal;

    public  FloatValue currentMana;

    public byte strength = 10,
                dexterity = 10,
                constitution = 10,
                charisma = 10,
                wisdom = 10,
                intelligence = 10;

    protected Rigidbody2D myRigidBody => gameObject.GetComponent<Rigidbody2D>();
    protected Animator animator => gameObject.GetComponent<Animator>();

    public void Knock(float knockTime, float damage)
    {
        currentHealth.initialValue -= damage;
        if (currentHealth.initialValue > 0)
        {
            if (healthSignal != null)
                healthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
    }
    protected IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.isKinematic = true;

            currentState = EntityState.IDLE;
        }
    }
    protected IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = EntityState.ATTACK;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = EntityState.IDLE;
    }
}
