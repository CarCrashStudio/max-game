using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake ()
    {
        current = this;
    }

    // Combat Events
    public event Action<Enemy> onEnemyKilled;

    // Quest Events
    public event Action<Objective> onObjectiveCompleted;
    public event Action<Quest> onQuestCompleted;

    // Player Data Events
    public event Action<Player> onLevelUp;

    public void EnemyKilled(Enemy enemy)
    {
        onEnemyKilled?.Invoke(enemy);
    }
    public void ObjectiveCompleted(Objective objective)
    {
        onObjectiveCompleted?.Invoke(objective);
    }
    public void QuestCompleted(Quest quest)
    {
        onQuestCompleted?.Invoke(quest);
    }
    public void LevelUp(Player player)
    {
        onLevelUp?.Invoke(player);
    }
}
