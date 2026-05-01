using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneManager : MonoBehaviour
{
    public string microphoneName;
    public bool playThroughSpeakers = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("No microphone found.");
            return;
        }

        if (string.IsNullOrEmpty(microphoneName))
            microphoneName = Microphone.devices[0];

        audioSource.clip = Microphone.Start(microphoneName, true, 10, 44100);
        audioSource.loop = true;
        audioSource.mute = false;
        audioSource.volume = 0.01f;
        
        while (Microphone.GetPosition(microphoneName) <= 0) { }
        
        audioSource.Play();

        Debug.Log("Microphone started: " + microphoneName);
    }
}