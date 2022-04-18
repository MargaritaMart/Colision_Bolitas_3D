using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectScript : MonoBehaviour
{
    public float fMass, fSpeed, fRadius, restitucion = 0.8f;
    public Vector3 vPosition, vForces, vInitVelocity, vVelocity, vImpactForces;
    public bool bCollision;
    public bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        fMass = 1.0f;
        vPosition = this.transform.position;
        fSpeed = 0;
        vForces = Vector3.zero;
        if (this.tag != "Plano")
        {
            fRadius = this.transform.localScale.x / 2;
        }
        else
        {
            fRadius = 0.0f;
        }
        vVelocity = vInitVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            // calcular fuerzas
            CalcForces();
            // actualizar posicion del objeto
            UpdateBody(Time.deltaTime);
        }
        else
        {
            this.transform.position = vPosition;
            vVelocity = vInitVelocity;
        }
    }

    //calculo de las fuerzas del sistema
    void CalcForces()
    {
        //reset forces
        vForces = Vector3.zero;
        if (bCollision)
        {
            //añade al nuevo calculo la fuerza del impacto
            vForces += vImpactForces;
            bCollision = false;
            vImpactForces = Vector3.zero;
        }
    }

    void UpdateBody(float dt)
    {
        Vector3 a, dv, ds;

        //calculo de la aceleracion
        a = vForces / fMass;

        // calculo de la varicion de la velocidad como derivada
        // de la aceleracion
        dv = a * dt;
        // nueva velocidad
        vVelocity += dv;

        // calculo de la varicion de la posicion como derivada
        // de la velocidad
        ds = vVelocity * dt;
        // nueva posicion
        vPosition += ds;

        // velocidad total del sistema
        fSpeed = vVelocity.magnitude;
        // actualiza la posiciopn del objeto
        this.transform.position = vPosition;
    }
}
