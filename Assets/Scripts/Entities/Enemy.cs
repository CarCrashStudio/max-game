using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ICanWalk
{
    public string enemyName;

    public LootTable lootTable;

    private GameObject player;
    private Rigidbody2D playerRb;

    public bool returnHome;
    private Vector2 startingPoint;
    private bool isHome { get { return myRigidBody.position == startingPoint; } }

    public Vector2 StartingPoint => startingPoint;
    public float WalkRange => throw new System.NotImplementedException();
    public float Speed => throw new System.NotImplementedException();

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

    public void Walk(ref Vector2 velocity)
    {
        throw new System.NotImplementedException();
    }
}
