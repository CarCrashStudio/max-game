public enum Die { d4 = 4, d6 = 6, d8 = 8, d10 = 10, d12 = 12, d20 = 20, d100 = 100 };
public class SkillChecks
{
    public static int MakeCheck (Die rolledDie, int modifier)
    {
        var max = (int)rolledDie + 1;
        var roll = UnityEngine.Random.Range(1, max);
        //UnityEngine.Debug.Log($"Roll: {roll} Modifier: {modifier}");
        return roll + modifier;
    }
}
