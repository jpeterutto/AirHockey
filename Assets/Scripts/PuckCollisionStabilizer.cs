using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckCollisionStabilizer : MonoBehaviour
{
    public float maxSpeedAfterCollision = 16f; 
    public float minBounceAngleDegrees = 8f;   

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
     
        if (rb.linearVelocity.magnitude > maxSpeedAfterCollision)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeedAfterCollision;

        
        float angle = Vector2.Angle(rb.linearVelocity, Vector2.right); 
        float fromHorizontal = Mathf.Min(angle, 180f - angle);   

        if (fromHorizontal < minBounceAngleDegrees)
        {
           
            float sign = Mathf.Sign(rb.linearVelocity.y == 0 ? Random.Range(-1f, 1f) : rb.linearVelocity.y);
            Vector2 v = rb.linearVelocity;
            v.y = Mathf.Abs(v.y) + 1.2f; 
            v.y *= sign;
            rb.linearVelocity = v.normalized * Mathf.Min(rb.linearVelocity.magnitude, maxSpeedAfterCollision);
        }
    }
}