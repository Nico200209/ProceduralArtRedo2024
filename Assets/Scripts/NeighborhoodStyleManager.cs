using Demo;
using UnityEngine;

public class NeighborhoodStyleManager : MonoBehaviour
{
    public Neighborhood[] neighborhoods;

    public GridCity gridCity;

    public void ApplyStylesToNeighborhoods()
    {
        foreach (var neighborhood in neighborhoods)
        {
            if (neighborhood != null && neighborhood.style != null)
            {
                neighborhood.ApplyStyleToBuildings();
            }
        }


        if (gridCity != null)
        {
            gridCity.GenerateCity();
        }
    }
}
