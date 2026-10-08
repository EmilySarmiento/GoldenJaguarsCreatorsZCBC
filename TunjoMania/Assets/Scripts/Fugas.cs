using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fugas : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public BarraTiempo barraTiempo;

    private bool collected = false;
    public AudioSource Dialogo;
    public AudioSource Sonido_Elegido;
    public AudioSource Sonido_Victoria;
    public GameObject Objeto_Gastadir;
    public GameObject Objeto_Ahorrativo;
    public BarraTiempo Barratiempos;

    private bool trigger = true;

    public void Start()
    {
        Objeto_Ahorrativo.SetActive(false);
        Objeto_Gastadir.SetActive(true);
        Barratiempos = FindFirstObjectByType<BarraTiempo>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player"))
        {
            if (barraTiempo.derrota == false)
            {
                collected = true;

                // Aumentar contador
                int contador = int.Parse(contadorTexto.text);
                contador++;
                contadorTexto.text = contador.ToString();
                //Agrega tiempo a barra de porcntaje de agua
                barraTiempo.AgregarTiempo();
                Sonido_Elegido.Play();
                Dialogo.Play();
                Objeto_Ahorrativo.SetActive(true);
                Objeto_Gastadir.SetActive(false);
                // Desaparecer objeto
                Destroy(gameObject);

                if (contador == 8)
                {
                    if (trigger == true)
                    {
                        trigger = false;

                        Debug.Log("GANASTE");
                        Sonido_Victoria.Play();
                    }
                }
            }
        }
    }
}
