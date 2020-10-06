using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Inventory inventory;

    [SerializeField] private CooldownManager cooldownManager => FindObjectOfType<CooldownManager>();

    public void MainHandAttack()
    {
        if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null && !cooldownManager.IsOnCooldown(inventory.equipment[(int)EquipmentSlotType.MAINHAND].ID))
        {
            animator.SetTrigger("Attack");

            inventory.equipment[(int)EquipmentSlotType.MAINHAND].Use();

            Debug.Log("Main Hand Attack!");
        }
    }
    public void OffHandAttack()
    {
        if (inventory.equipment[(int)EquipmentSlotType.OFFHAND] != null && !cooldownManager.IsOnCooldown(inventory.equipment[(int)EquipmentSlotType.OFFHAND].ID))
        {
            animator.SetTrigger("Attack");
            inventory.equipment[(int)EquipmentSlotType.OFFHAND].Use();
            Debug.Log("Off Hand Attack!");
        }
    }
}