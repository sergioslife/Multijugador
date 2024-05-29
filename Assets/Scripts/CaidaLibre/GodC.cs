using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodC : MonoBehaviour
{
    // Variables serializadas que pueden configurarse desde el editor de Unity
    [SerializeField] float h = 0f; // Paso de tiempo para la simulación
    [SerializeField] float gravity = -9.8f; // Aceleración debida a la gravedad
    float distancia; // Variable para almacenar la distancia entre dos esferas

    // Array que contendrá los objetos con la etiqueta "Apple"
    public GameObject[] Gobj;

    // Método Start() se llama antes del primer frame update
    void Start()
    {
        // Encuentra todos los objetos en la escena con la etiqueta "Apple" y los almacena en Gobj
        Gobj = GameObject.FindGameObjectsWithTag("Apple");

        // Desactiva el script para que no se ejecute hasta que se inicie la simulación
        enabled = false;
    }

    // Método Update() se llama una vez por frame
    void Update()
    {
        // Recorre todos los objetos en Gobj
        for (int i = 0; i < Gobj.Length; i++)
        {
            // Obtiene el componente CaidaLibre del objeto actual
            CaidaLibre BolaI = Gobj[i].GetComponent<CaidaLibre>();
            // Actualiza el movimiento de la esfera usando el método Parabola
            BolaI.Parabola(h, gravity);

            // Calcula el radio de la esfera actual
            float radio1 = BolaI.transform.localScale.x / 2;

            // Compara la esfera actual con las siguientes en el array para detectar colisiones
            for (int j = i + 1; j < Gobj.Length; j++)
            {
                // Obtiene el componente CaidaLibre del siguiente objeto
                CaidaLibre BolaJ = Gobj[j].GetComponent<CaidaLibre>();

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
    
}
