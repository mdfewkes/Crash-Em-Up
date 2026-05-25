using UnityEngine;

public class vroomVroom : MonoBehaviour
{
    [SerializeField] GameObject motionTarget;

    private AudioSource sfx;
    // start low for a nice revving sound to begin with
    private float currentPitch = 2.0f; 
    private float idlePitch = 2.0f;
    private float accelPitch = 3.0f;
    private float brakePitch = 0.5f;
    private float turnPitch = 3.0f;
    private float pitchChangeSpeed = 2f;

    private Vector3 lastPosition;
    
    void Start()
    {
        sfx = GetComponent<AudioSource>();

        if (motionTarget) lastPosition = motionTarget.transform.position;
    }

    void Update()
    {
        if (!sfx) return;
        if (!motionTarget) return;
        Vector3 positionDelta = motionTarget.transform.position - lastPosition;
        lastPosition = motionTarget.transform.position;

        float accel = positionDelta.x;
        float turning = positionDelta.z;

        float targetPitch = idlePitch;
        if (turning != 0f) {
            targetPitch = turnPitch;
        }
        if (accel > 0f) {
            targetPitch = accelPitch;
        }
        if (accel < 0f) {
            targetPitch = brakePitch;
        }
        
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, pitchChangeSpeed * Time.deltaTime);
        sfx.pitch = currentPitch;

        // Debug.Log("Engine Pitch: "+currentPitch);

    }
}