using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MalletMouseController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 25f;

    [Header("Bounds (world units)")]
    public float minX = -3.2f;
    public float maxX =  3.2f;
    public float minY = -4.5f;
    public float maxY =  0.0f; 

    Rigidbody2D rb;
    Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (cam == null) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = new Vector2(mouseWorld.x, mouseWorld.y);

        target.x = Mathf.Clamp(target.x, minX, maxX);
        target.y = Mathf.Clamp(target.y, minY, maxY);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, maxSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}