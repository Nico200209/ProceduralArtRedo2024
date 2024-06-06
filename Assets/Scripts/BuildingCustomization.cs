using UnityEngine;

public class BuildingCustomization : MonoBehaviour
{
    public float buildingHeight = 0f;
    public Color buildingColor = Color.gray;

    private void Start()
    {
        ApplyCustomization();
    }

    public void ApplyCustomization()
    {
        transform.localScale = new Vector3(transform.localScale.x, buildingHeight, transform.localScale.z);
        Renderer renderer = GetComponent<Renderer>();
        if (renderer)
        {
            renderer.material.color = buildingColor;
        }
        Debug.Log("Customization applied. Height: " + buildingHeight + ", Color: " + buildingColor);
    }

    public void ApplyStyle(BuildingStyle style)
    {
        Debug.Log("Applying style. Height: " + style.height + ", Color: " + style.color);
        buildingHeight = style.height;
        buildingColor = style.color;
        ApplyCustomization();
    }
}
