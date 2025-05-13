using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine; // Ensure you're using the updated namespace for Cinemachine

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlin;

    private float shakeDuration;
    private float shakeIntensity;
    private float originalAmplitude;

    private void Awake()
    {
        // Singleton pattern for CameraShake
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Get the CinemachineVirtualCamera if not assigned
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Get the Perlin noise component for shake effect
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin == null)
            perlin = virtualCamera.gameObject.AddComponent<CinemachineBasicMultiChannelPerlin>();

        // Store the original amplitude for resetting later
        originalAmplitude = perlin.AmplitudeGain;
    }

    // Public method to trigger shake with intensity and duration
    public void ShakeCamera(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        perlin.AmplitudeGain = shakeIntensity;  // Set the amplitude for shaking
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;

            // Keep shaking
            if (shakeDuration <= 0)
            {
                perlin.AmplitudeGain = 0f; // Stop shaking when the duration ends
            }
        }
    }
}
