using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Private Variables
    private Inputs inputs;
    #endregion

    #region Public Variables
    public Vector2 MouseDelta { get; private set; }
    public Vector2 ScrollDelta { get; private set; }
    public bool IsLeftPressed { get; private set; }
    public bool IsRightPressed { get; private set; }

    #endregion
    private void Start()
    {

    }
    private void Update()
    {

    }
    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new Inputs();

            inputs.Camera.Delta.performed += ctx => MouseDelta = ctx.ReadValue<Vector2>();
            inputs.Camera.Scroll.performed += ctx => ScrollDelta = ctx.ReadValue<Vector2>();

            inputs.Camera.LeftButton.started += _ => IsLeftPressed = true;
            inputs.Camera.LeftButton.canceled += _ => IsLeftPressed = false;
            inputs.Camera.RightButton.started += _ => IsRightPressed = true;
            inputs.Camera.RightButton.canceled += _ => IsRightPressed = false;
        }   
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
}
