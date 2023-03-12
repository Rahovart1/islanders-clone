using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHandler : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GameObject _obj;
    [SerializeField] private LayerMask _placeableLayer;
    [SerializeField] private Material _canPlaceableMaterial;
    [SerializeField] private Material _cantPlaceableMaterial;
    [SerializeField] private MeshRenderer _objMeshRenderer;
    private void Start()
    {
        _camera = Camera.main;
        _objMeshRenderer = _obj.GetComponent<MeshRenderer>(); 
    }

    private void Update()
    {
        HandlePlace();
    }
    private void HandlePlace()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, _placeableLayer))
        {
            _objMeshRenderer.material = _canPlaceableMaterial;
            _obj.transform.position = hit.point;
        }
        else
        {
            _objMeshRenderer.material = _cantPlaceableMaterial;
        }
    }
}
