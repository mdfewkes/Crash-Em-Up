using UnityEngine;

public class SteeringComponent : MonoBehaviour {
    public Vector2 speed;

    public bool targetSelfOnAwake = false;

    [SerializeField] Vector2 target;
    [SerializeField] Transform targetObject = null;
    Vector2 steeringVector;

	void Awake() {
		if (targetSelfOnAwake) targetObject = transform;
	}

    public void SetTarget(Vector2 position) {
        target = position;
        targetObject = null;
    }

    public void SetTarget(Vector3 position) {
        target = new Vector2(position.x, position.z);
        targetObject = null;
    }

    public void SetTarget(Transform targetTransform) {
        target = new Vector2(targetTransform.position.x, targetTransform.position.z);
        targetObject = targetTransform;
    }

	public Vector2 UpdateSteeringVector() {
        steeringVector = Vector2.zero;
        foreach(SteeringArea area in SteeringArea.steeringAreas) {
            if (area.gameObject == gameObject) continue;

            steeringVector += area.GetPull(this.transform.position);
        }

        if (targetObject != null) SetTarget(targetObject.transform);
        Vector2 targetDirection = new Vector2(target.x - transform.position.x, target.y - transform.position.z);
        if (targetDirection.magnitude > 1.0f) targetDirection.Normalize();

        steeringVector = (steeringVector + targetDirection) * speed * Time.deltaTime;
        return steeringVector;
    }
}
