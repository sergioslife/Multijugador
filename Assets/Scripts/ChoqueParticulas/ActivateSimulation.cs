using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSimulation : MonoBehaviour
{
    public GodP simulationScript; // Referencia al script de la simulación
    public AudioSource audioSource; // Referencia al componente AudioSource

    void OnMouseDown()
    {
        // Verifica si el script de simulación está asignado
        if (simulationScript == null)
        {
            Debug.LogError("Simulation script not assigned in the Inspector");
            return;
        }

        // Verifica si el componente AudioSource está asignado
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not assigned in the Inspector");
            return;
        }

        // Reproduce el audio
        audioSource.Play();

        // Activar el script para comenzar la simulación
        simulationScript.enabled = true;

        // Reinicia todos los parámetros necesarios en el script GodP
        simulationScript.ResetSimulation();

        // Para asegurarse de que la simulación pueda reiniciarse, desactivamos y reactivamos el script
        simulationScript.enabled = false;
        simulationScript.enabled = true;
    }
}
