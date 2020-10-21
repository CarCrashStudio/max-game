using UnityEngine;
public class KillObjective : Objective
{
    private Player player;

    // enemy to target
    // enemy type tag (rat, slime, goblin)
    [SerializeField] private string enemyTag = "";

    // area to kill (center vector and radius float amount)
    private Vector2 killAreaCenter = Vector2.zero;
    private float killAreaRadius = 0f;

    public KillObjective(string enemyTag, Vector2 killAreaCenter, float killAreaRadius, string description, bool isCompleted, int currentAmount, int requiredAmount) :
        base (description, isCompleted, currentAmount, requiredAmount)
    {
        this.enemyTag = enemyTag;
        this.killAreaCenter = killAreaCenter;
        this.killAreaRadius = killAreaRadius;
    }
    public void EnemyKilled (Enemy enemy)
    {
        // if kill area radius is not 0 then
        if (killAreaRadius == 0f) { return; }

        // if player is in kill area radius then
        //if (player.gameObject.transform.position <= killAreaCenter + killAreaRadius)
        //{

        //}

        // if the enemy is the target enemy then
        if (enemy.CompareTag(enemyTag))
        {
            currentAmount++;
            Evaluate();
        }
    }
}