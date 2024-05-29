using System.Collections.Generic;
using UnityEngine;

public class Cabello : MonoBehaviour
{
    [SerializeField] float k = 1.0f; // Constante del cabello (rigidez)
    [SerializeField] float restLength = 1.0f; // Longitud natural del cabello
    [SerializeField] float mass = 1.0f; // Masa del objeto
    [SerializeField] Vector3 velocity = Vector3.zero; // Velocidad inicial del objeto
    [SerializeField] List<Cabello> cabellosConectados = new List<Cabello>(); // Lista de cabellos conectados
    private bool isDragging = false; // Variables para mover el objeto con el ratón
    private Vector3 offset; // Offset del ratón
    private LineRenderer lineRenderer; // LineRenderer para visualizar la cuerda

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Configurar LineRenderer por defecto
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.useWorldSpace = true;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Actualizar la posición del objeto según la posición del ratón
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.z = transform.position.z;

            // Calcular el desplazamiento y mover los cabellos conectados
            Vector3 positionOffset = newPosition - transform.position;
            transform.position = newPosition;

            foreach (Cabello connectedCabello in cabellosConectados)
            {
                Vector3 connectedNewPosition = connectedCabello.transform.position + positionOffset;
                connectedNewPosition.z = connectedCabello.transform.position.z;
                connectedCabello.transform.position = connectedNewPosition;
            }
        }

        // Asegurarse de que la línea se actualice siempre
        UpdateLineRenderer();
    }

    public void Simulate(float h, float friction, float gravity)
    {
        Vector3 totalForce = Vector3.zero;

        foreach (Cabello connectedCabello in cabellosConectados)
        {
            totalForce += CalculateSpringForce(connectedCabello);
        }

        Vector3 acceleration = totalForce / mass;
        acceleration -= friction * velocity;
        acceleration += new Vector3(0, gravity, 0);

        velocity += acceleration * h;
        transform.position += velocity * h;

        if (transform.position.y < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            velocity.y = 0.0f;
        }

        UpdateLineRenderer();
    }

    Vector3 CalculateSpringForce(Cabello connectedCabello)
    {
        if (connectedCabello == null) return Vector3.zero;

        Vector3 displacement = transform.position - connectedCabello.transform.position;
        float currentLength = displacement.magnitude;
        Vector3 springForce = k * (restLength - currentLength) * displacement.normalized;

        return springForce;
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void UpdateLineRenderer()
    {
        if (lineRenderer != null && cabellosConectados.Count > 0)
        {
            foreach (Cabello connectedCabello in cabellosConectados)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, connectedCabello.transform.position);
            }
        }
    }
}
