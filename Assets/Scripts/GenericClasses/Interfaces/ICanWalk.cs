using UnityEngine;

public interface ICanWalk
{
    Vector2 StartingPoint { get; }
    float WalkRange { get; }
    float Speed { get; }

    void Walk(ref Vector2 velocity);
}