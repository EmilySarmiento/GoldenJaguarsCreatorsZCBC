using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Estado direccional")]
    [Range(1, 4)]
    public int estado = 1;

    [Header("GameObjects por dirección")]
    public GameObject derecha;
    public GameObject izquierda;
    public GameObject arriba;
    public GameObject abajo;

    [Header("Movimiento")]
    public float velocidad = 2f;
    public Vector3 posicionObjetivo;
    public bool tieneObjetivo = false;

    private void Update()
    {
        ActualizarEstado();
        Mover();
    }

    private void ActualizarEstado()
    {
        if (derecha != null)
            derecha.SetActive(estado == 1);

        if (izquierda != null)
            izquierda.SetActive(estado == 2);

        if (arriba != null)
            arriba.SetActive(estado == 3);

        if (abajo != null)
            abajo.SetActive(estado == 4);
    }

    private void Mover()
    {
        if (!tieneObjetivo)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            posicionObjetivo,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, posicionObjetivo) < 0.01f)
        {
            transform.position = posicionObjetivo;
            tieneObjetivo = false;
        }
    }
}

