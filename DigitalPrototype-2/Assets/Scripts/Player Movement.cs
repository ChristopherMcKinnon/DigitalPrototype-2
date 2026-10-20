using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // RUN PROPERTIES
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float groundAccel = 90f;
    [SerializeField] private float groundDecel = 110f;
    [SerializeField] private float airAccel = 60f;
    [SerializeField] private float airDecel = 40f;

    // JUMP PROPERTIES
    [SerializeField] private bool allowJump = true;
    [SerializeField] private float jumpSpeed = 14f;
    [SerializeField] private float minJumpSpeed = 9f;
    [SerializeField] private float jumpCutMultiplier = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float maxFallSpeed = 20f;
    private float jumpBufferTimer;
    private bool jumpReleased;

    private Rigidbody2D rb;
    private InputAction moveAction;
    private InputAction jumpAction;

    private readonly ContactPoint2D[] contacts = new ContactPoint2D[8];
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Create reference to controls
        moveAction = new InputAction("Move", InputActionType.Value);
        jumpAction = new InputAction("Jump", InputActionType.Button);

        // Map the keybinds for controls
        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Positive", "<Keyboard>/d");
        jumpAction.AddBinding("<Keyboard>/w");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }
    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }
    void OnDestroy()
    {
        moveAction.Dispose();
        jumpAction.Dispose();
    }

    // Update is called once per frame
    
    void Update()
    {
        // Animate the player's scale always moving back to (1, 1, 1)
        Vector3 targetMove = new Vector3(1.0f, 1.0f, 1.0f);
        transform.localScale = Vector3.Lerp(transform.localScale, targetMove, 10f * Time.deltaTime);

        if (jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        // Tackles any on frame jumping actions
        if (allowJump && jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            jumpBufferTimer = jumpBufferTime;
            // Animate
            transform.localScale = new Vector3(0.8f, 1.4f, 1.0f);
        }
        

        if (jumpAction.WasReleasedThisFrame())
        {
            jumpReleased = true;
        }

    }
    void FixedUpdate()
    {
        bool grounded = IsGrounded();
        Vector2 vel = rb.linearVelocity;

        float input = moveAction.ReadValue<float>();
        float target = input * moveSpeed;
        float rate;

        // Check if the player is moving AT ALL in any direction
        if (Mathf.Abs(input) > 0.01f)
        {
            // Air acceleration and deceleration is slower
            rate = grounded ? groundAccel : airAccel;

            // Check if the target position to get to and the current velocity are the same direction, and if the player is moving at all
            if (Mathf.Sign(target) != Mathf.Sign(vel.x) && Mathf.Abs(vel.x) > 0.01f)
            {
                rate = grounded ? groundDecel : airDecel;
            }
        }
        // If the player ISN'T moving then decelerate them
        else
        {
            rate = grounded ? groundDecel : airDecel;
        }

        // Finally apply this movement via rate over time
        vel.x = Mathf.MoveTowards(vel.x, target, rate * Time.fixedDeltaTime);

        // If the player jumps in the air but lands within the timer set by that action, the player is allowed to jump
        if (jumpBufferTimer > 0 && grounded)
        {
            vel.y = jumpSpeed;
            jumpBufferTimer = 0f;
            jumpReleased = false;
        }

        // Cut the jump short if released before the peak of the jump
        if (jumpReleased && vel.y > minJumpSpeed)
        {
            vel.y *= jumpCutMultiplier;
            if (vel.y < minJumpSpeed)
            {
                vel.y = minJumpSpeed;
            }

            jumpReleased = false;
        }

        // Incur falling, and a max fall speed to prevent falling at infinite speed
        if (!grounded)
        {
            vel.y += -0.5f;
            vel.y = Mathf.Max(vel.y, -maxFallSpeed);
        }
        else
        {
            // If on the floor clamp downward velocity to 0
            if (vel.y < 0.01f)
            {
                vel.y = 0f;
            }
        }
        

        rb.linearVelocity = vel;
    }
    private bool IsGrounded()
    {
        // Loops through any contacts with the player and determines if the surface normal is pointed upwards (checks if the is surface flat enough to stand on)
        int count = rb.GetContacts(contacts);
        for (int i = 0; i < count; i++)
        {
            if (contacts[i].normal.y > 0.5f) return true;
        }
        return false;
    }
}
