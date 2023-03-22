using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandle : MonoBehaviour
{
    public bool IsColliding { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            IsColliding = false;
        }
    }
}
