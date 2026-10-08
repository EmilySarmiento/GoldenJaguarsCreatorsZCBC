using UnityEngine;

public class BarraTiempo : MonoBehaviour
{
    public float tiempoMaximo = 30f;
    public float tiempoActual;

    public float tiempoPorObjeto = 5f;

    private Vector3 escalaInicial;

    void Start()
    {
        tiempoActual = tiempoMaximo;

        escalaInicial = transform.localScale;
    }

    void Update()
    {
        // Disminuir el tiempo
        tiempoActual -= Time.deltaTime;

        // Evitar que sea menor que 0
        tiempoActual = Mathf.Clamp(tiempoActual, 0f, tiempoMaximo);

        // Calcular cuánto debe medir la barra
        float porcentaje = tiempoActual / tiempoMaximo;

        // Reducir solamente el ancho
        transform.localScale = new Vector3(
            escalaInicial.x ,
            escalaInicial.y * porcentaje,
            escalaInicial.z
        );

        // Cuando se acaba el tiempo
        if (tiempoActual <= 0)
        {
            Debug.Log("PERDISTE");
        }
    }

    public void AgregarTiempo()
    {
        tiempoActual += tiempoPorObjeto;

        // No superar el máximo
        tiempoActual = Mathf.Clamp(tiempoActual, 0f, tiempoMaximo);
    }
}