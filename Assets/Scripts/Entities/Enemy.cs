using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public string enemyName;
    public float awakeRadius;
    public float moveRadius;
    public float attackRadius;

    public LootTable lootTable;

    public float speed = 4f;

    public bool returnHome;

    private GameObject player;
    private Rigidbody2D playerRb;
    private Rigidbody2D rb;

    private Vector2 home;


    private bool isHome { get { return rb.position == home; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();

        home = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        float d = Vector2.Distance(rb.position, playerRb.position);
        if (d <= awakeRadius && d > moveRadius)
            animator.SetBool("sleeping", false);

        else if (d <= moveRadius && d >= attackRadius)
        {
            Vector2 distance = new Vector2(d - attackRadius, d - attackRadius);
            if (!animator.GetBool("moving"))
                animator.SetBool("moving", true);
            returnHome = true;
            rb.position = Vector2.MoveTowards(rb.position, playerRb.position, speed * Time.deltaTime); ;
        }
        else if (d <= attackRadius)
        {
            Debug.Log("Attacking");
            animator.SetBool("moving", false);
            StartCoroutine(AttackCo());
        }
        else
        {
            if (returnHome)
            {
                if (!isHome)
                {
                    if (!animator.GetBool("moving"))
                        animator.SetBool("moving", true);
                    rb.position = Vector2.MoveTowards(rb.position, home, speed * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("moving", false);
                    animator.SetBool("sleeping", true);
                }
            }
            else
                if (!animator.GetBool("sleeping"))
                    animator.SetBool("sleeping", true);

        }
    }
}
