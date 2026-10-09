using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fugas : MonoBehaviour
{
    [Header("Contador y tiempo")]
    public TextMeshProUGUI contadorTexto;
    public BarraTiempo barraTiempo;

    [Header("Interfaz del dialogo")]
    public GameObject PanelDialogo;
    public TextMeshProUGUI TextoDialogo;

    [TextArea(2, 5)]
    public string Mensaje;

    [Header("Interfaz de victoria")]
    public GameObject PanelVictoria;

    [Header("Escena de creditos")]
    public string NombreEscenaCreditos = "Creditos";

    [Header("Audios")]
    public AudioSource Dialogo;
    public AudioSource Sonido_Elegido;
    public AudioSource Sonido_Victoria;

    [Header("Objetos visuales")]
    public GameObject Objeto_Gastadir;
    public GameObject Objeto_Ahorrativo;

    private bool collected = false;
    private static bool dialogoActivo = false;

    private void Start()
    {
        if (Objeto_Ahorrativo != null)
            Objeto_Ahorrativo.SetActive(false);

        if (Objeto_Gastadir != null)
            Objeto_Gastadir.SetActive(true);

        if (barraTiempo == null)
            barraTiempo = FindFirstObjectByType<BarraTiempo>();

        if (PanelDialogo != null)
            PanelDialogo.SetActive(false);

        if (PanelVictoria != null)
            PanelVictoria.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || dialogoActivo)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (barraTiempo == null || barraTiempo.derrota)
            return;

        collected = true;
        StartCoroutine(ProcesoRecoleccion());
    }

    private IEnumerator ProcesoRecoleccion()
    {
        dialogoActivo = true;

        int contador = int.Parse(contadorTexto.text);
        contador++;
        contadorTexto.text = contador.ToString();

        barraTiempo.AgregarTiempo();

        if (Objeto_Ahorrativo != null)
            Objeto_Ahorrativo.SetActive(true);

        if (Objeto_Gastadir != null)
            Objeto_Gastadir.SetActive(false);

        // Pausar el juego mientras se reproduce el dialogo
        Time.timeScale = 0f;

        if (PanelDialogo != null)
            PanelDialogo.SetActive(true);

        if (TextoDialogo != null)
            TextoDialogo.text = Mensaje;

        if (Sonido_Elegido != null)
            Sonido_Elegido.Play();

        if (Dialogo != null && Dialogo.clip != null)
        {
            Dialogo.Play();

            while (Dialogo.isPlaying)
                yield return null;
        }

        if (PanelDialogo != null)
            PanelDialogo.SetActive(false);

        if (contador >= 8)
        {
            Debug.Log("¡GANASTE! Entrando al bloque de victoria.");

            // Mostrar la interfaz de victoria
            if (PanelVictoria != null)
            {
                PanelVictoria.SetActive(true);
                Debug.Log("PanelVictoria activado: " + PanelVictoria.activeSelf);
            }
            else
            {
                Debug.LogError("PanelVictoria NO está asignado.");
            }

            // Reproducir el audio de victoria
            if (Sonido_Victoria != null)
            {
                Debug.Log("Audio asignado: " + Sonido_Victoria.name);
                Debug.Log("Clip asignado: " +
                    (Sonido_Victoria.clip != null));

                if (Sonido_Victoria.clip != null)
                {
                    Sonido_Victoria.Play();
                    Debug.Log("Audio reproduciéndose: " +
                        Sonido_Victoria.isPlaying);

                    while (Sonido_Victoria.isPlaying)
                        yield return null;
                }
            }
            else
            {
                Debug.LogError("Sonido_Victoria NO está asignado.");
            }

            Debug.Log("¡GANASTE! Finalizó el proceso de victoria.");

            Destroy(gameObject);
            Time.timeScale = 1f;
            SceneManager.LoadScene(NombreEscenaCreditos);

            yield break;
        }

        // Si aun no ha ganado, destruir el objeto recogido
        Destroy(gameObject);

        Time.timeScale = 1f;
        dialogoActivo = false;
    }
}
