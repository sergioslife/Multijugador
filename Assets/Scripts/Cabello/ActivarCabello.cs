using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarCabello : MonoBehaviour
{
    private GodCabello godCabello;

    void Start()
    {
        // Encuentra el script GodCabello en la escena
        godCabello = FindObjectOfType<GodCabello>();
    }

    void OnMouseDown()
    {
        // Si el script GodCabello existe, activa la simulación
        if (godCabello != null)
        {
            godCabello.ActivateSimulation();
        }
    }
}
