using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject meleePoint;
    [SerializeField] float meleeRadius;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] Inventory inventory;
    Player player;

    [SerializeField] private CooldownManager cooldownManager => FindObjectOfType<CooldownManager>();

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void MainHandAttack()
    {
        if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null && !cooldownManager.IsOnCooldown(((Weapon)inventory.equipment[(int)EquipmentSlotType.MAINHAND]).ID))
        {
            ((Weapon)inventory.equipment[(int)EquipmentSlotType.MAINHAND]).Attack(player, animator, meleePoint, meleeRadius, enemyLayers);
        }
    }
    public void OffHandAttack()
    {
    }

    private void OnDrawGizmosSelected()
    {
        if (!meleePoint) { return; }
        Gizmos.DrawWireSphere(meleePoint.transform.position, meleeRadius);
        Gizmos.DrawLine(meleePoint.transform.parent.position, Input.mousePosition);
    }
}