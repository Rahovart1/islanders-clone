using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    #region Private Variables
    private InputManager _inputs;
    private Transform _cam;
    private Vector3 _targetPosition;
    private Vector3 _targetHeight;
    private Quaternion _targetRotation;
    private Vector3 _positionDelta;
    private float _rotationDelta;
    private float _heightDelta;
    #endregion

    #region Serializeable Variables
    [Header("Position Settings")]
    [SerializeField] private float _positionSensivity = 1f;
    [SerializeField] private float _positionMinDistance = 100f;
    [SerializeField] private float _positionMaxDistance = 100f;

    [Header("Height Settings")]
    [SerializeField] private float _heightSensivity = 1f;
    [SerializeField] private float _heightMinDistance = 100f;
    [SerializeField] private float _heightMaxDistance = 100f;
    [SerializeField] private AnimationCurve _positionCurve;
    
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSensivity = 1f;
    [SerializeField] private float _smoothTime = 0.3f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _cam = Camera.main.transform;
        _inputs = GetComponent<InputManager>();    
        _targetPosition = transform.position;
        _targetHeight = _cam.localPosition;
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        InputHandle();
        RotationHandle();
        HeightHandle();
        PositionHandle();
    }
    #endregion
 
    #region Private Methods
    private void PositionHandle()
    {
        float _currentPositionSensivity = _positionSensivity * _positionCurve.Evaluate((_cam.localPosition.y - _heightMinDistance) / (_heightMaxDistance - _heightMinDistance));

        _targetPosition = _targetPosition - (transform.right * _positionDelta.x + transform.forward * _positionDelta.z) * _currentPositionSensivity;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _positionMinDistance, _positionMaxDistance);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, _positionMinDistance, _positionMaxDistance);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothTime);
    }
    private void RotationHandle()
    {
        _targetRotation = _targetRotation * Quaternion.Euler(0, _rotationDelta * _rotationSensivity, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _smoothTime);
    }
    private void HeightHandle()
    {
        _targetHeight = _targetHeight - new Vector3(0f, _heightDelta, -_heightDelta) * _heightSensivity;
        _targetHeight.y = Mathf.Clamp(_targetHeight.y, _heightMinDistance, _heightMaxDistance);
        _targetHeight.z = -_targetHeight.y;
        _cam.localPosition = Vector3.Lerp(_cam.localPosition, _targetHeight, Time.deltaTime * _smoothTime);
    }
    private void InputHandle()
    {
        _positionDelta = _inputs.IsLeftPressed ? new Vector3(_inputs.MouseDelta.x, 0, _inputs.MouseDelta.y).normalized : Vector3.zero;
        _rotationDelta = _inputs.IsRightPressed ? _inputs.MouseDelta.x : 0f;
        _heightDelta = _inputs.ScrollDelta.y/120;
        _heightDelta = Mathf.Clamp(_heightDelta, -1, 1);   
    }
    #endregion
}


