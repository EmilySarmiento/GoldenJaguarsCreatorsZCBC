using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fugas : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public BarraTiempo barraTiempo;

    private bool collected = false;
    public AudioSource Dialogo;
    public GameObject Objeto_Gastadir;
    public GameObject Objeto_Ahorrativo;

    public void Start()
    {
        Objeto_Ahorrativo.SetActive(false);
        Objeto_Gastadir.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Aumentar contador
            int contador = int.Parse(contadorTexto.text);
            contador++;
            contadorTexto.text = contador.ToString();
            //Agrega tiempo a barra de porcntaje de agua
            barraTiempo.AgregarTiempo();
            Dialogo.Play();
            Objeto_Ahorrativo.SetActive(true);
            Objeto_Gastadir.SetActive(false);
            // Desaparecer objeto
            Destroy(gameObject);

            if ( contador == 8)
            {
                Debug.Log("GANASTE");
                SceneManager.LoadScene(3);
            }
        }
    }
}
