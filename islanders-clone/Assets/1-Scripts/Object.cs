using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object Data")]
public class ObjectData : ScriptableObject
{
    public string _name;
    public int _id;
    public int _type;
    public int _baseValue;
    public float _range;
    public Vector3 _offset;
    public Sprite _icon;
    public GameObject _prefab;
    public PlaceableLayer _layer;
    
}