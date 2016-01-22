using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MovingPlatformTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") other.transform.parent = transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") other.transform.parent = null;
    }
}
