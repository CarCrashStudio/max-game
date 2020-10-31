using UnityEngine;

public class Player : Entity, IInteracter
{
    public DungeonManager manager;
    public bool currentlyInInteractable = false;
    public float gold = 5f;
    public bool gameIsPaused = false;
    public byte Level { get => level; set => level = value; }
    public IInteractable currentInteractable { get; set; }
    [SerializeField] private GameObject interactableNotification;

    private void Awake ()
    {
        GameEvents.onLevelUp += onLevelUp;
        GameEvents.onInteractableEnter += OnInteractableEnter;
        GameEvents.onInteractableExit += OnInteractableExit;
        GameEvents.onPause += GameEvents_onPause;
        GameEvents.onResume += GameEvents_onResume;
    }

    private void GameEvents_onResume()
    {
        gameIsPaused = false;
    }
    private void GameEvents_onPause()
    {
        gameIsPaused = true;
    }

    public override void Start ()
    {
        base.Start();
    }
    public override void Update()
    {
        if (gameIsPaused) { return; }

        #region MOVEMENT
        Vector2 velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        moveEntity(velocity);
        #endregion
        base.Update();
    }

    public void Interact ()
    {
        if (currentInteractable == null) { return; }

        //currentlyInInteractable = (currentState != EntityState.INTERACTING);
        currentInteractable.Interact(this.gameObject);
        //currentState = (!currentlyInInteractable) ? EntityState.IDLE : EntityState.INTERACTING;
    }
    public void onLevelUp (Player player)
    {
        level++;
        GameEvents.ChangesMade();
    }
    private void OnInteractableEnter (Player player)
    {
        interactableNotification.SetActive(true);
    }
    private void OnInteractableExit(Player player)
    {
        interactableNotification.SetActive(false);
    }
}