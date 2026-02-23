using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PuckController : MonoBehaviour
{
    [Header("Launch")]
    public float launchSpeed = 8f;
    public float minAbsY = 0.35f; 

    [Header("Speed Limit")]
    public float maxLinearSpeed = 14f; 

    [Header("Reset")]
    public float resetDelay = 0.6f;

    private Rigidbody2D rb;
    private Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Start()
    {
        Launch();
    }

    void FixedUpdate()
    {
        float maxSq = maxLinearSpeed * maxLinearSpeed;
        if (rb.linearVelocity.sqrMagnitude > maxSq)
            rb.linearVelocity = rb.linearVelocity.normalized * maxLinearSpeed;
    }

    public void Launch()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;

        Vector2 dir = GetRandomDirection();
        rb.AddForce(dir * launchSpeed, ForceMode2D.Impulse);
    }

    public void ResetPuck()
    {
        StopAllCoroutines();
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;

        yield return new WaitForSeconds(resetDelay);

        Vector2 dir = GetRandomDirection();
        rb.AddForce(dir * launchSpeed, ForceMode2D.Impulse);
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 dir;
        do
        {
            dir = Random.insideUnitCircle.normalized;
        } while (Mathf.Abs(dir.y) < minAbsY);

        return dir;
    }
}