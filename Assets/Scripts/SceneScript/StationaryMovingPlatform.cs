using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StationaryMovingPlatform : MonoBehaviour {
    ///////////////////////////////////////////
    //
    //      Custom Editor
    //
    ///////////////////////////////////////////

    private Vector3 NewStationPosition;
    public Vector3 SetUpNewStationVector3
    {
        get { return NewStationPosition; }
        set { NewStationPosition = value; }
    }

    public GameObject setNewStation()
    {
        GameObject tempObj = new GameObject("Station" + stationList.Count);
        tempObj.transform.position = NewStationPosition + this.transform.position;
        tempObj.transform.parent = this.transform;
        return tempObj;
    }

    public void deleteLastStation()
    {
        Transform tempObj = stationList[stationList.Count - 1];
        stationList.RemoveAt(stationList.Count-1);
        DestroyImmediate(tempObj.gameObject);
    }

    public void deleteStationAt(int index)
    {
        Transform tempObj = stationList[index];
        stationList.RemoveAt(index);
        DestroyImmediate(tempObj.gameObject);
    }

    ///////////////////////////////////////////
    public float duration = 2.0f;
    public Transform platform;
    public List<Transform> stationList;
    private bool pause = false;
    private List<Vector3> wayPoints = new List<Vector3>();
    private Transform mTransform;
    private Rigidbody mRigidbody;
    private int edgeIndex = 0;
    private int edgeCount = 0;
    private int maxEdgeCount = 0;

    private float timer = 0f;

    //manipulation
    private Transform currentSource;
    private Transform currentDestination;

	// Use this for initialization
	void Awake () {
        foreach(Transform ele in stationList)
        {
            wayPoints.Add(ele.position);
        }
        mTransform = platform.transform;
        mRigidbody = platform.GetComponent<Rigidbody>();
        mTransform.position = wayPoints[0];
        edgeCount = (wayPoints.Count - 1);
        edgeIndex = 0;
        currentSource = stationList[edgeIndex];
        currentDestination = stationList[edgeIndex+1];
	}

    void FixedUpdate()
    {
        if(pause) return;
        else
        {
            if ( (currentDestination.position - mTransform.position).magnitude < 0.01f)
            {
                edgeIndex += 1;
                edgeIndex = edgeIndex % (edgeCount * 2);
                if (edgeIndex < edgeCount)
                {
                    currentSource = stationList[edgeIndex];
                    currentDestination = stationList[edgeIndex + 1];
                }
                else
                {
                    currentSource = stationList[edgeCount * 2 - edgeIndex];
                    currentDestination = stationList[edgeCount * 2 - edgeIndex - 1];
                }
            }
            platformMove();
        }
    }

    //move from source to destination
    void platformMove()
    {
        Vector3 lerpVec = ((currentDestination.position - currentSource.position) * Time.fixedDeltaTime * 1/duration);
        mTransform.Translate(lerpVec);
        //Vector3 lerpVec = Vector3.Lerp(mTransform.position, currentDestination.position, Time.fixedDeltaTime * 1/duration);
        //mTransform.position = lerpVec;
    }

    public void SetMovementEnable(bool set)
    {
        pause = set;
    }
}
