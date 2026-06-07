using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSelect : MonoBehaviour , IPointerEnterHandler
{

    public static event Action<int> OnButtonHoverr;

    [SerializeField] private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonHoverr?.Invoke(index);

    }
}
