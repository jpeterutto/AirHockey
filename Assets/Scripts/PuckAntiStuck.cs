using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckAntiStuck : MonoBehaviour
{
    [Header("Detect stuck by position (recommended)")]
    public float minMoveDistance = 0.02f; 
    public float stuckTime = 0.45f;       

    [Header("Nudge")]
    public float nudgeImpulse = 2.0f;      
    public float edgeMargin = 0.35f;     

    [Header("Playfield bounds (world units)")]
    public float minX = -4.2f;
    public float maxX =  4.2f;
    public float minY = -8.2f;
    public float maxY =  8.2f;

    Rigidbody2D rb;
    Vector2 lastPos;
    float stuckTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPos = rb.position;
    }

    void FixedUpdate()
    {
        Vector2 pos = rb.position;
        float moved = (pos - lastPos).magnitude;

        if (moved < minMoveDistance)
            stuckTimer += Time.fixedDeltaTime;
        else
        {
            stuckTimer = 0f;
            lastPos = pos;
            return;
        }

        if (stuckTimer < stuckTime) return;

        Vector2 push = Vector2.zero;

        if (pos.x < minX + edgeMargin) push.x = 1f;
        else if (pos.x > maxX - edgeMargin) push.x = -1f;

        if (pos.y < minY + edgeMargin) push.y = 1f;
        else if (pos.y > maxY - edgeMargin) push.y = -1f;

        Vector2 dir = (push != Vector2.zero) ? push.normalized : Random.insideUnitCircle.normalized;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(dir * nudgeImpulse, ForceMode2D.Impulse);

        stuckTimer = 0f;
        lastPos = rb.position;
    }
}