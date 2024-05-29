using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCircularUniformeAcelerado : MonoBehaviour
{
    // Velocidad inicial de rotación circular
    public float initialVelocity = 0.1f;

    // Aceleración angular de la rotación circular
    public float angularAcceleration = 0.001f;

    // Tiempo transcurrido desde el inicio del movimiento
    public float time = 0.0f;

    // Radio del círculo en el que se moverá el objeto
    public float radius = 5.0f;

    Quaternion LastRotation;

    // Posición inicial del objeto en el círculo
    public Vector3 initialPosition = Vector3.zero;

    // Posición actual del objeto en el círculo
    private Vector3 position;

    // Guardar el último estado
    private float lastTime = 0.0f;
    private Vector3 lastPosition = Vector3.zero;

    // Variable para verificar si el objeto está en movimiento
    public bool isMoving = false;

    void Start()
    {
        CalculatePosition();
        transform.position = position;
    }

    void Update()
    {
        if (!isMoving) return;

        CalculatePosition();
        transform.position = position;
        transform.Rotate(0, initialVelocity * Time.deltaTime * Mathf.Rad2Deg, 0, Space.World);

        time += Time.deltaTime;
    }

    void CalculatePosition()
    {
        position = initialPosition + new Vector3(
            radius * Mathf.Cos(initialVelocity * time + 0.5f * angularAcceleration * time * time),
            0.0f,
            radius * Mathf.Sin(initialVelocity * time + 0.5f * angularAcceleration * time * time)
        );
    }

    public void SaveLastState()
    {
        initialVelocity = 0;
        isMoving = false;
        lastPosition = transform.position; // Guardar la posición actual
        LastRotation = transform.rotation;
    }

    public void ResetState(float newInitialVelocity)
{
    initialVelocity = newInitialVelocity;
    transform.position = lastPosition; // Reiniciar la posición a la última guardada
    transform.rotation = LastRotation;
    time = 0.0f; // Reiniciar el tiempo
}


}
