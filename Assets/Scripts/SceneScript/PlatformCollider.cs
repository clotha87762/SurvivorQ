using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlatformCollider : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform;
    }

    void OnCollisionExit(Collision other)
    {
        other.transform.parent = null;
    }
}
