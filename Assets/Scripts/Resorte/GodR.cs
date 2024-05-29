using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

public class GodR : MonoBehaviour
{
    public float h;
    public float friction;
    public float gravity;
    public List<Resorte> resortes = new List<Resorte>();

    void Start()
    {
        Resorte[] resortesEnEscena = FindObjectsOfType<Resorte>();
        resortes.AddRange(resortesEnEscena);
    }

    void Update()
    {
        foreach (Resorte resorte in resortes)
        {
            resorte.Simulate(h, friction, gravity);
        }
    }
}
