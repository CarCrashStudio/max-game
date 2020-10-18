using UnityEngine;
using System.Collections;

public interface IHasCooldown
{
    int ID { get; }
    float CooldownTime { get; }
}
