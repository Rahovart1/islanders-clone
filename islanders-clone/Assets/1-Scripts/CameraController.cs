using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    private Transform _cam;
    private Vector3 _targetPosition;
    private Vector3 _targetHeight;
    private Quaternion _targetRotation;
    private Vector3 _positionDelta;
    private float _rotationDelta;
    private float _heightDelta;
    [Header("Position Settings")]
    [SerializeField] private float _positionSensivity = 1f;
    [SerializeField] private float _positionMinDistance = 100f;
    [SerializeField] private float _positionMaxDistance = 100f;

    [Header("Height Settings")]
    [SerializeField] private float _heightSensivity = 1f;
    [SerializeField] private float _heightMinDistance = 100f;
    [SerializeField] private float _heightMaxDistance = 100f;
    
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSensivity = 1f;
    [SerializeField] private float _smoothTime = 0.3f;

    private void Start()
    {
        _cam = Camera.main.transform;    
        _targetPosition = transform.position;
        _targetHeight = _cam.localPosition;
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        RotationHandle();
        HeightHandle();
        PositionHandle();

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
    private void PositionHandle()
    {
        _targetPosition = _targetPosition - (transform.right * _positionDelta.x + transform.forward * _positionDelta.z) * _positionSensivity;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _positionMinDistance, _positionMaxDistance);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, _positionMinDistance, _positionMaxDistance);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothTime);
    }
    public void ReceiveInputs(Vector2 positionDelta, float rotationDelta, float heightDelta)
    {
        _positionDelta = new Vector3(positionDelta.x, 0, positionDelta.y).normalized;
        _rotationDelta = rotationDelta;
        _heightDelta = heightDelta/120;
        _heightDelta = Mathf.Clamp(_heightDelta, -1, 1);
    }
}


