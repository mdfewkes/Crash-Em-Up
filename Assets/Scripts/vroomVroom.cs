using UnityEngine;

public class vroomVroom : MonoBehaviour
{
    private AudioSource sfx;
    // start low for a nice revving sound to begin with
    private float currentPitch = 0f; 
    private float idlePitch = 2.0f;
    private float accelPitch = 3.0f;
    private float brakePitch = 0.5f;
    private float turnPitch = 3.0f;
    private float pitchChangeSpeed = 1f;
    
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!sfx) return;

        float accel = Input.GetAxis("Horizontal");
        float turning = Input.GetAxis("Vertical");

        float targetPitch = idlePitch;
        if (turning!=0f) targetPitch = turnPitch;
        if (accel>0f) targetPitch = accelPitch;
        if (accel<0f) targetPitch = brakePitch;
        
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, pitchChangeSpeed * Time.deltaTime);
        sfx.pitch = currentPitch;

        // Debug.Log("Engine Pitch: "+currentPitch);

    }
}