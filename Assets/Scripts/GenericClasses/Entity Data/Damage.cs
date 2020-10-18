using UnityEngine;

/// <summary>
/// Used when creating a collision based damaging system
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Damage : MonoBehaviour
{
    [SerializeField] protected string otherTag;
    [SerializeField] protected float damage;

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Health temp = other.GetComponent<Health>();
            if (temp)
            {
                temp.Damage(damage);
            }
        }
    }
}
