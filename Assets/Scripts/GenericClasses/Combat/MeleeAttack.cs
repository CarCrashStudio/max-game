using UnityEngine;

public class MeleeAttack : IAttack
{
    public void Attack(Entity attacker, Weapon weapon, Animator animator, GameObject meleePoint, float meleeRadius, LayerMask enemyLayers)
    {
        animator.SetTrigger("Attack");

        // find enemies
        Collider2D[] hitDefenders = Physics2D.OverlapCircleAll(meleePoint.transform.position, meleeRadius, enemyLayers);

        int proficency = 0;
        //if (attacker.GainedWeaponProficiencies.Contains(weapon.WeaponProficiencySatisfied)) { proficency = attacker.proficiencyBonus; }
        //else { proficency = 0; }

        // loop through enemies
        foreach (Collider2D defender in hitDefenders)
        {
            // make skill check
            if (SkillChecks.MakeCheck(Die.d20, 0) + proficency + attacker.attributes.totalModifiers.Strength >= defender.GetComponent<Entity>().defense)
            {
                // deal damage
                int damage = SkillChecks.MakeCheck(weapon.RolledDie, attacker.attributes.totalModifiers.Strength + proficency);

                defender.GetComponent<Health>().Damage(damage);
            }
        }
    }
    public void Attack(Entity attacker, Weapon weapon)
    {
        throw new System.NotImplementedException();
    }
}