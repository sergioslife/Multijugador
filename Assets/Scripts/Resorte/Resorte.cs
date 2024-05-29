using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resorte : MonoBehaviour
{
    // Constante de resorte (rigidez)
    [SerializeField] float k;

    // Longitud natural del resorte
    [SerializeField] float restLength;

    // Objeto al que está conectado el resorte
    [SerializeField] Resorte objResorte;

    // Masa del objeto
    [SerializeField] float mass;

    // Velocidad inicial del objeto
    [SerializeField] Vector3 velocity = Vector3.zero;

    // Lista de resortes conectados
    [SerializeField] List<Resorte> resortesConectados = new List<Resorte>();

    // Variables para mover el objeto con el ratón
    private bool isDragging = false;
    private Vector3 offset;

    // Método para simular el comportamiento del resorte
    public void Simulate(float h, float friction, float gravity)
    {
        // Si no hay objeto resorte conectado, no hacer nada
        if (!objResorte) return;

        // Calcular la fuerza del resorte
        Vector3 force = CalculateSpringForce();

        // Calcular la aceleración (F = ma)
        Vector3 acceleration = force / mass;

        // Aplicar la fricción
        acceleration -= friction * velocity;

        // Añadir la aceleración de la gravedad
        acceleration += new Vector3(0, gravity, 0);

        // Actualizar la velocidad y la posición del objeto
        velocity += acceleration * h;
        transform.position += velocity * h;

        // Limitar la posición del objeto para que no pase por debajo del plano y = 0
        if (transform.position.y < 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            velocity.y = 0.0f;
        }
    }

    // Método para calcular la fuerza del resorte
    Vector3 CalculateSpringForce()
    {
        // Si no hay objeto resorte conectado, retornar vector cero
        if (!objResorte) return Vector3.zero;

        Vector3 totalForce = Vector3.zero;
        Vector3 displacement;

        // Calcular el desplazamiento entre el objeto y el resorte conectado
        displacement = transform.position - objResorte.transform.position;

        // Longitud actual del resorte
        float currentLength = displacement.magnitude;

        // Ley de Hooke (F = -k * (longitud actual - longitud natural))
        Vector3 springForce = k * (restLength - currentLength) * displacement.normalized;
        totalForce += springForce;

        // Si hay resortes conectados, añadir su fuerza también
        if (objResorte != null)
        {
            Vector3 forceFromConnectes = CalculateForceFromConnected(objResorte);
            totalForce += forceFromConnectes;
        }

        return totalForce;
    }

    // Método para calcular la fuerza de los resortes conectados
    Vector3 CalculateForceFromConnected(Resorte connectedResorte)
    {
        Vector3 displacement = transform.position - connectedResorte.transform.position;
        float currentLength = displacement.magnitude;
        Vector3 springForce = k * (restLength - currentLength) * displacement.normalized;

        return springForce;
    }

    // Método llamado cuando el ratón se presiona sobre el objeto
    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        offset = transform.position - GetMouseWorldPosition();
    }

    // Método llamado cuando el ratón se suelta
    private void OnMouseUp()
    {
        isDragging = false;
    }

    // Método llamado en cada frame
    private void Update()
    {
        if (isDragging)
        {
            // Actualizar la posición del objeto según la posición del ratón
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            newPosition.z = transform.position.z;

            // Calcular el desplazamiento y mover los resortes conectados
            Vector3 positionOffset = newPosition - transform.position;

            transform.position = newPosition;

            foreach (Resorte connectedResorte in resortesConectados)
            {
                Vector3 connectedNewPosition = connectedResorte.transform.position + positionOffset;
                connectedNewPosition.z = connectedResorte.transform.position.z;
                connectedResorte.transform.position = connectedNewPosition;
            }
        }
    }

    // Método para obtener la posición del ratón en el mundo
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
