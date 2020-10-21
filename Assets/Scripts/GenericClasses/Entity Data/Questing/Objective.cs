using System;
using UnityEngine;

[Serializable]
public class Objective
{
    [SerializeField] protected string description;
    [SerializeField] protected bool isCompleted;
    [SerializeField] protected int currentAmount;
    [SerializeField] protected int requiredAmount;

    public string Description => description;
    public bool IsCompleted => isCompleted;
    public string ObjectiveProgress => $"{currentAmount} / {requiredAmount}";

    public Objective(string description, bool isCompleted, int currentAmount, int requiredAmount)
    {
        this.description = description;
        this.isCompleted = isCompleted;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public void Evaluate ()
    {
        if (isCompleted) { return; }
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }
    protected void Complete()
    {
        isCompleted = true;

        GameEvents.current.ObjectiveCompleted(this);
    }
}
