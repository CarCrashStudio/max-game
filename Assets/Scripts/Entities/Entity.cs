// Entity Type
// Author: Trey Hall
// Description:
// The base class for Player and Enemy types

using UnityEngine;
using UnityEngine.UI;

public enum EntityState { WALK, ATTACK, STAGGER, IDLE, INTERACTING }
public class Entity : MonoBehaviour
{
    const int EQUIPMENT_SIZE = 6;

    [SerializeField] protected byte level = 1;
    public float speed = 4f;

    public EntityState currentState;

    public Attributes attributes;

    public bool charOpen = false;
    public Equipment[] equipment = new Equipment[EQUIPMENT_SIZE];
    public GameObject characterUI;
    
    public void Equip(Equipment e)
    {
        equipment[(int)e.slot] = e;
    }
    public void Unequip(Equipment e)
    {
        var slot = characterUI.transform.GetChild(1).GetChild((int)e.slot);
        slot.GetChild(0).GetComponent<Image>().sprite = null;
        equipment[(int)e.slot] = null;
    }

    [SerializeField] protected Rigidbody2D myRigidBody => gameObject.GetComponent<Rigidbody2D>();
    [SerializeField] protected Animator animator => gameObject.GetComponent<Animator>();

    public virtual void Start()
    {
        attributes.Start();
    }
    public virtual void Update ()
    {
        // apply any modifier buffs from equipment
        var totalAdditionalModifiers = new Modifier();
        for (int i = 0; i < EQUIPMENT_SIZE; i++)
        {
            if (equipment[i] != null) //&& equipment[i].modifier != null)
            {
                // get the modifier of the equipment
                totalAdditionalModifiers += equipment[i].modifier;
            }
        }

        //Debug.Log(totalAdditionalModifiers.ToString());
        attributes.equipmentModifiers = totalAdditionalModifiers;

        // Update the attributes of this entity
        attributes.Update();
    }

    protected void moveEntity (Vector2 velocity)
    {
        if ((currentState == EntityState.WALK || currentState == EntityState.IDLE) && velocity != Vector2.zero)
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
}

