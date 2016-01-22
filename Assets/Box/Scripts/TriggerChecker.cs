using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class TriggerChecker : MonoBehaviour {
    
    private bool mGrounded = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "PlayerGroundCheck" && !other.isTrigger)
        {
            mGrounded = true;
            Debug.Log("groundtrigger      ::Grounded");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "PlayerGroundCheck" && !other.isTrigger)
        {
            mGrounded = false;
            Debug.Log("groundtrigger      ::Not Grounded");
        }
    }

    public bool isGrounded() { return mGrounded; }
	
}
