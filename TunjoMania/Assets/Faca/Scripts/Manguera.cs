using UnityEngine;

public class Manguera : MonoBehaviour
{
    [Header("Crecimiento")]
    [SerializeField] private Vector3 escalaMaxima = new Vector3(5f, 5f, 5f);

    [SerializeField] private float velocidadCrecimiento = 1f;
    [SerializeField] private bool cerrar = false;
    [SerializeField] private GameObject manguera;

    private void Start()
    {
        // Comienza completamente pequeño
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (manguera.gameObject.activeSelf) { }

        else { 
            cerrar = true; }

        if (cerrar == true)
            return;
        // Si ya llegó al tamaño máximo, no hacemos nada
        if (transform.localScale == escalaMaxima)
            return;

        // Crecimiento basado en tiempo
        transform.localScale += Vector3.one * velocidadCrecimiento * Time.deltaTime;

        // Evita que se pase de la escala máxima
        transform.localScale = Vector3.Min(
            transform.localScale,
            escalaMaxima
        );
    }
    public void cambiarestado()
    {
        if (cerrar == true)
        {
            cerrar = false;
        }
        else
        {
            cerrar= true;   
        }
    }
}