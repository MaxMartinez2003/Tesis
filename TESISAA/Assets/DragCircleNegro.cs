using UnityEngine;

public class DragCircleNegro : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private LineRenderer lineRenderer;
    private Vector3 initialPosition; // Almacena la posición inicial del círculo
    private Vector3 lastPosition; // Almacena la última posición donde se soltó el ratón

    void Start()
    {
        // Obtén la cámara principal de la escena
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontró ninguna cámara principal en la escena.");
        }

        // Obtén el componente LineRenderer (asegúrate de añadirlo al objeto o a otro objeto)
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("No se encontró el componente LineRenderer en el objeto.");
        }
        else
        {
            // Configura los parámetros iniciales del LineRenderer
            lineRenderer.positionCount = 2; // Dos puntos (inicio y fin)
            lineRenderer.startWidth = 0.1f; // Ancho del cable
            lineRenderer.endWidth = 0.1f; // Ancho del cable
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Material básico
            lineRenderer.startColor = Color.black; // Color inicial
            lineRenderer.endColor = Color.black; // Color final

            // Desactiva el LineRenderer al inicio
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (mainCamera == null)
        {
            return;
        }

        // Verifica si el botón izquierdo del ratón está siendo presionado
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Asegúrate de que la posición esté en el plano 2D
            Collider2D collider = GetComponent<Collider2D>();

            if (collider.OverlapPoint(mousePosition))
            {
                isDragging = true;
                // Guarda la posición inicial del círculo
                initialPosition = transform.position;
                // Calcula el offset para que el objeto no se desplace abruptamente
                offset = transform.position - mousePosition;

                // Activa el LineRenderer y actualízalo
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, initialPosition); // Inicio del cable
                    lineRenderer.SetPosition(1, mousePosition); // Fin del cable
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Guarda la última posición cuando se suelta el ratón
            lastPosition = transform.position;

            // Desactiva el LineRenderer
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(1, lastPosition); // Fin del cable
            }
        }

        if (isDragging)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Asegúrate de que la posición esté en el plano 2D
            transform.position = mousePosition + offset;

            // Actualiza la posición del LineRenderer
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, initialPosition); // Inicio del cable
                lineRenderer.SetPosition(1, transform.position); // Fin del cable
            }
        }
        else if (lineRenderer != null)
        {
            // Actualiza la posición del LineRenderer para que muestre la última posición
            lineRenderer.SetPosition(0, initialPosition); // Inicio del cable
            lineRenderer.SetPosition(1, lastPosition); // Fin del cable
        }
    }
}
