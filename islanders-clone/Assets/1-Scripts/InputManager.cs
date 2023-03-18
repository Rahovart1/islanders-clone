using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Inputs inputs;
    private CameraController cameraController;
    private Vector2 mouseDelta;
    private Vector2 scrollDelta;
    private bool _isLeftPressed = false;
    private bool _isRightPressed = false;
    private Vector2 _tempLeftButtonDelta;
    private float _tempRightButtonDelta;
    private void Start()
    {
        cameraController = GetComponent<CameraController>();    
    }
    private void Update()
    {
        MouseInputHandle();
    }
    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new Inputs();

            inputs.Camera.Delta.performed += ctx => mouseDelta = ctx.ReadValue<Vector2>();
            inputs.Camera.Scroll.performed += ctx => scrollDelta = ctx.ReadValue<Vector2>();

            inputs.Camera.LeftButton.started += ctx => _isLeftPressed = true;
            inputs.Camera.LeftButton.canceled += ctx => _isLeftPressed = false;
            inputs.Camera.RightButton.started += ctx => _isRightPressed = true;
            inputs.Camera.RightButton.canceled += ctx => _isRightPressed = false;

        }   
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
    private void MouseInputHandle()
    {
        _tempLeftButtonDelta = _isLeftPressed ? mouseDelta : Vector2.zero;
        _tempRightButtonDelta = _isRightPressed ? mouseDelta.x : 0;

        cameraController.ReceiveInputs(_tempLeftButtonDelta, _tempRightButtonDelta, scrollDelta.y);
    }
}
