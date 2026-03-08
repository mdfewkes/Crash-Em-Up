using UnityEngine;

public class PlayerBound : MonoBehaviour
{
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;

    // Update is called once per frame
    void Update()
    {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), 0, Mathf.Clamp(transform.position.z,-zRange, zRange));
    }
}
