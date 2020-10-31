using UnityEngine;

public class PlayerHealth : Health
{
    public override void Damage(float amt)
    {
        base.Damage(amt);
        RaiseSignal();
        GameEvents.ChangesMade();
    }
    public override void Heal(float amt)
    {
        base.Heal(amt);
        RaiseSignal();
        GameEvents.ChangesMade();
    }
}
