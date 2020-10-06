using UnityEditorInternal;
using UnityEngine;

public class PlayerExperience : Experience
{
    [SerializeField] private Signal experienceSignal;
    private void RaiseSignal()
    {
        if (experienceSignal != null)
        {
            experienceSignal.Raise();
        }
    }

    public override void GiveExperience(float amt)
    {
       base.GiveExperience(amt);
        
        if (currentExp.runtimeValue >= maxExp.runtimeValue)
        {
            FindObjectOfType<Player>().LevelUp();
            var level = FindObjectOfType<Player>().Level;
            maxExp.runtimeValue = Mathf.Pow(level, levelLength) * maxExp.initialValue;
        }
        RaiseSignal();
    }
}
