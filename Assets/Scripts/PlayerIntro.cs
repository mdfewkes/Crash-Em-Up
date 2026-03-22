using System.Collections;
using UnityEngine;
using System;


public class PlayerIntro : MonoBehaviour
{
    public static event System.Action OnReached;

    [SerializeField] private float startingPostionX;
    [SerializeField] private float endPostionX;
    [SerializeField] private float speed;

    private void OnEnable()
    {
        GameManager.OnGameStart += GameManager_OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart()
    {
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        while (transform.position.x< endPostionX-1 )
        {
            transform.position = new Vector3( Mathf.Lerp(transform.position.x, endPostionX, speed * Time.deltaTime),transform.position.y,transform.position.z);
            yield return null;
        }
        OnReached?.Invoke();
        this.enabled = false;
    }
}
