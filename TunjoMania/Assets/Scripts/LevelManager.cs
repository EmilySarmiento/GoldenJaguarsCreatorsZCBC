using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BotonInicio()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonCreditos()
    {
        SceneManager.LoadScene(3);
    }
    public void BotonSalir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

}