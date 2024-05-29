using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaP : MonoBehaviour
{
    // Variables serializadas configurables desde el editor de Unity
    [SerializeField] Vector3 PosInicial; // Posición inicial de la esfera
    [SerializeField] Vector3 VelInicial; // Velocidad inicial de la esfera
    [SerializeField] float angulo = 0.0f; // Ángulo de lanzamiento en grados
    [SerializeField] float masa = 10.0f; // Masa de la esfera
    [SerializeField] float dampingFactor; // Factor de amortiguación

    // Variables internas para la posición, velocidad y fuerza actuales
    Vector3 Pos;
    Vector3 Vel;
    Vector3 Fuerza;

    private Boolean collision; // Indicador de colisión con el suelo

    // Método Start() se llama antes del primer frame update
    void Start()
    {
        // Inicializa la posición y velocidad de la esfera
        Pos = PosInicial;
        Vel = VelInicial;
        collision = false; // Inicialmente no hay colisión
        launchProject(); // Calcula las componentes iniciales de la velocidad
        
        // Desactiva el script para que no se ejecute hasta que se inicie la simulación
        enabled = false;
    }

    // Convierte el ángulo a radianes y ajusta las componentes x e y de la velocidad inicial
    public void launchProject()
    {
        angulo *= Mathf.Deg2Rad; // Convierte el ángulo a radianes
        Vel.y = Mathf.Sin(angulo) * Vel.magnitude; // Componente y de la velocidad
        Vel.x = Mathf.Cos(angulo) * Vel.magnitude; // Componente x de la velocidad
    }

    // Calcula la fuerza neta que actúa sobre la esfera
    public void calcularFuerza(float gravity, float friction)
    {
        Vector3 g; // Fuerza gravitacional
        Vector3 b; // Fuerza de fricción

        // Si hay colisión, invierte la componente y de la velocidad y aplica el factor de amortiguación
        if (collision)
        {
            Vel.y = Vel.y * (-1f * dampingFactor);
        }

        g.x = 0;
        g.y = masa * gravity; // Fuerza gravitacional en y
        g.z = 0;

        b = masa * friction * Vel; // Fuerza de fricción

        // Fuerza neta es la suma de la fuerza gravitacional y la fuerza de fricción
        Fuerza = b + g;
    }

    // Verifica si la esfera está en contacto con el suelo y actualiza el estado de colisión
    public void colision()
    {
        collision = false;

        // Si la posición y de la esfera es menor o igual al radio, hay colisión con el suelo
        if (Pos.y <= transform.localScale.x / 2)
        {
            collision = true;
        }
    }

    // Calcula la nueva velocidad tras una colisión con otra esfera
    public void Choque()
    {
        // Calcula los ángulos de la velocidad en los planos xy y zx
        float anguloxy = Mathf.Atan2(Vel.y, Vel.x);
        float angulozx = Mathf.Atan2(Vel.z, Vel.x);

        // Invierte los ángulos para simular la reflexión de la colisión
        anguloxy = Mathf.PI - anguloxy;
        angulozx = Mathf.PI - angulozx;

        // Calcula la nueva magnitud de la velocidad considerando el factor de amortiguación
        float speed = Vel.magnitude * dampingFactor;

        // Calcula la nueva velocidad en función de los ángulos ajustados y la nueva magnitud
        Vector3 NewVel = new Vector3(Mathf.Cos(anguloxy), Mathf.Sin(anguloxy), Mathf.Sin(angulozx)) * speed;

        // Actualiza la velocidad con los nuevos valores
        Vel = NewVel; 
    }

    // Actualiza la posición y velocidad de la esfera basándose en la física del movimiento parabólico
    public void Parabola(float friction, float h, float gravity)
    {
        calcularFuerza(gravity, friction); // Calcula la fuerza neta
        Vector3 a = Fuerza / masa; // Calcula la aceleración
        Vel += (h * a); // Actualiza la velocidad
        Pos += (Vel * h); // Actualiza la posición

        colision(); // Verifica y maneja la colisión con el suelo
        carga(); // Actualiza la posición del objeto en la escena
    }

    // Método para reiniciar el estado de la esfera
    public void ResetBall()
    {
        Pos = PosInicial;
        Vel = VelInicial;
        collision = false; // Estado de colisión
        // Otros reinicios necesarios
    }

    // Actualiza la posición del objeto en la escena
    public void carga()
    {
        transform.position = Pos; // Establece la posición del objeto en el mundo
    }
}
