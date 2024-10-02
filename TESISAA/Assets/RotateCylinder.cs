using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    // Velocidad de rotación
    public float rotationSpeed = 100f;

    // Material que contiene el degradado
    public Material gradientMaterial;
    void Update()
    {
        // Rotar alrededor del eje Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime,
         0);
    }
}
