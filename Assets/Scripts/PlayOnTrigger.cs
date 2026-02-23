using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    public AudioClip goalClip;
    public string puckTag = "Puck";

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null) source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
        source.spatialBlend = 0f; // 2D
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(puckTag)) return;
        if (goalClip != null) source.PlayOneShot(goalClip);
    }
}