
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonNivel : MonoBehaviour
{
    public void CargarNivel()
    {
        SceneManager.LoadScene(1);/*
        if (LevelManager.Instance != null)
            LevelManager.Instance.CargarNivel();
        else
            Debug.LogError("No existe un LevelManager activo.");*/
    }

    public void CambioEscena()
    {
        SceneManager.LoadScene(2);
        /*
        if (LevelManager.Instance != null)
            LevelManager.Instance.CambioEscena();
        else
            Debug.LogError("No existe un LevelManager activo.");*/
    }

    public void MenuInicial()
    {
        SceneManager.LoadScene(0);/*
        if (LevelManager.Instance != null)
            LevelManager.Instance.MenuInicial();
        else
            Debug.LogError("No existe un LevelManager activo.");*/
    }

    public void ReiniciarJuego()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.ReiniciarJuego();
        else
            Debug.LogError("No existe un LevelManager activo.");
    }

    public void CargarCreditos()
    {
        SceneManager.LoadScene(3);/*
        if (LevelManager.Instance != null)
            LevelManager.Instance.CargarCreditos();
        else
            Debug.LogError("No existe un LevelManager activo.");*/
    }

    public void SalirJuego()
    {
        Application.Quit();/*
        if (LevelManager.Instance != null)
            LevelManager.Instance.SalirJuego();
        else
            Debug.LogError("No existe un LevelManager activo.");*/
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
