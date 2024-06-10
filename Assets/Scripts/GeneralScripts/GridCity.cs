using UnityEngine;
using System.Collections;


namespace Demo
{
    public class GridCity : MonoBehaviour
    {
        public int neighborhoodRows = 2;
        public int neighborhoodColumns = 2;
        public int neighborhoodRowWidth = 100;
        public int neighborhoodColumnWidth = 100;

        public int buildingsPerNeighborhoodRows = 10;
        public int buildingsPerNeighborhoodColumns = 10;
        public int buildingRowWidth = 10;
        public int buildingColumnWidth = 10;
        public GameObject[] buildingPrefabs;

        public BuildingStyle[] neighborhoodStyles;

        private NeighborhoodStyleManager styleManager;

        void Start()
        {
            styleManager = FindObjectOfType<NeighborhoodStyleManager>();
            GenerateCity();
            StartCoroutine(ApplyStylesWithDelay()); // Call the function with a delay
        }

        IEnumerator ApplyStylesWithDelay()
        {
            // Wait for a short delay to allow all buildings to be instantiated and initialized
            yield return new WaitForSeconds(0.1f);
            styleManager.ApplyStylesToNeighborhoods(); // Apply styles after a short delay
        }

        public void GenerateCity()
        {
            for (int row = 0; row < neighborhoodRows; row++)
            {
                for (int col = 0; col < neighborhoodColumns; col++)
                {
                    GenerateNeighborhood(row, col);
                }
            }
        }

        void GenerateNeighborhood(int row, int col)
        {
            // Create a new neighborhood GameObject
            GameObject neighborhoodObject = new GameObject("Neighborhood_" + row + "_" + col);
            neighborhoodObject.transform.parent = transform;
            neighborhoodObject.transform.localPosition = new Vector3(col * neighborhoodColumnWidth, 0, row * neighborhoodRowWidth);

            // Add Neighborhood component and assign style
            Neighborhood neighborhood = neighborhoodObject.AddComponent<Neighborhood>();
            neighborhood.style = neighborhoodStyles[Random.Range(0, neighborhoodStyles.Length)];

            // Generate buildings within this neighborhood
            for (int buildingRow = 0; buildingRow < buildingsPerNeighborhoodRows; buildingRow++)
            {
                for (int buildingCol = 0; buildingCol < buildingsPerNeighborhoodColumns; buildingCol++)
                {
                    int buildingIndex = Random.Range(0, buildingPrefabs.Length);
                    GameObject newBuilding = Instantiate(buildingPrefabs[buildingIndex], neighborhoodObject.transform);

                    // Ensure the new building has a BuildingCustomization component
                    if (newBuilding.GetComponent<BuildingCustomization>() == null)
                    {
                        newBuilding.AddComponent<BuildingCustomization>();
                    }

                    // Place it in the grid
                    newBuilding.transform.localPosition = new Vector3(buildingCol * buildingColumnWidth, 0, buildingRow * buildingRowWidth);
                }
            }

            // Apply style to buildings in this neighborhood
            neighborhood.ApplyStyleToBuildings();
        }
    }
}
