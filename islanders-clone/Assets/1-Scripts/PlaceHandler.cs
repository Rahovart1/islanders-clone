using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHandler : MonoBehaviour
{
    #region Private Variables
    private Camera _camera;
    private InputManager _inputManager;
    public ColliderHandle[] _objColliders;
    private ColliderHandle _objCollider1;
    private ColliderHandle _objCollider2;
    private ColliderHandle _objCollider3;
    private ColliderHandle _objCollider4;
    private int _objBaseValue = 0;
    private bool _canPlace = false;
    private bool _place = false;
    private bool _release = false;
    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject _obj;
    [SerializeField] private GameObject _objPrefab;
    private GameObject _objTest;
    [SerializeField] private LayerMask _placeableLayer;
    [SerializeField] private Material _canPlaceableMaterial;
    [SerializeField] private Material _cantPlaceableMaterial;
    [SerializeField] private MeshRenderer _objMeshRenderer;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private ObjectData _objectData;
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private float _rotateDegree = 30f;
    [SerializeField] private GameObject _objectParent;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _camera = Camera.main;
        _inputManager = GameObject.Find("Camera Holder").GetComponent<InputManager>();

        SetNewObject(_obj);
        _objTest = _obj;
        CheckColliders();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentInGameState == InGameState.Place)
        {
            //CalculatePoint();
            RotateObject();
            HandlePlace();
            Place(); 
        }

    }
    #endregion

    #region Private Methods
    private void HandlePlace()
    {
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
                _objMeshRenderer.material = _cantPlaceableMaterial;
                _canPlace = false; 
            }    
            _obj.transform.position = hit.point + _offset;
        }
    }
    
    private void PlaceObject()
    {
        GameObject newObject = Instantiate(_objPrefab, _obj.transform.position, _obj.transform.rotation);
        _obj = null;
        _place = false;
        _release = false;
        GameManager.Instance.SetInGameState(InGameState.Normal);
        //GameManager.Instance.AddPoints(_objBaseValue + CalculatePoint());

        newObject.name = "house"; // replace later newObject.name = _objectData.name;
        newObject.transform.parent = _objectParent.transform;
    }
    private void Place()
    {
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
    private void RotateObject() 
    {
        if (_inputManager.ScrollDelta.y > 0)
        {
            _obj.transform.Rotate(0, _rotateDegree, 0);
        }
        else if (_inputManager.ScrollDelta.y < 0)
        {
            _obj.transform.Rotate(0, -_rotateDegree, 0);
        }
    }

    private void CheckColliders()
    {   
        if (_objCollider1.IsColliding && _objCollider2.IsColliding && _objCollider3.IsColliding && _objCollider4.IsColliding)
        {
            _objMeshRenderer.material = _canPlaceableMaterial;
            _canPlace = true;
            //Debug.Log("Can Place");
        }
        else
        {
            _objMeshRenderer.material = _cantPlaceableMaterial;
            _canPlace = false;
            _release = false;
            _place = false;
            //Debug.Log("Can not Place");
        }
    }
    private void SetNewObject(GameObject obj)
    {
        _obj = obj;
        _objMeshRenderer = _obj.GetComponent<MeshRenderer>();

        // foreach (Transform child in _obj.transform)
        // {
        //     child.gameObject.AddComponent<ColliderHandle>();
        // }
        _objCollider1 = _obj.transform.GetChild(0).GetComponent<ColliderHandle>();
        _objCollider2 = _obj.transform.GetChild(1).GetComponent<ColliderHandle>();
        _objCollider3 = _obj.transform.GetChild(2).GetComponent<ColliderHandle>();
        _objCollider4 = _obj.transform.GetChild(3).GetComponent<ColliderHandle>();
    }
    private int CalculatePoint()
    {
        int _points = 0;
        Collider[] objects = Physics.OverlapSphere(_obj.transform.position, _maxDistance);
        foreach (Collider obj in objects)
        {
            Debug.Log(obj.name);
            // TODO: Calculate points for each object
            // TODO: Add points to _points
        }
        return _points;
    }
    #endregion

    #region Public Methods
    public void RemoveObject()
    {
        if (_obj == null) return;
        _obj = null;
        GameManager.Instance.SetInGameState(InGameState.Normal);
    }
    public void SetNew()
    {
        
        SetNewObject(_objTest);
        GameManager.Instance.SetInGameState(InGameState.Place);
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (_obj == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_obj.transform.position, _maxDistance);
    }
}
