using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Experience : MonoBehaviour
{
    // TODO: MOVE LEVEL INTO EXPERIENCE

    [SerializeField] protected float levelLength = 2f;
    [SerializeField] protected float levelDifficulty = 5f;

    [SerializeField] protected FloatValue maxExp;
    [SerializeField] protected FloatValue currentExp;

    [SerializeField] protected Signal experienceSignal;
    protected void RaiseSignal()
    {
        if (experienceSignal != null)
        {
            experienceSignal.Raise();
        }
    }

    public FloatValue MaxExp => maxExp;
    public FloatValue CurrentExp => currentExp;

    public void Start()
    {
        currentExp.runtimeValue = 0;
    }

    public override string ToString()
    {
        return string.Concat(currentExp.ToString(), "/", maxExp.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amt"></param>
    public virtual void GiveExperience(float amt)
    {
        currentExp.runtimeValue += amt;
        //if (currentExp.runtimeValue > maxExp.initialValue)
        //currentExp.runtimeValue = maxExp.initialValue;
    }
    public virtual void FullExperience()
    {
        currentExp.runtimeValue = maxExp.runtimeValue;
    }

    public void Load(float currentExp, float maxExp)
    {
        this.currentExp.runtimeValue = currentExp;
        this.maxExp.runtimeValue = maxExp;
    }
}
