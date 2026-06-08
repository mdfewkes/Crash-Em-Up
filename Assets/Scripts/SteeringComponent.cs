using UnityEngine;

public class SteeringComponent : MonoBehaviour {
    public Vector2 speed;

    public bool targetSelfOnAwake = false;

    public bool roam = true;
    public float roamCooldownMin = 5f;
    public float roamCooldownMax = 15f;
    private float roamCooldown;

    [SerializeField] Vector2 target;
    [SerializeField] Transform targetObject = null;
    Vector2 steeringVector;

    void Awake() {
        if (targetSelfOnAwake) {
            GameObject anchor = Instantiate(new GameObject(), transform.position, transform.rotation);
            targetObject = anchor.transform;
        }

        roamCooldown = Random.Range(roamCooldownMin, roamCooldownMax);
    }

	void Update() {
		roamCooldown -= Time.deltaTime;
        if (targetObject == null && roam && roamCooldown <= 0f) {
            Vector2 playerpos = new Vector2(PlayerTag.playerTag.transform.position.x, PlayerTag.playerTag.transform.position.z);
            if (Random.Range(0f, 1f) < 0.333f) {
                target.x += Random.Range(-5f, 5f);
                target.y += Random.Range(-5f, 5f);
            } else if (Random.Range(0f, 1f) < 0.5f){
                target.x = playerpos.x + Random.Range(-4f, 4f) + 2f;
                target.y = playerpos.y + Random.Range(-5f, 5f) + 1f;
            } else {
                Vector2 pos = new Vector2(transform.position.x, transform.position.z);
                Vector2 direction = playerpos - pos;
                direction *= Random.Range(0.3f, 0.8f);
                target = pos + direction;
            }


            roamCooldown = Random.Range(roamCooldownMin, roamCooldownMax);
        }
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
