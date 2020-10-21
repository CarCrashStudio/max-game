using UnityEditorInternal;
using UnityEngine;

public class PlayerExperience : Experience
{
    [SerializeField] private Signal experienceSignal;
    void Awake ()
    {
        GameEvents.current.onEnemyKilled += OnEnemyKilled;
        GameEvents.current.onQuestCompleted += OnQuestCompleted;
    }
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
            GameEvents.current.LevelUp(FindObjectOfType<Player>());
            maxExp.runtimeValue = Mathf.Pow(FindObjectOfType<Player>().Level, levelLength) * maxExp.initialValue;
        }
        RaiseSignal();
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
