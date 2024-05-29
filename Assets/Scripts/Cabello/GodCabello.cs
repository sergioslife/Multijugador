using System.Collections.Generic;
using UnityEngine;

public class GodCabello : MonoBehaviour
{
    public float h = 0.02f; // Paso de tiempo
    public float friction = 0.1f; // Fricción
    public float gravity = -9.81f; // Gravedad
    public List<Cabello> cabellos = new List<Cabello>();
    private bool isSimulationActive = false; // Variable para controlar el estado de la simulación

    void Start()
    {
        // Encuentra todos los objetos Cabello en la escena y los añade a la lista
        Cabello[] cabellosEnEscena = FindObjectsOfType<Cabello>();
        cabellos.AddRange(cabellosEnEscena);
    }

    void Update()
    {
        if (isSimulationActive)
        {
            // Actualiza cada objeto Cabello en cada frame
            foreach (Cabello cabello in cabellos)
            {
                cabello.Simulate(h, friction, gravity);
            }
        }
    }

    // Método para activar la simulación
    public void ActivateSimulation()
    {
        isSimulationActive = true;
    }
}
