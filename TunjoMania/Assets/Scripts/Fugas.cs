using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fugas : MonoBehaviour
{
    public TextMeshProUGUI contadorTexto;
    public BarraTiempo barraTiempo;

    private bool collected = false;

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
