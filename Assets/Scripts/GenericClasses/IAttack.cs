using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//public enum AttackType { NONE, MELEE, RANGED, SPELL }

//public interface IAttack
//{
//    // what goes here ????
//    void Attack(Entity attacker, Entity target);
//}

//public class MeleeAttack : IAttack
//{
//    public void Attack(Entity attacker, Entity target)
//    {
//        var damage = SkillChecks.MakeCheck(Dice.d20, attacker.attributes.totalModifiers.Strength);
//        Debug.Log(damage);
//        target.health.Damage(damage);
//    }
//}
//public class RangedAttack : IAttack
//{
//    public void Attack(Entity attacker, Entity target)
//    {
//        target.health.Damage(attacker.attributes.totalModifiers.Dexterity);
//    }
//}
//public class SpellAttack : IAttack
//{
//    public void Attack(Entity attacker, Entity target)
//    {
//        throw new NotImplementedException();
//    }
//}