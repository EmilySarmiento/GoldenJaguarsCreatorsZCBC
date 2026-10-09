
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    [Header("Interfaz")]
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TMP_Text textoDialogo;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private bool dialogoActivo = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        panelDialogo.SetActive(false);
    }

    public void MostrarDialogo(string mensaje, AudioClip audio)
    {
        if (dialogoActivo)
            return;

        StartCoroutine(ReproducirDialogo(mensaje, audio));
    }

    private IEnumerator ReproducirDialogo(
        string mensaje, AudioClip audio)
    {
        dialogoActivo = true;

        // Detener el juego
        Time.timeScale = 0f;

        // Mostrar interfaz y texto
        panelDialogo.SetActive(true);
        textoDialogo.text = mensaje;

        // Reproducir audio
        audioSource.Stop();
        audioSource.clip = audio;

        if (audio != null)
        {
            audioSource.Play();

            // Esperar hasta que termine el audio
            while (audioSource.isPlaying)
                yield return null;
        }

        // Ocultar interfaz
        panelDialogo.SetActive(false);

        // Reanudar el juego
        Time.timeScale = 1f;

        dialogoActivo = false;
    }

}
