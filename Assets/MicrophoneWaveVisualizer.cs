using UnityEngine;

public class MicrophoneWaveVisualizer : MonoBehaviour
{
    public AudioSource microphoneSource;
    public Transform[] waves;

    public float sensitivity = 80f;
    public float minScale = 0.5f;
    public float maxScale = 5f;
    public float smoothSpeed = 8f;

    private float[] samples = new float[256];

    void Update()
    {
        if (microphoneSource == null) return;

        microphoneSource.GetOutputData(samples, 0);

        float sum = 0f;

        foreach (float sample in samples)
        {
            sum += sample * sample;
        }

        float volume = Mathf.Sqrt(sum / samples.Length);
        Debug.Log("Mic volume: " + volume);

        float targetScale = Mathf.Clamp(volume * sensitivity, minScale, maxScale);

        foreach (Transform wave in waves)
        {
            if (wave == null) continue;

            Vector3 target = new Vector3(targetScale, targetScale, targetScale);
            wave.localScale = Vector3.Lerp(wave.localScale, target, Time.deltaTime * smoothSpeed);
        }
    }
}