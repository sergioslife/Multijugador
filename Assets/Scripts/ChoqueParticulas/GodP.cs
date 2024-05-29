using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodP : MonoBehaviour
{
    // Variables serializadas que pueden configurarse desde el editor de Unity
    [SerializeField] float h = 0f; // Paso de tiempo para la simulación
    [SerializeField] float friction = 0f; // Coeficiente de fricción
    [SerializeField] float gravity = -9.8f; // Aceleración debida a la gravedad
    float distancia; // Variable para almacenar la distancia entre dos esferas

    // Array que contendrá los objetos con la etiqueta "Player"
    public GameObject[] Gobj;

    // Método Start() se llama antes del primer frame update
    void Start()
    {
        // Encuentra todos los objetos en la escena con la etiqueta "Player" y los almacena en Gobj
        Gobj = GameObject.FindGameObjectsWithTag("Player");

        // Desactiva el script para que no se ejecute hasta que se inicie la simulación
        enabled = false;
    }

    // Método Update() se llama una vez por frame
    void Update()
    {
        // Recorre todos los objetos en Gobj
        for (int i = 0; i < Gobj.Length; i++)
        {
            // Obtiene el componente BolaP del objeto actual
            BolaP BolaI = Gobj[i].GetComponent<BolaP>();
            // Actualiza el movimiento de la esfera usando el método Parabola
            BolaI.Parabola(friction, h, gravity);

            // Calcula el radio de la esfera actual
            float radio1 = BolaI.transform.localScale.x / 2;

            // Compara la esfera actual con las siguientes en el array para detectar colisiones
            for (int j = i + 1; j < Gobj.Length; j++)
            {
                // Obtiene el componente BolaP del siguiente objeto
                BolaP BolaJ = Gobj[j].GetComponent<BolaP>();

                // Calcula el radio de la siguiente esfera
                float radio2 = BolaJ.transform.localScale.x / 2;

                // Calcula la distancia entre las dos esferas
                distancia = Vector3.Distance(BolaI.transform.position, BolaJ.transform.position);

                // Si la distancia entre las esferas es menor o igual a la suma de sus radios, hay colisión
                if (distancia <= radio1 + radio2)
                {
                    // Llama al método Choque de ambas esferas para manejar la colisión
                    BolaI.Choque();
                    BolaJ.Choque();
                }
            }
        }
    }

    // Método para reiniciar la simulación
    public void ResetSimulation()
    {
        Start(); // Reinicia las variables iniciales como en el método Start
        foreach (var obj in Gobj)
        {
            BolaP bolaP = obj.GetComponent<BolaP>();
            if (bolaP != null)
            {
                bolaP.ResetBall(); // Asegúrate de que también este método exista en BolaP para resetear su estado
            }
        }
    }
}
