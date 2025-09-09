using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShotManager : MonoBehaviour
{
    public TMP_Text reportText;

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
    }
}