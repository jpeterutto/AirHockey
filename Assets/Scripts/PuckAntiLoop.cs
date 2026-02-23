using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckAntiLoop : MonoBehaviour
{
    public float sameDirDotThreshold = 0.985f; 
    public int repeatHitsToNudge = 4;
    public float nudgeDegrees = 6f;         

    Rigidbody2D rb;
    Vector2 lastDir;
    int repeatCount;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDir = Vector2.zero;
        repeatCount = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 dir = rb.linearVelocity.sqrMagnitude > 0.0001f ? rb.linearVelocity.normalized : Vector2.zero;

        if (lastDir != Vector2.zero && Vector2.Dot(dir, lastDir) > sameDirDotThreshold)
            repeatCount++;
        else
            repeatCount = 0;

        lastDir = dir;

        if (repeatCount >= repeatHitsToNudge)
        {
            float angle = Random.value < 0.5f ? -nudgeDegrees : nudgeDegrees;
            rb.linearVelocity = Rotate(rb.linearVelocity, angle);
            repeatCount = 0;
        }
    }

    Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}