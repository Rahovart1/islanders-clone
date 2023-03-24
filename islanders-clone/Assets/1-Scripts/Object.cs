using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object Data")]
public class ObjectData : ScriptableObject
{
    public string objectName;
    public int id;
    public int type;
    public int baseValue;
    public float range;
    public Vector3 offset;
    public Sprite icon;
    public GameObject prefab;
    public PlaceableLayer layer;
}