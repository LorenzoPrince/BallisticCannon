using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class protectile : MonoBehaviour
{
    private float flightTime; // Tiempo de vuelo
    private Vector3 impactPoint; // Punto de impacto
    private Vector3 relativeVelocity; // Velocidad relativa
    private float collisionImpulse; // Impulso de colisión
    private bool hasCollided = false; // Para evitar múltiples colisiones

    private static int piecesDestroyed = 0;

    private ShotManager shotManager;




    private void Start()
    {
        // Iniciar el tiempo de vuelo
        piecesDestroyed = 0;
        shotManager = FindObjectOfType<ShotManager>();
        StartCoroutine(TrackFlightTime());
    }

    private IEnumerator TrackFlightTime()
    {
        flightTime = 0f;

        while (!hasCollided)
        {
            flightTime += Time.deltaTime;
            yield return null;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;
        hasCollided = true;

        impactPoint = collision.contacts[0].point;
        relativeVelocity = collision.relativeVelocity;
        collisionImpulse = collision.impulse.magnitude;

        if (collision.gameObject.CompareTag("Destructible"))
        {
            piecesDestroyed++;
        }

        if (shotManager != null)
        {
            shotManager.ShowReport(
                flightTime,
                impactPoint,
                relativeVelocity.magnitude,
                collisionImpulse,
                piecesDestroyed
            );
        }

        Destroy(gameObject, 0.1f);
    }
}