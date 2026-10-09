using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarNivel()
    {
        Debug.Log("CargandoNivel");
        SceneManager.LoadScene(1);
    }

    public void CambioEscena()
    {
        Debug.Log("CambiandoEscena");
        SceneManager.LoadScene(2);
    }

    public void MenuInicial()
    {
        Debug.Log("CambiandoEscena");
        SceneManager.LoadScene(0);
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CargarCreditos()
    {
        Debug.Log("CargandoCreditos");
        SceneManager.LoadScene(3);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}