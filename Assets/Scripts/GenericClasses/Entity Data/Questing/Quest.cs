using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class Quest
{
    private bool completed = false;

    [SerializeField] private string name;
    [SerializeField] private string description;

    [SerializeField] private LootTable rewardTable;
    [SerializeField] private InventoryItem rewardItem;
    [SerializeField] private float rewardExp;

    [SerializeField] private Objective[] objectives;

    public Quest(string name, string description, LootTable rewardTable = null, float rewardExp = 0f)
    {
        this.name = name;
        this.description = description;
        this.rewardTable = rewardTable;
        this.rewardExp = rewardExp;

        GameEvents.onObjectiveCompleted += onObjectiveCompleted;

        if (rewardTable == null) { return; }
        rewardItem = rewardTable.GetLoot();
    }
    [JsonConstructor]
    public Quest(string name, string description, LootTable rewardTable = null, float rewardExp = 0f, params Objective[] objectives)
    {
        this.name = name;
        this.description = description;
        this.rewardTable = rewardTable;
        this.rewardExp = rewardExp;

        this.objectives = objectives;

        GameEvents.onObjectiveCompleted += onObjectiveCompleted;

        if (rewardTable == null) { return; }
        rewardItem = rewardTable.GetLoot();
    }

    public bool Completed => completed;

    public string Name => name; 
    public string Description => description; 

    public LootTable RewardTable => rewardTable; 
    public InventoryItem RewardItem => rewardItem;
    public float RewardExp => rewardExp;

    public Objective[] Objectives => objectives;

    private void onObjectiveCompleted (Objective objective)
    {
        if (!objectives.Contains(objective)) { return; }
        if (objectives.All(o => o.IsCompleted))
        {
            Complete();
        }
    }

    private void Complete ()
    {
        completed = true;
        GameEvents.QuestCompleted(this);
    }
}
