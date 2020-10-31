using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class NPC : MonoBehaviour, IInteractable, ICanWalk
{
    bool gameIsPaused = false;
    [SerializeField] bool entered = false;

    [SerializeField] Vector2 velocity;
    [SerializeField] protected Vector2 startingPoint;
    [SerializeField] private float walkRange;
    [SerializeField] private float speed;

    protected bool active = false;
    public bool Entered => entered;
    public bool Active => active;

    protected Rigidbody2D myRigidBody => gameObject.GetComponent<Rigidbody2D>();
    protected Animator animator => gameObject.GetComponent<Animator>();

    public Vector2 StartingPoint => startingPoint;
    public float WalkRange => walkRange;
    public float Speed => speed;

    public abstract void Interact(GameObject interacter);

    public void OnInteractableEnter(IInteracter interacter)
    {
        Debug.Log(interacter);
        if (interacter == null) { return; }
        interacter.currentInteractable = this;

        GameEvents.InteractableEnter(interacter as Player);

        entered = true;
    }
    public void OnInteractableExit(IInteracter interacter)
    {
        Debug.Log(interacter);
        if (interacter == null) { return; }

        GameEvents.InteractableExit(interacter as Player);

        entered = false;
        interacter.currentInteractable = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // do something
        if (collision.CompareTag("Player"))
        {
            OnInteractableEnter(collision.GetComponent<IInteracter>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // do something
            OnInteractableExit(collision.GetComponent<IInteracter>());
        }
    }

    public void Walk(ref Vector2 velocity)
    {
        if (entered || velocity == Vector2.zero)
        {
            myRigidBody.bodyType = RigidbodyType2D.Static;
            animator.SetBool("moving", false); 
            return;
        }

        if (velocity.x > 0)
            if ((velocity.x * Time.deltaTime * speed) + transform.position.x >= startingPoint.x + walkRange)
                velocity = -velocity;
        if (velocity.x < 0)
            if ((velocity.x * Time.deltaTime * speed) + transform.position.x <= startingPoint.x - walkRange)
                velocity = -velocity;
        if (velocity.y > 0)
            if ((velocity.y * Time.deltaTime * speed) + transform.position.y >= startingPoint.y + walkRange)
                velocity = -velocity;
        if (velocity.y < 0)
            if ((velocity.y * Time.deltaTime * speed) + transform.position.y <= startingPoint.y - walkRange)
                velocity = -velocity;

        animator.SetFloat("horizontal", velocity.x);
        animator.SetFloat("vertical", velocity.y);
        animator.SetBool("moving", true);

        transform.position = new Vector3(transform.position.x + (velocity.x * Time.deltaTime * speed), transform.position.y + (velocity.y * Time.deltaTime * speed), 0);
    }
    public virtual void Awake ()
    {
        GameEvents.onPause += GameEvents_onPause;
        GameEvents.onResume += GameEvents_onResume;
        startingPoint = new Vector2(transform.position.x, transform.position.y);
    }

    private void GameEvents_onResume()
    {
        gameIsPaused = false;
    }
    private void GameEvents_onPause()
    {
        gameIsPaused = true;
    }

    private void Update()
    {
        if (gameIsPaused) { return; }

        Walk(ref velocity);
    }
}
