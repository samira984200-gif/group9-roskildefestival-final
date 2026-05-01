using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundWaveManager : MonoBehaviour
{
    public Transform[] soundWaves;

    public float minScale = 0.5f;
    public float maxScale = 5f;
    public float sensitivity = 3f;
    public float smoothSpeed = 5f;

    private AudioSource audioSource;
    private float[] samples = new float[64];

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource == null || soundWaves.Length == 0)
            return;

        float audioLevel = GetAudioLevel();

        float targetScale = Mathf.Lerp(minScale, maxScale, audioLevel * sensitivity);

        foreach (Transform wave in soundWaves)
        {
            if (wave == null) continue;

            float newScale = Mathf.Lerp(
                wave.localScale.x,
                targetScale,
                smoothSpeed * Time.deltaTime
            );

            wave.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    float GetAudioLevel()
    {
        audioSource.GetOutputData(samples, 0);

        float sum = 0f;

        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        return Mathf.Clamp01(sum * 10f);
    }
}