using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int index = 0;

    public InputActionAsset inputActions;
    private InputAction uiUpAction;
    private InputAction uiDownAction;
    private InputAction uiReturnAction;

    private void Start()
    {
        SelectButton(0);
        ButtonHoverSelect.OnButtonHoverr += UIButton_OnButtonHoverr;

        if (inputActions != null)
        {
            inputActions.FindActionMap("UI").Enable();   
        }
        uiUpAction = InputSystem.actions.FindAction("UI/MenuUp");
        uiDownAction = InputSystem.actions.FindAction("UI/MenuDown");
        uiReturnAction = InputSystem.actions.FindAction("UI/MenuSelect");
    }

    private void UIButton_OnButtonHoverr(int obj)
    {
        index = obj;
        SelectButton(obj);
    }

    public void SetIndex(int value)
    {
        index = value;
        SelectButton(index);
    }

    private void OnDestroy()
    {
        ButtonHoverSelect.OnButtonHoverr -= UIButton_OnButtonHoverr;
    }

    private void SelectButton(int index)
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(0).gameObject.SetActive(false);
        }


        buttons[index].transform.GetChild(0).gameObject.SetActive(true);
    }


    private void Update()
    {

        if (uiUpAction.WasPressedThisFrame())
        {
            
            index--;
            index = Mathf.Clamp(index, 0, buttons.Length);
            if (!buttons[index].gameObject.activeInHierarchy)
            {
                index++;
                return;
            }


            SelectButton(index);
        }
        
        if (uiDownAction.WasPressedThisFrame())
        {
         
            index++;
            index = Mathf.Clamp(index, 0, buttons.Length-1);

            if (!buttons[index].gameObject.activeInHierarchy)
            {
                index--;
                return;
            }
            SelectButton(index);

        }

        if(uiReturnAction.WasPressedThisFrame())
        {
            buttons[index].onClick.Invoke();
        }

    }
}
