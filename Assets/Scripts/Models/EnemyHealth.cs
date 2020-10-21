using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private Signal healthSignal;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
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
            GameEvents.current.EnemyKilled(enemy);
            Destroy(this.gameObject);
        }
    }
}
