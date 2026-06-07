using UnityEngine;

public class EnemyBound : MonoBehaviour {
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;

    void Update() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), 0, Mathf.Clamp(transform.position.z, -zRange, zRange));
    }
}
