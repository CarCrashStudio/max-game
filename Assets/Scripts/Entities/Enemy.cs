using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public string enemyName;

    public Health health;
    public LootTable lootTable;

    private GameObject player;
    private Rigidbody2D playerRb;

    //public bool returnHome;
    //private Vector2 home;
    //private bool isHome { get { return myRigidBody.position == home; } }

    // Start is called before the first frame update
    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // ** TODO: **
        // add player discovery/follow code

        base.Update();
    }
}
