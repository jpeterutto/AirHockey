using UnityEngine;

public class PuckOutOfBoundsReset : MonoBehaviour
{
    public PuckController puckController;

    public float minX = -5.5f;
    public float maxX =  5.5f;
    public float minY = -10f;
    public float maxY =  10f;

    void Awake()
    {
        if (puckController == null)
            puckController = GetComponent<PuckController>();
    }

    void Update()
    {
        Vector3 p = transform.position;
        if (p.x < minX || p.x > maxX || p.y < minY || p.y > maxY)
        {
            puckController.ResetPuck();
        }
    }
}