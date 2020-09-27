using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Dice { d4 = 4, d6 = 6, d8 = 8, d10 = 10, d12 = 12, d20 = 20, d100 = 100 };
public class SkillChecks
{
    public static int MakeCheck (Dice rolledDice, int modifier)
    {
        var max = (int)rolledDice + 1;
        var roll = UnityEngine.Random.Range(1, max);
        UnityEngine.Debug.Log(roll);
        return roll + modifier;
    }
}
