using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSimulationCaidaLibre1 : MonoBehaviour
{
    public GodC1 simulationScript; // Referencia al script de la simulación

    void OnMouseDown()
    {
        // Verifica si el script de simulación está asignado
        if (simulationScript == null)
        {
            Debug.LogError("Simulation script not assigned in the Inspector");
            return;
        }

        // Activar el script para comenzar la simulación
        simulationScript.enabled = true;

        // Reinicia todos los parámetros necesarios en el script GodC
        

        // Para asegurarse de que la simulación pueda reiniciarse, desactivamos y reactivamos el script
        simulationScript.enabled = false;
        simulationScript.enabled = true;
    }
}