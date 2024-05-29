using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMCUA : MonoBehaviour
{
    // Referencia al GameObject de la esfera
    public GameObject sphere;

    // Tiempo personalizado que se incrementará manualmente
    private float customTime = 0.0f;
    // Incremento de tiempo por frame (ajustado para que el movimiento sea más lento)
    public float timeIncrement = 0.001f; // Reducido para un movimiento más lento

    // Método Update que se llama en cada frame
    void Update()
    {
        // Obtener el componente MovimientoCircularUniformeAcelerado del GameObject de la esfera
        MovimientoCircularUniformeAcelerado script = sphere.GetComponent<MovimientoCircularUniformeAcelerado>();

        // Verificar si el componente existe
        if (script != null)
        {
            // Incrementar el tiempo personalizado
            customTime += timeIncrement;

            // Asignar el tiempo personalizado al script de la esfera
            script.time = customTime;
        }
    }
}
