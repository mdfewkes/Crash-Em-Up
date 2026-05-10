using UnityEngine;

public class ObjectMover : MonoBehaviour {
    public Vector3 speed;

    // Update is called once per frame
    void Update() {
        transform.position += speed * Time.deltaTime;
    }
}
