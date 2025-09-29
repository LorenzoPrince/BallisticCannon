using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShotResult
{
    public float angle;           // �ngulo vertical del ca��n (eje X)
    public float force;           // Fuerza de disparo
    public float mass;            // Masa del proyectil
    public bool hit;              // Si impact� algo relevante
    public float distance;        // Distancia al impacto desde el ca��n
    public int objectsAffected;   // Cantidad de objetos destruidos
    public long timestamp;        // Momento del disparo (en epoch ms)

    public ShotResult(float angle, float force, float mass, bool hit, float distance, int objectsAffected)
    {
        this.angle = angle;
        this.force = force;
        this.mass = mass;
        this.hit = hit;
        this.distance = distance;
        this.objectsAffected = objectsAffected;
        this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}