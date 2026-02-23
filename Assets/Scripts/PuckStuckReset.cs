using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckStuckReset : MonoBehaviour
{
    [Header("References")]
    public PuckController puckController;

    [Header("Stuck detection")]
    public float minMoveDistance = 0.02f;  
    public float stuckSeconds = 1.2f;      
    public float minSpeed = 0.25f;         

    [Header("Playfield bounds (world units)")]
    public float minX = -4.2f;
    public float maxX =  4.2f;
    public float minY = -8.2f;
    public float maxY =  8.2f;

    [Header("Corner check")]
    public float edgeMargin = 0.35f; 

    Rigidbody2D rb;
    Vector2 lastPos;
    float timer;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lastPos = rb.position;

        if (puckController == null)
            puckController = GetComponent<PuckController>();
    }

    void FixedUpdate()
    {
        if (sr != null && !sr.enabled) return;
        if (puckController == null) return;

        Vector2 pos = rb.position;

        float moved = (pos - lastPos).magnitude;
        float speed = rb.linearVelocity.magnitude;

        if (moved < minMoveDistance && speed < minSpeed)
            timer += Time.fixedDeltaTime;
        else
        {
            timer = 0f;
            lastPos = pos;
            return;
        }

        if (timer < stuckSeconds) return;

        bool nearLeft   = pos.x < minX + edgeMargin;
        bool nearRight  = pos.x > maxX - edgeMargin;
        bool nearBottom = pos.y < minY + edgeMargin;
        bool nearTop    = pos.y > maxY - edgeMargin;

        bool inCorner = (nearLeft || nearRight) && (nearBottom || nearTop);

        if (inCorner)
        {
            puckController.ResetPuck();
        }

        timer = 0f;
        lastPos = rb.position;
    }
}