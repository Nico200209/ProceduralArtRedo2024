using UnityEditor;
using UnityEngine;

public class RoadDrawingTool : EditorWindow
{
    private GameObject roadPrefab;
    private Vector3 startPoint;
    private Vector3 endPoint;

    [MenuItem("Tools/Road Drawing Tool")]
    public static void ShowWindow()
    {
        GetWindow<RoadDrawingTool>("Road Drawing Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Draw Roads in Scene", EditorStyles.boldLabel);

        roadPrefab = (GameObject)EditorGUILayout.ObjectField("Road Prefab", roadPrefab, typeof(GameObject), false);
        startPoint = EditorGUILayout.Vector3Field("Start Point", startPoint);
        endPoint = EditorGUILayout.Vector3Field("End Point", endPoint);

        if (GUILayout.Button("Draw Road"))
        {
            DrawRoad();
        }
    }

    private void DrawRoad()
    {
        if (roadPrefab)
        {
            GameObject newRoad = (GameObject)PrefabUtility.InstantiatePrefab(roadPrefab);
            newRoad.transform.position = (startPoint + endPoint) / 2;
            newRoad.transform.LookAt(endPoint);
            float distance = Vector3.Distance(startPoint, endPoint);
            newRoad.transform.localScale = new Vector3(newRoad.transform.localScale.x, newRoad.transform.localScale.y, distance);
        }
        else
        {
            Debug.LogWarning("No road prefab selected.");
        }
    }
}
