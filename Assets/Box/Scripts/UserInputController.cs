using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxController))]
public class UserInputController : MonoBehaviour
{

    private BoxController mBox;
    private Transform mCamera;
    private Vector3 moveVector;
    private bool enableControl = true;
	private bool resettingCamera = false;
	private Vector3 cameraFoward;
	private Vector3 cameraRight;
	private float tempH, tempV, h, v;
	private bool keyH, keyV;
	private bool keyHAxis, keyVAxis;
	
    void Start()
    {
        if (Camera.main != null)
        {
            mCamera = Camera.main.transform;
        }
        else Debug.Log("userinput_controller::No main camera");
        mBox = transform.GetComponent<BoxController>();
    }
	
	void Update()
	{        
		if (enableControl)
        {
			if(!resettingCamera && Input.GetButtonDown ("ResetCamera")) 
			{
				Debug.Log("resettingCamera:: ture");
				resettingCamera = true;
				cameraFoward = Vector3.Scale(mCamera.forward, new Vector3(1, 0, 1)).normalized;
				cameraRight = Vector3.Scale(mCamera.right, new Vector3(1, 0, 1)).normalized;
				keyHAxis = (h>0.0f);
				keyVAxis = (v>0.0f);
				keyH = Input.GetButton ("Horizontal");
				keyV = Input.GetButton ("Vertical");
			}
        }
	}
	
	
    void FixedUpdate()
    {
        if (enableControl)
        {
            MoveInput();
            AttackInput();
        }
    }

    void MoveInput()
    {
        //jump if character is grounded and Jump key is pressed
        if (Input.GetButton("Jump")) mBox.Jump(); 


        ///////////////////////////////////
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
		bool tempkeyHAxis = (h>0.0f);
		bool tempkeyVAxis = (v>0.0f);
		//if(resettingCamera && (Mathf.Abs(h-tempH) > 0.1f || Mathf.Abs(v-tempV) > 0.1f)) 
		if(resettingCamera && ( (keyH != Input.GetButton ("Horizontal") || keyV != Input.GetButton ("Vertical")) || 
								(tempkeyHAxis != keyHAxis || tempkeyVAxis != keyVAxis)
								) )
		{
			resettingCamera = false;
			Debug.Log("resettingCamera:: false");
			//h = 0.0f;
			//v = 0.0f;
		}

        //calculate move vector and pass to boxcontroller
        if (mCamera != null)
        {
            if(!resettingCamera)
			{
				cameraFoward = Vector3.Scale(mCamera.forward, new Vector3(1, 0, 1)).normalized;
				cameraRight = Vector3.Scale(mCamera.right, new Vector3(1, 0, 1)).normalized;
			}
            moveVector = v * cameraFoward + h * cameraRight;
        }
        else moveVector = v * Vector3.forward + h * Vector3.right;
        mBox.Move(moveVector, Input.GetKey(KeyCode.LeftShift));
        
        //////////////////////////////////
    }

    void AttackInput()
    {
        if (Input.GetMouseButton(0)) mBox.Attack();
    }

    public void SetEnable(bool set)
    {
        enableControl = set;
    }
}
