using UnityEngine;

namespace Demo
{
    public class TriangularCity : MonoBehaviour
    {
        public float triangleSize = 50f; // Size of the triangle from vertex to vertex
        public float buildingSpacing = 10f; // Spacing between buildings
        public GameObject[] buildingPrefabs;
        public BuildingStyle buildingStyle;

        private void Start()
        {
            GenerateTriangularCity();
        }

        private void GenerateTriangularCity()
        {
            // Define the vertices of the triangle
            Vector3 vertex1 = new Vector3(0, 0, 0);
            Vector3 vertex2 = new Vector3(triangleSize, 0, 0);
            Vector3 vertex3 = new Vector3(triangleSize / 2f, 0, triangleSize * Mathf.Sqrt(3) / 2f);

            // Generate buildings along each edge of the triangle
            GenerateBuildingsAlongEdge(vertex1, vertex2);
            GenerateBuildingsAlongEdge(vertex2, vertex3);
            GenerateBuildingsAlongEdge(vertex3, vertex1);

            // Generate streets between vertices
            GenerateStreetBetween(vertex1, vertex2);
            GenerateStreetBetween(vertex2, vertex3);
            GenerateStreetBetween(vertex3, vertex1);
        }

        private void GenerateBuildingsAlongEdge(Vector3 start, Vector3 end)
        {
            Vector3 direction = (end - start).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            float distance = Vector3.Distance(start, end);
            int numBuildings = Mathf.RoundToInt(distance / buildingSpacing);

            for (int i = 0; i < numBuildings; i++)
            {
                Vector3 position = start + direction * (i * buildingSpacing + buildingSpacing / 2f);
                SpawnBuilding(position, rotation);
            }
        }

        private void GenerateStreetBetween(Vector3 start, Vector3 end)
        {
            Vector3 direction = (end - start).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            float distance = Vector3.Distance(start, end);
            int numBuildings = Mathf.RoundToInt(distance / buildingSpacing);

            for (int i = 0; i < numBuildings; i++)
            {
                Vector3 position = start + direction * (i * buildingSpacing + buildingSpacing / 2f);
                SpawnBuilding(position, rotation);
            }
        }

        private void SpawnBuilding(Vector3 position, Quaternion rotation)
        {
            if (buildingPrefabs.Length == 0)
            {
                Debug.LogWarning("No building prefabs assigned.");
                return;
            }

            GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
            GameObject building = Instantiate(buildingPrefab, position, rotation, transform);

            // Apply building customization
            BuildingCustomization customization = building.GetComponent<BuildingCustomization>();
            if (customization != null && buildingStyle != null)
            {
                customization.ApplyStyle(buildingStyle);
            }
            else
            {
                Debug.LogWarning("BuildingCustomization component or buildingStyle not assigned.");
            }

            building.transform.localScale = Vector3.one;
        }
    }
}
