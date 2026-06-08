using UnityEngine;

public class EnemyBound : MonoBehaviour {

    void Update() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -GameManager.bounds.x, GameManager.bounds.x), 0, Mathf.Clamp(transform.position.z, -GameManager.bounds.y, GameManager.bounds.y));
    }
}
