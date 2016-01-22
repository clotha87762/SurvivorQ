using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(StationaryMovingPlatform))]
public class StationaryMovingPlatformEditor : Editor
{
    private SerializedObject mObj;
    private SerializedProperty mDuration;
    private SerializedProperty mStationsList;
    private int listCount;
    private StationaryMovingPlatform movingPlatform;
    private int removeIndex;

    public void OnEnable()
    {
        movingPlatform = (StationaryMovingPlatform)target;
        mObj = new SerializedObject(target);
        mDuration = mObj.FindProperty("duration");
        mStationsList = mObj.FindProperty("stationList");
    }

    public override void OnInspectorGUI()
    {
        listCount = mStationsList.arraySize;

        EditorGUILayout.BeginHorizontal();
        movingPlatform.SetUpNewStationVector3 = EditorGUILayout.Vector3Field("New Station At", movingPlatform.SetUpNewStationVector3);
        if (GUILayout.Button("Add new"))
        {
            GameObject tmpObj = movingPlatform.setNewStation();
            Selection.activeGameObject = tmpObj;
            movingPlatform.stationList.Add(tmpObj.transform);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.IntField("Delete Station At", removeIndex);
        if (GUILayout.Button("Delete old"))
        {
            movingPlatform.deleteStationAt(removeIndex);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Delete Last Station"))
        {
            movingPlatform.deleteLastStation();
        }
        DrawDefaultInspector();
        mObj.ApplyModifiedProperties();
    }
}
