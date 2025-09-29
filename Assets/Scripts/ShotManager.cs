using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShotManager : MonoBehaviour
{
    public TMP_Text reportText;
    private Cannon cannon; // Referencia al cañón

    private void Start()
    {
        cannon = FindObjectOfType<Cannon>();
    }

    public void ShowReport(float flightTime, Vector3 impactPoint, float relativeVelocity, float impulse, int piecesDestroyed)
    {
        string report = $"<b>REPORTE DE TIRO</b>\n" +
                        $"- Tiempo de vuelo: {flightTime:F2} s\n" +
                        $"- Impacto: {impactPoint}\n" +
                        $"- Vel. relativa: {relativeVelocity:F2} m/s\n" +
                        $"- Impulso: {impulse:F2} N·s\n" +
                        $"- Obj. Collisionados: {piecesDestroyed}\n" +
                        $"- Puntuacion: {Mathf.RoundToInt(relativeVelocity * piecesDestroyed)}";

        reportText.text = report;
        SaveShotToFirebase(impactPoint, piecesDestroyed);
    }

    private void SaveShotToFirebase(Vector3 impactPoint, int piecesDestroyed)
    {
        if (cannon == null)
        {
            Debug.LogWarning("Cannon no encontrado para tomar datos del disparo.");
            return;
        }
        if (FirebaseManager.Instance == null)
        {
            Debug.LogWarning("FirebaseManager no inicializado.");
            return;
        }

        float force = cannon.Force; // Propiedad pública que expondrás (abajo te explico)
        float angle = cannon.AngleX; // También lo expondrás
        float mass = 1f;

        // Buscar el proyectil actual
        GameObject lastProjectile = GameObject.FindGameObjectWithTag("Projectile"); // Asegurate de que tus proyectiles tengan el tag "Projectile"
        if (lastProjectile != null)
        {
            Rigidbody rb = lastProjectile.GetComponent<Rigidbody>();
            if (rb != null) mass = rb.mass;
        }

        // Calcular distancia desde el cañón
        float distance = Vector3.Distance(cannon.transform.position, impactPoint);
        bool hit = piecesDestroyed > 0;

        ShotResult shot = new ShotResult(angle, force, mass, hit, distance, piecesDestroyed);

        FirebaseManager.Instance.SaveShot(shot);
    }
}

