using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMotionMCUA : MonoBehaviour
{
     // Referencia al script de movimiento
    public MovimientoCircularUniformeAcelerado movimientoScript;

    // Variable para manejar la desaceleración
    private bool slowingDown = false;

    // Velocidad inicial guardada para reiniciar
    private float initialVelocitySaved;

    void Start()
    {
        // Guardar la velocidad inicial
        initialVelocitySaved = movimientoScript.initialVelocity;
    }

    private void OnMouseDown()
    {
        if (!movimientoScript.isMoving)
        {
 
            movimientoScript.ResetState(initialVelocitySaved);
            movimientoScript.isMoving = true;
        }
        else 
        {
            Click();
        }
    }

    private void Click()
    {
    
        movimientoScript.SaveLastState();
              
    }
 
}
