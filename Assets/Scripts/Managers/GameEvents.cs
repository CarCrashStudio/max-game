using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    // Combat Events
    public static event Action<Enemy> onEnemyKilled;

    // Quest Events
    public static event Action<Objective> onObjectiveCompleted;
    public static event Action<Quest> onQuestCompleted;

    // Player Data Events
    public static event Action<Player> onLevelUp;
    public static event Action<Player> onInteractableEnter;
    public static event Action<Player> onInteractableExit;

    // Game Events
    public static event Action onChangesMade;
    public static event Action onMainMenuGameLoad;
    public static event Action onMainMenuGameNew;
    public static event Action onPause;
    public static event Action onResume;

    public static void EnemyKilled(Enemy enemy) => onEnemyKilled?.Invoke(enemy);
    public static void ObjectiveCompleted(Objective objective) => onObjectiveCompleted?.Invoke(objective);
    public static void QuestCompleted(Quest quest) => onQuestCompleted?.Invoke(quest);
    public static void LevelUp(Player player) => onLevelUp?.Invoke(player);
    public static void InteractableEnter(Player player) => onInteractableEnter?.Invoke(player);
    public static void InteractableExit(Player player) => onInteractableExit?.Invoke(player);
    public static void ChangesMade() => onChangesMade?.Invoke();
    public static void MainMenuGameLoad() => onMainMenuGameLoad?.Invoke();
    public static void MainMenuGameNew() => onMainMenuGameNew?.Invoke();
    public static void Pause() => onPause?.Invoke();
    public static void Resume() => onResume?.Invoke();
}
