using UnityEngine;

public class NeighborhoodStyleManager : MonoBehaviour
{
    public BuildingStyle[] styles;

    void Start()
    {
        if (styles.Length > 0)
        {
            Debug.Log("Applying style: " + styles[0].name);
            ApplyStyleToBuildings(styles[0]); // Apply the first style in the array
        }
        else
        {
            Debug.LogError("No styles assigned in NeighborhoodStyleManager.");
        }
    }

    public void ApplyStyleToBuildings(BuildingStyle style)
    {
        BuildingCustomization[] buildings = FindObjectsOfType<BuildingCustomization>();
        Debug.Log("Found " + buildings.Length + " buildings to apply style to.");
        foreach (var building in buildings)
        {
            Debug.Log("Applying style to building: " + building.gameObject.name);
            building.ApplyStyle(style);
        }
    }
}
