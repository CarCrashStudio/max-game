// Attribute Script
// Author: Trey Hall
// Description:
// Handles getting and modifying attributes related to the player such as health, mana, and other rpg attributes.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public DungeonManager manager;
    public bool currentlyInInteractable = false;
    public float gold = 5f;
    public int Level => level;

    private Interactable nearestInteractable
    {
        get
        {
            List<Interactable> temp = new List<Interactable>();
            temp.AddRange(FindObjectsOfType<Interactable>());
            temp = temp.Where(t => t.entered).ToList();

            if (temp.Count > 0)
                return temp[0];
            else return null;
        }
    }

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

        #region INTERACTION
        if (Input.GetButtonDown("interact") && (currentState == EntityState.IDLE || currentState == EntityState.WALK || currentState == EntityState.INTERACTING))
        {
            var n = nearestInteractable;
            if (n != null)
            {
                currentlyInInteractable = (currentState != EntityState.INTERACTING);
                n.Interact(gameObject);
                currentState = (!currentlyInInteractable) ? EntityState.IDLE : EntityState.INTERACTING;
            }
        }
        #endregion
        base.Update();
    }

    public void LevelUp ()
    {
        level++;
    }
}