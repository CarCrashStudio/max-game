// Attribute Script
// Author: Trey Hall
// Description:
// Handles getting and modifying attributes related to the player such as health, mana, and other rpg attributes.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity, IInteracter
{
    public DungeonManager manager;
    public bool currentlyInInteractable = false;
    public float gold = 5f;
    public int Level => level;

    public IInteractable currentInteractable { get; set; }

    public override void Start ()
    {
        base.Start();
    }
    public override void Update()
    {
        #region MOVEMENT
        Vector2 velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        moveEntity(velocity);
        #endregion
        base.Update();
    }


    public void Interact ()
    {
        if (currentInteractable == null) { return; }

        //currentlyInInteractable = (currentState != EntityState.INTERACTING);
        currentInteractable.Interact(this.gameObject);
        //currentState = (!currentlyInInteractable) ? EntityState.IDLE : EntityState.INTERACTING;
    }
    public void LevelUp ()
    {
        level++;
    }
}