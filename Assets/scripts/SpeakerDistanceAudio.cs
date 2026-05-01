using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeakerDistanceAudio : MonoBehaviour
{
    public Transform player;

    public float maxDistance = 25f;
    public float minVolume = 0.05f;
    public float maxVolume = 1f;

    private AudioSource speakerAudio;

    void Start()
    {
        speakerAudio = GetComponent<AudioSource>();

        speakerAudio.loop = true;
        speakerAudio.spatialBlend = 1f;
        speakerAudio.minDistance = 2f;
        speakerAudio.maxDistance = maxDistance;

        if (!speakerAudio.isPlaying && speakerAudio.clip != null)
            speakerAudio.Play();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        float closeness = 1f - Mathf.Clamp01(distance / maxDistance);

        speakerAudio.volume = Mathf.Lerp(minVolume, maxVolume, closeness);
    }
}