using UnityEngine;

public class Quest
{
    bool completed = false;

    [SerializeField] private new string name;
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
}
