using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHandler : MonoBehaviour
{
    #region Private Variables
    private Camera _camera;
    private InputManager _inputManager;
    private bool _canPlace = false;
    private ColliderHandle _objCollider1;
    private ColliderHandle _objCollider2;
    private ColliderHandle _objCollider3;
    private ColliderHandle _objCollider4;
    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject _obj;
    [SerializeField] private LayerMask _placeableLayer;
    [SerializeField] private Material _canPlaceableMaterial;
    [SerializeField] private Material _cantPlaceableMaterial;
    [SerializeField] private MeshRenderer _objMeshRenderer;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private ObjectData _objectData;
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private float _rotateDegree = 30f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _camera = Camera.main;
        _inputManager = GameObject.Find("Camera Holder").GetComponent<InputManager>();

        SetNewObject(_obj);
        CheckColliders();
    }

    private void Update()
    {
        HandlePlace();
        Place(); 
    }
    #endregion

    private void HandlePlace()
    {
        if (_obj == null) return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                CheckColliders();
            }
            else
            {
                if (_canPlace && (Vector3.Distance(_obj.transform.position, hit.point) > _maxDistance))
                {
                    return;
                }
                _canPlace = false; 
            }    
            _obj.transform.position = hit.point + _offset;
        }
    }
    private bool _place = false;
    private bool _release = false;

    private void PlaceObject()
    {
        Instantiate(_obj, _obj.transform.position, _obj.transform.rotation);
        // _obj = null;
        _place = false;
        _release = false;
    }
    private void Place()
    {
        if (_obj == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            _place = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _release = true;
        }
        if (_place && _release && _canPlace)
        {
            PlaceObject();
        }
    }

    private void CheckColliders()
    {
        if (_objCollider1 && _objCollider2.IsColliding && _objCollider3.IsColliding && _objCollider4.IsColliding)
        {
            _objMeshRenderer.material = _canPlaceableMaterial;
            _canPlace = true;
            Debug.Log("Can Place");
        }
        else
        {
            _objMeshRenderer.material = _cantPlaceableMaterial;
            _canPlace = false;
            Debug.Log("Can not Place");
            _release = false;
            _place = false;
        }
    }
    private void SetNewObject(GameObject obj)
    {
        _obj = obj;

        _objCollider1 = _obj.transform.GetChild(0).GetComponent<ColliderHandle>();
        _objCollider2 = _obj.transform.GetChild(1).GetComponent<ColliderHandle>();
        _objCollider3 = _obj.transform.GetChild(2).GetComponent<ColliderHandle>();
        _objCollider4 = _obj.transform.GetChild(3).GetComponent<ColliderHandle>();

        _objMeshRenderer = _obj.GetComponent<MeshRenderer>();
    }
    private void RemoveObject()
    {
        _obj = null;
    }
}
