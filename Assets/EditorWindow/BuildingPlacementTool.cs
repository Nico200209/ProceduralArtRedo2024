using UnityEditor;
using UnityEngine;

public class BuildingPlacementTool : EditorWindow
{
    private GameObject buildingPrefab;
    private BuildingStyle buildingStyle;
    private new Vector3 position = Vector3.zero;

    [MenuItem("Tools/Building Placement Tool")]
    public static void ShowWindow()
    {
        GetWindow<BuildingPlacementTool>("Building Placement Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Place Buildings in Scene", EditorStyles.boldLabel);

        buildingPrefab = (GameObject)EditorGUILayout.ObjectField("Building Prefab", buildingPrefab, typeof(GameObject), false);
        buildingStyle = (BuildingStyle)EditorGUILayout.ObjectField("Building Style", buildingStyle, typeof(BuildingStyle), false);
        position = EditorGUILayout.Vector3Field("Position", position);

        if (GUILayout.Button("Place Building"))
        {
            PlaceBuilding();
        }
    }

    private void PlaceBuilding()
    {
        if (buildingPrefab)
        {
            GameObject newBuilding = (GameObject)PrefabUtility.InstantiatePrefab(buildingPrefab);
            newBuilding.transform.position = position;

            Demo.SimpleBuilding simpleBuilding = newBuilding.GetComponent<Demo.SimpleBuilding>();
            if (simpleBuilding != null)
            {
                simpleBuilding.buildingStyle = buildingStyle;
                simpleBuilding.Generate();
            }
            else
            {
                Debug.LogWarning("The selected prefab does not have a SimpleBuilding component.");
            }
        }
        else
        {
            Debug.LogWarning("No building prefab selected.");
        }
    }
}
