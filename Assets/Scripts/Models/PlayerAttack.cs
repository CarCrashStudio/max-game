using UnityEngine;

public class PlayerAttack : GenericAttack
{
    /* TODO
     * Weapons should identify the type of attack they are using
     * Weapons should be given an array of the proficiencies required by the user to wield them
     */
    [SerializeField] Inventory inventory;
    Player player;

    [SerializeField] private CooldownManager cooldownManager => FindObjectOfType<CooldownManager>();

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void MainHandAttack()
    {
        if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null && !cooldownManager.IsOnCooldown(inventory.equipment[(int)EquipmentSlotType.MAINHAND].ID))
        {
            base.Attack(player);

            // apply cooldown
            inventory.equipment[(int)EquipmentSlotType.MAINHAND].Use();

            Debug.Log("Main Hand Attack!");
        }
    }
    public void OffHandAttack()
    {
        //if (inventory.equipment[(int)EquipmentSlotType.OFFHAND] != null && !cooldownManager.IsOnCooldown(inventory.equipment[(int)EquipmentSlotType.OFFHAND].ID))
        //{
        //    base.Attack(player);

        //    inventory.equipment[(int)EquipmentSlotType.OFFHAND].Use();
        //    Debug.Log("Off Hand Attack!");
        //}
        base.RangedAttack();
    }
}