using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
public class Object : ScriptableObject
{
    public string _name;
    public int _id;
    public int _type;
    public int _baseValue;
    public Sprite _icon;
    public GameObject _prefab;
    public PlaceableLayer _layer;
    public enum PlaceableLayer
    {
        grass,
        stone,
        sand,
        water
    }
}