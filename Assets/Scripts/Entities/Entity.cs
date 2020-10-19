using UnityEngine;

/* TODO:
 * Weapon and armor proficiencies? 
 * Weapon proficiency bonuses would be added to Attack rolls 
 * while armor proficiencies will determine whether or not the armor affects the user negatively or not
 */

public enum EntityState { WALK, ATTACK, STAGGER, IDLE, INTERACTING }
public class Entity : MonoBehaviour
{
    const int DEFENSE_CONSTANT = 5;
    [SerializeField] GameObject meleePoint;
    [SerializeField] protected byte level = 1;
    [SerializeField] private float meleePointDistanceFromPlayer = 10f;

    public float speed = 4f;

    public EntityState currentState;

    public Attributes attributes;

    public int defense = 0;
    public int proficiencyBonus = 2;

    [SerializeField] private ArmorProficiencies[] gainedArmorProficiencies;
    [SerializeField] private WeaponProficiencies[] gainedWeaponProficiencies;

    [SerializeField] protected Rigidbody2D myRigidBody => gameObject.GetComponent<Rigidbody2D>();
    [SerializeField] protected Animator animator => gameObject.GetComponent<Animator>();

    public WeaponProficiencies[] GainedWeaponProficiencies => gainedWeaponProficiencies;
    public ArmorProficiencies[] GainedArmorProficiencies => gainedArmorProficiencies;

    public virtual void Start()
    {
        attributes.Start();
    }
    public virtual void Update ()
    {
        

    }
    public void LateUpdate()
    {
        defense = DEFENSE_CONSTANT + attributes.totalModifiers.Dexterity;
    }

    protected void moveEntity (Vector2 velocity)
    {
        if (velocity != Vector2.zero)
        {
            meleePoint.transform.position = new Vector3(transform.position.x + velocity.x * meleePointDistanceFromPlayer, transform.position.y + velocity.y * meleePointDistanceFromPlayer, 0);
        }
        if ((currentState == EntityState.WALK || currentState == EntityState.IDLE) && velocity != Vector2.zero)
        {
            myRigidBody.position += velocity * speed * Time.deltaTime;

            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}

