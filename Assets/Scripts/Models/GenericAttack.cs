using System.Collections;
using Unity.Collections;
using UnityEngine;

public class GenericAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject meleePoint;
    [SerializeField] float meleeRadius;
    [SerializeField] LayerMask enemyLayers;

    protected void Attack(Entity attacker)
    {
        animator.SetTrigger("Attack");

        // find enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.transform.position, meleeRadius, enemyLayers);

        // loop through enemies
        foreach (Collider2D defender in hitEnemies)
        {
            // make skill check
            if (SkillChecks.MakeCheck(Dice.d12, 0) >= defender.GetComponent<Entity>().defense)
            {
                // deal damage
                int damage = SkillChecks.MakeCheck(Dice.d12, attacker.attributes.totalModifiers.Strength);

                defender.GetComponent<Health>().Damage(damage);
            }
        }
    }

    protected void RangedAttack()
    {
        // play range attack animation
        /* TODO: make range attack animation */

        // create a raycast path for the projectile
        var ray = Physics2D.Raycast(meleePoint.transform.position, Input.mousePosition);
        float speed = 10f;
        // create the projectile, moving along ray path
        var projectile = Instantiate(Resources.Load<GameObject>("Items/ArrowProjectile"), meleePoint.transform.position, meleePoint.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = ray.normal * speed;

        // as the projectile follows path, check if it hits an enemy collider
        //StartCoroutine(ProjectileCo(projectile, ray));
    }

    IEnumerator ProjectileCo(GameObject projectile, RaycastHit2D ray)
    {
        Debug.Log("Start ProjectileCo");
        bool enemyHit = false;
        int i = 10;
        do
        {
            // check for collisions in enemy layers
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(projectile.transform.position, meleeRadius, enemyLayers);

            // if collide, deal damage and destroy projectile
            projectile.transform.position += new Vector3(ray.normal.x, ray.normal.y, 0);

            // else
            // move the projectile along raycast
            i--;
        } while (i > -1);
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (!meleePoint) { return; }
        Gizmos.DrawWireSphere(meleePoint.transform.position, meleeRadius);
        Gizmos.DrawLine(meleePoint.transform.parent.position, Input.mousePosition);
    }
}