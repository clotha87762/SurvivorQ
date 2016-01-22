using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform target;
    public float speedX = 1.0f;
    public float speedY = 1.0f;
    public float maxDistance=5.0f;
    private Transform mCamera;
    [SerializeField] private float rotateMax = 90f;
    [SerializeField] private float rotateMin = 0.0f;
    [SerializeField] private float distanceMax = 5.0f;
    [SerializeField] private float distanceMin = 2.0f;

    [SerializeField] private float x = 0.0f;   //representing mouseX axis
    [SerializeField] private float y = 0.0f;   //representing mouseY axis

	private float resetRotateSpeed = 5.0f;
	private bool resetFlag = false;
	private float targetX = 0.0f;
	
	// Use this for initialization
	void Start () {
	    if(Camera.main != null)
        {
            mCamera = Camera.main.transform;
        }
        else Debug.Log("camera_controller   ::No main camera");

        //target = GameObject.Find("/center").transform;

        Vector3 angles = mCamera.eulerAngles;
        x = angles.y;
        y = angles.x;
        ResetCamera();
	}

	// Update is called once per frame
	void Update () {
        if (target)
        {
			if(Input.GetButtonDown ("ResetCamera")) 
			{
				targetX = target.eulerAngles.y;
				resetFlag = true;
			}
			if (resetFlag) LerpResetCamera ();
			else 
			{
				x += Input.GetAxis("RotateCamera") * speedX * maxDistance;
				y = 30;
				//x += Input.GetAxis("Mouse X") * speedX * maxDistance;
	            //y -= Input.GetAxis("Mouse Y") * speedY;
			}
	        y = ClampAngle(y, rotateMin, rotateMax);         

	        Quaternion rotation = Quaternion.Euler(y, x, 0);

	        //scroll to zoom
	        maxDistance = Mathf.Clamp(maxDistance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

	        Vector3 negDistance = new Vector3(0.0f, 0.0f, -maxDistance);
	        Vector3 position = target.position + rotation * negDistance;

	        mCamera.rotation = rotation;
	        mCamera.position = position;
	        //mCamera.position = Vector3.Lerp(mCamera.position, position, Time.fixedDeltaTime * 5.0f);
			
        }
	}

	void LerpResetCamera()
	{
		x = Mathf.Lerp (x, targetX, Time.deltaTime * resetRotateSpeed);
		if(Mathf.Abs(x - targetX) < 0.001f) resetFlag = false;
	}


    void ResetCamera()
    {
        x = target.eulerAngles.y;
        //y = 30;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        else if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);

    }
}
