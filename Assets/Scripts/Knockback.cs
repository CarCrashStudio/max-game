using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);

        if (other.gameObject.CompareTag("breakable"))
        {
            //other.GetComponent<Breakable>().Smash();
        }
        Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
        if (hit != null)
        {
            hit.isKinematic = false;
            Vector2 diff = hit.transform.position - transform.position;
            diff = diff.normalized * thrust;
            hit.AddForce(diff, ForceMode2D.Impulse); 
        }
        if (other.gameObject.CompareTag("enemy") && other.isTrigger)
        {
            {
                hit.GetComponent<Enemy>().currentState = EntityState.STAGGER;
                other.GetComponent<Enemy>().Knock(knockTime, damage);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (hit.GetComponent<Enemy>().currentState != EntityState.STAGGER)
            {
                hit.GetComponent<MaxAttributes>().currentState = EntityState.STAGGER;
                other.GetComponent<MaxAttributes>().Knock(knockTime, damage);
            }
        }
    }
}
