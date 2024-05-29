using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 95f; // Valor medio entre 50 y 100, ajustable para afinar la experiencia
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Capturar el movimiento del ratón, aplicando una atenuación de 75% al efecto de Time.deltaTime
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 0.95f;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 0.95f;

        // Restringir y aplicar la rotación en el eje X
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Aplicar la rotación local a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotar el cuerpo del jugador en el eje Y
        playerBody.Rotate(Vector3.up * mouseX);
    }
}


