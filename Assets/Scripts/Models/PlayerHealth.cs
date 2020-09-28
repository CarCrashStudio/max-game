using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private Signal healthSignal;
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
        RaiseSignal();
    }
    public override void Heal(float amt)
    {
        base.Heal(amt);
        RaiseSignal();
    }
}
