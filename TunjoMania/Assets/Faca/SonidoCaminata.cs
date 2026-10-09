using UnityEngine;
using System.Collections;

public class SonidoCaminata : MonoBehaviour
{
    [Header("Character Controller")]
    public CharacterController characterController;

[Header("Sonidos de pasos")]
    public AudioSource pasosAgua;
    public AudioSource pasosCemento;
    public AudioSource pasosPasto;

    [Header("Configuración")]
    public float intervaloPasos = 0.4f;

    private string superficie = "Cemento";
    private Coroutine corrutinaPasos;

    private void Update()
    {
        if (characterController.caminar)
        {
            if (corrutinaPasos == null)
                corrutinaPasos = StartCoroutine(ReproducirPasos());
        }
        else
        {
            if (corrutinaPasos != null)
            {
                StopCoroutine(corrutinaPasos);
                corrutinaPasos = null;
            }

            pasosAgua.Stop();
            pasosCemento.Stop();
            pasosPasto.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Water"))
            superficie = "Water";
        else if (hit.CompareTag("Cemento"))
            superficie = "Cemento";
        else if (hit.CompareTag("Pasto"))
            superficie = "Pasto";
    }

    private IEnumerator ReproducirPasos()
    {
        while (characterController.caminar)
        {
            AudioSource sonido = pasosCemento;

            if (superficie == "Water")
                sonido = pasosAgua;
            else if (superficie == "Pasto")
                sonido = pasosPasto;

            sonido.Play();

            yield return new WaitForSeconds(intervaloPasos);
        }

        corrutinaPasos = null;
    }

}
