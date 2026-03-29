using UnityEngine;

public class SteeringComponent : MonoBehaviour {
    public float speed;

    Vector2 target;
    Transform targetObject;
    Vector2 steeringVector;

	void Start() {
		targetObject = transform;
	}

    public void SetTarget(Vector2 position) {
        target = position;
        targetObject = null;
    }

    public void SetTarget(Vector3 position) {
        SetTarget(new Vector2(position.x, position.z));
    }

    public void SetTarget(Transform targetTransform) {
        SetTarget(new Vector2(targetTransform.position.x, targetTransform.position.z));
        targetObject = targetTransform;
    }

    public void ResetTarget() {
		targetObject = transform;
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
