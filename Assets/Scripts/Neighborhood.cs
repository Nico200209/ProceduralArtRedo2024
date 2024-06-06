using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    public BuildingStyle style;

    private void Start()
    {
        ApplyStyleToBuildings();
    }

    public void ApplyStyleToBuildings()
    {
        BuildingCustomization[] buildings = GetComponentsInChildren<BuildingCustomization>();
        foreach (var building in buildings)
        {
            building.ApplyStyle(style);
            Debug.Log("All styles applied to neighborhoods.");
        }
    }
}
