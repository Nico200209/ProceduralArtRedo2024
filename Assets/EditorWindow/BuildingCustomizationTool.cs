using UnityEditor;
using UnityEngine;

public class BuildingCustomizationTool : EditorWindow
{
    private GameObject building;
    private float height;
    private Color color;

    [MenuItem("Tools/Building Customization Tool")]
    public static void ShowWindow()
    {
        GetWindow<BuildingCustomizationTool>("Building Customization Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Customize Building", EditorStyles.boldLabel);

        building = (GameObject)EditorGUILayout.ObjectField("Building", building, typeof(GameObject), true);
        height = EditorGUILayout.FloatField("Height", height);
        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("Apply Customization"))
        {
            ApplyCustomization();
        }
    }

    private void ApplyCustomization()
    {
        if (building)
        {
            BuildingCustomization customization = building.GetComponent<BuildingCustomization>();
            if (customization)
            {
                customization.buildingHeight = height;
                customization.buildingColor = color;
                customization.ApplyCustomization();
            }
            else
            {
                Debug.LogWarning("No BuildingCustomization component found on the selected building.");
            }
        }
        else
        {
            Debug.LogWarning("No building selected.");
        }
    }
}
