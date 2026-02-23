using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MalletAIController : MonoBehaviour
{
    [Header("References")]
    public Transform puck;

    [Header("Movement")]
    public float maxSpeed = 14f;

    [Header("Bounds (world units)")]
    public float minX = -4.2f;
    public float maxX =  4.2f;
    public float minY =  1.0f;  
    public float maxY =  8.2f;

    [Header("AI Behavior")]
    public float defendY = 4.5f; 

    private Rigidbody2D rb;
    private Rigidbody2D puckRb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (puck != null)
            puckRb = puck.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (puck == null) return;
        if (puckRb == null) puckRb = puck.GetComponent<Rigidbody2D>();

        bool puckInAiSide = puck.position.y > 0f;
        bool puckComingUp = puckRb != null && puckRb.linearVelocity.y > 0.2f;

        Vector2 target;


        if (puckInAiSide || puckComingUp)
            target = puck.position;
        else
            target = new Vector2(puck.position.x, defendY); 

        target.x = Mathf.Clamp(target.x, minX, maxX);
        target.y = Mathf.Clamp(target.y, minY, maxY);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, maxSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}