using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnCollision : MonoBehaviour
{
    public AudioClip hitClip;

    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitClip != null)
            source.PlayOneShot(hitClip);
    }
}