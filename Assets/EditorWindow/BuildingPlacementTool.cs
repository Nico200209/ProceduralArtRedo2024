using Demo;
using UnityEditor;
using UnityEngine;

public class BuildingPlacementTool : EditorWindow
{
    private GameObject buildingPrefab;
    private BuildingStyle buildingStyle;
    private Vector3 position = Vector3.zero;

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

            BuildingCustomization customization = newBuilding.GetComponent<BuildingCustomization>();
            if (customization != null)
            {
                // Set the building height and color from the BuildingStyle
                if (buildingStyle != null)
                {
                    customization.buildingHeight = buildingStyle.height;
                    customization.buildingColor = buildingStyle.color;
                }
                customization.ApplyCustomization(); // Apply customization with the correct height and color
            }
            else
            {
                Debug.LogWarning("The selected prefab does not have a BuildingCustomization component.");
            }
        }
        else
        {
            Debug.LogWarning("No building prefab selected.");
        }
    }

}
