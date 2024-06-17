using UnityEngine;

namespace Demo
{
    public class SimpleBuilding : Shape
    {
        public int buildingHeight = -1; // The total building height (=#stocks) - value should be the same for all stocks
        public float stockHeight = 1; // The height of one stock. Change this value depending on the height of your stock prefabs
        public float foundationHeight = 0.5f; // The height of the foundation block
        // If buildingHeight is negative, a random building height will be chosen between these two limits:
        public int maxHeight = 5;
        public int minHeight = 1;

        public GameObject foundationPrefab; // Your foundation prefab
        public GameObject[] stockPrefabs; // Your stock prefabs (make sure they all have the same height)
        public GameObject[] roofPrefabs; // Your roof prefabs (may have different height)

        public BuildingStyle buildingStyle; // Reference to the building style
        private int stockNumber = 0; // The number of stocks that have already been spawned below this

        public void Initialize(int pBuildingHeight, float pStockHeight, int pStockNumber, GameObject[] pStockPrefabs, GameObject[] pRoofPrefabs)
        {
            buildingHeight = pBuildingHeight;
            stockHeight = pStockHeight;
            stockNumber = pStockNumber;
            stockPrefabs = pStockPrefabs;
            roofPrefabs = pRoofPrefabs;
        }

        // Returns a random game object chosen from a given gameobject array
        GameObject ChooseRandom(GameObject[] choices)
        {
            int index = Random.Range(0, choices.Length);
            return choices[index];
        }

        protected override void Execute()
        {
            if (buildingStyle != null)
            {
                Debug.Log("Applying building style. Height: " + buildingStyle.height + ", Stock Prefab: " + buildingStyle.stockPrefab.name + ", Roof Prefab: " + buildingStyle.roofPrefab.name);
                stockPrefabs = new GameObject[] { buildingStyle.stockPrefab };
                roofPrefabs = new GameObject[] { buildingStyle.roofPrefab };
                stockHeight = buildingStyle.height;
            }

            if (buildingHeight < 0)
            { // This is only done once for the root symbol
                buildingHeight = Random.Range(minHeight, maxHeight + 1);
            }

            if (stockNumber == 0)
            {
                // Spawn the foundation block
                GameObject foundation = SpawnPrefab(foundationPrefab);

                // Update the customization component
                BuildingCustomization customization = foundation.GetComponent<BuildingCustomization>();
                if (customization != null)
                {
                    customization.buildingHeight = foundationHeight;
                    customization.buildingColor = buildingStyle.color;
                    customization.ApplyCustomization();
                }

                // Create the rest of the building above the foundation
                SimpleBuilding remainingBuilding = CreateSymbol<SimpleBuilding>("stock", new Vector3(0, foundationHeight, 0));
                remainingBuilding.Initialize(buildingHeight, stockHeight, stockNumber + 1, stockPrefabs, roofPrefabs);
                remainingBuilding.Generate(buildDelay);
            }
            else if (stockNumber < buildingHeight)
            {
                // First spawn a new stock...
                GameObject newStock = SpawnPrefab(ChooseRandom(stockPrefabs));

                // Update the customization component
                BuildingCustomization customization = newStock.GetComponent<BuildingCustomization>();
                if (customization != null)
                {
                    customization.buildingHeight = stockHeight;
                    customization.buildingColor = buildingStyle.color;
                    customization.ApplyCustomization();
                }

                // ...and then continue with the remainder of the building, right above the spawned stock:
                // Create a new symbol - make sure to increase the y-coordinate:
                SimpleBuilding remainingBuilding = CreateSymbol<SimpleBuilding>("stock", new Vector3(0, stockHeight, 0));
                // Pass the parameters to the new symbol (component), but increase the stockNumber:
                remainingBuilding.Initialize(buildingHeight, stockHeight, stockNumber + 1, stockPrefabs, roofPrefabs);
                // ...and continue with the shape grammar:
                remainingBuilding.Generate(buildDelay);
            }
            else
            {
                // Spawn a roof and stop:
                GameObject newRoof = SpawnPrefab(ChooseRandom(roofPrefabs));

                // Update the customization component
                BuildingCustomization customization = newRoof.GetComponent<BuildingCustomization>();
                if (customization != null)
                {
                    customization.buildingHeight = stockHeight;
                    customization.buildingColor = buildingStyle.color;
                    customization.ApplyCustomization();
                }
            }
        }
    }
}
