using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResultadosUI : MonoBehaviour
{

    public GameObject panelResultados;
    public TMP_Text resultadosText;
    public Button btnVerResultados;
    public Button btnCerrar;
    public Button btnAnterior;
    public Button btnSiguiente;

    private List<ShotResult> resultados = new List<ShotResult>();
    private int indiceActual = 0;

    private void Start()
    {
        panelResultados.SetActive(false);

        btnVerResultados.onClick.AddListener(CargarResultados);
        btnCerrar.onClick.AddListener(() => panelResultados.SetActive(false));
        btnAnterior.onClick.AddListener(MostrarAnterior);
        btnSiguiente.onClick.AddListener(MostrarSiguiente);

        btnAnterior.interactable = false;
        btnSiguiente.interactable = false;
    }

    private void CargarResultados()
    {
        panelResultados.SetActive(true);
        resultadosText.text = "Cargando resultados...\n";

        FirebaseManager.Instance.GetAllShots((success, results) =>
        {
            if (!success || results == null || results.Count == 0)
            {
                resultadosText.text = "Error al obtener los resultados o no hay datos.";
                btnAnterior.interactable = false;
                btnSiguiente.interactable = false;
                return;
            }

            resultados = results;

 
            resultados.Sort((a, b) => b.timestamp.CompareTo(a.timestamp));

            indiceActual = 0;
            MostrarResultado(indiceActual);

            btnAnterior.interactable = false;
            btnSiguiente.interactable = resultados.Count > 1;
        });
    }

    private void MostrarResultado(int index)
    {
        if (index < 0 || index >= resultados.Count)
            return;

        ShotResult r = resultados[index];
        resultadosText.text = $"<b>Disparo {index + 1} de {resultados.Count}</b>\n" +
                             $"- Ángulo: {r.angle}°\n" +
                             $"- Fuerza: {r.force} N\n" +
                             $"- Masa: {r.mass} kg\n" +
                             $"- Impacto: {(r.hit ? "Sí" : "No")}\n" +
                             $"- Distancia: {r.distance:F1} m\n" +
                             $"- Objetos collisionados: {r.objectsAffected}\n";
    }

    private void MostrarAnterior()
    {
        if (indiceActual <= 0) return;
        indiceActual--;
        MostrarResultado(indiceActual);
        ActualizarBotones();
    }

    private void MostrarSiguiente()
    {
        if (indiceActual >= resultados.Count - 1) return;
        indiceActual++;
        MostrarResultado(indiceActual);
        ActualizarBotones();
    }

    private void ActualizarBotones()
    {
        btnAnterior.interactable = indiceActual > 0;
        btnSiguiente.interactable = indiceActual < resultados.Count - 1;
    }
}