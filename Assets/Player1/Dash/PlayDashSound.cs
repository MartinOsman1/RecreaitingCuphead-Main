using UnityEngine;

public class DashSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayDashSound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
