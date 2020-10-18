using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private Signal healthSignal;
    private Enemy enemy;

    private PlayerExperience playerExperience;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        playerExperience = FindObjectOfType<PlayerExperience>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void RaiseSignal()
    {
        if (healthSignal != null)
        {
            healthSignal.Raise();
        }
    }

    public override void Damage(float amt)
    {
        base.Damage(amt);
        Kill();
        RaiseSignal();
    }
    public override void Heal(float amt)
    {
        base.Heal(amt);
        RaiseSignal();
    }

    private void Kill()
    {
        if (currentHealth <= 0)
        {
            // give some exp amount to player
            playerExperience.GiveExperience(5f);

            // give a random loot item from the enemy's loot table
            playerInventory.AddInventoryItem(enemy.lootTable.GetLoot());

            Destroy(this.gameObject);
        }
    }
}
