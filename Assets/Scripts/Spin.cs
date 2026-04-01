using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = -1440f;

    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}
