using UnityEditorInternal;
using UnityEngine;

public class PlayerExperience : Experience
{
    void Awake ()
    {
        GameEvents.onEnemyKilled += OnEnemyKilled;
        GameEvents.onQuestCompleted += OnQuestCompleted;
    }

    public override void GiveExperience(float amt)
    {
       base.GiveExperience(amt);
        
        if (currentExp.runtimeValue >= maxExp.runtimeValue)
        {
            GameEvents.LevelUp(FindObjectOfType<Player>());
            maxExp.runtimeValue = Mathf.Pow(FindObjectOfType<Player>().Level, levelLength) * maxExp.initialValue;
        }
        RaiseSignal();
        GameEvents.ChangesMade();
    }

    private void OnEnemyKilled (Enemy Enemy)
    {
        // give some exp amount to player
        GiveExperience(5f);
    }
    private void OnQuestCompleted(Quest quest)
    {
        // give some exp amount to player
        GiveExperience(quest.RewardExp);
    }
}
