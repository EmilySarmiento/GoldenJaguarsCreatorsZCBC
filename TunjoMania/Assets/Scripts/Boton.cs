
using UnityEngine;

public class BotonNivel : MonoBehaviour
{
    public void CargarNivel()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.CargarNivel();
        else
            Debug.LogError("No existe un LevelManager activo.");
    }

    public void CambioEscena()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.CambioEscena();
        else
            Debug.LogError("No existe un LevelManager activo.");
    }

    public void MenuInicial()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.MenuInicial();
        else
            Debug.LogError("No existe un LevelManager activo.");
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
        if (LevelManager.Instance != null)
            LevelManager.Instance.CargarCreditos();
        else
            Debug.LogError("No existe un LevelManager activo.");
    }

    public void SalirJuego()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.SalirJuego();
        else
            Debug.LogError("No existe un LevelManager activo.");
    }
}
