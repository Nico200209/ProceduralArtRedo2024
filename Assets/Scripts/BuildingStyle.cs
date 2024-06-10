using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStyle", menuName = "City17/BuildingStyle")]
public class BuildingStyle : ScriptableObject
{
    public float height;
    public Color color;
    public GameObject stockPrefab;
    public GameObject roofPrefab;
}
