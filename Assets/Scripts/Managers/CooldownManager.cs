using UnityEngine;
using System.Collections.Generic;

public class CooldownManager : MonoBehaviour
{
    private readonly List<CooldownData> cooldowns = new List<CooldownData>();

    void Update() => ProcessCooldowns();
    public void PutOnCooldown (IHasCooldown cooldown)
    {
        cooldowns.Add(new CooldownData(cooldown));
    }
    public bool IsOnCooldown (int id)
    {
        foreach(CooldownData cooldown in cooldowns)
        {
            if (cooldown.ID == id) { return true; }
        }
        return false;
    }
    public float GetTimeRemaining(int id)
    {
        foreach (CooldownData cooldown in cooldowns)
        {
            if (cooldown.ID != id) { continue; }
            return cooldown.TimeRemaining;
        }
        return 0f;
    }

    private void ProcessCooldowns()
    {
        float deltaTime = Time.deltaTime;
        for (int i = cooldowns.Count - 1; i >= 0; i--)
        {
            if (cooldowns[i].DecrementCooldown(deltaTime))
            {
                cooldowns.RemoveAt(i);
            }
        }
    }
}

public class CooldownData
{
    public int ID { get; }
    public float TimeRemaining { get; private set; }
    public CooldownData (IHasCooldown cooldown)
    {
        ID = cooldown.ID;
        TimeRemaining = cooldown.CooldownTime;
    }

    public bool DecrementCooldown(float deltaTime)
    {
        TimeRemaining = Mathf.Max(TimeRemaining - deltaTime, 0);
        return (TimeRemaining == 0);
    }
}
