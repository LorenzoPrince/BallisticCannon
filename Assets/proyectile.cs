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

    public Text reportText;


    private float launchTime;

    public void SetLaunchTime(float time)
    {
        launchTime = time;
    }

    private void Start()
    {
        // Iniciar el tiempo de vuelo
        piecesDestroyed = 0;
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

        Debug.Log($"Tiempo de vuelo: {flightTime} segundos");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // Evitar múltiples colisiones
        hasCollided = true;

  
        impactPoint = collision.contacts[0].point;
        relativeVelocity = collision.relativeVelocity;
        collisionImpulse = collision.impulse.magnitude;

        Debug.Log($"Punto de impacto: {impactPoint}");
        Debug.Log($"Velocidad relativa: {relativeVelocity}");
        Debug.Log($"Impulso de colisión: {collisionImpulse}");

   
        if (collision.gameObject.CompareTag("Destructible"))
        {
            piecesDestroyed++;
            Debug.Log("¡Pieza derribada!");
            ShowReport();
            Destroy(gameObject, 0.1f);
        }

   
    }

}