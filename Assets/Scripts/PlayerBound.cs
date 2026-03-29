using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBound : MonoBehaviour
{
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;
    [SerializeField] private bool boundXForward = true;


    private void OnEnable()
    {
        GameTimer.OnTimeExceed += GameTimer_OnTimeExceed;
    }

    private void GameTimer_OnTimeExceed()
    {
        boundXForward = false;
    }

    private void OnDisable()
    {
        GameTimer.OnTimeExceed -= GameTimer_OnTimeExceed;
    }

    // Update is called once per frame
    void Update()
    {
        if (boundXForward)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, xRange), 0, Mathf.Clamp(transform.position.z,-zRange, zRange));
        else
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xRange, 100), 0, Mathf.Clamp(transform.position.z, -zRange, zRange));

        if(transform.position.x > xRange + 5)
        {
            SceneManager.LoadScene(1);
        }
    }
}
