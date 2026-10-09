
using UnityEngine;

public class EstadosLuzCarro : MonoBehaviour
{
    [Header("Referencias")]
    public Estados estados;
    public Car car;

    [Header("Lista de luces")]
    public GameObject[] luces;

    [Header("Objeto para el estado 3")]
    public GameObject objetoEstado3;
    public GameObject descativar_objeto;

    private int estadoAnterior = -1;

    private void Start()
    {
        if (estados == null)
            estados = GetComponent<Estados>();

        if (car == null)
            car = GetComponent<Car>();

        ActualizarEstado();
    }

    private void Update()
    {
        if (estados == null)
            return;

        if (estados.estadoActual != estadoAnterior)
            ActualizarEstado();
    }

    private void ActualizarEstado()
    {
        if (estados == null)
            return;

        int estado = estados.estadoActual;

        // Desactivar todas las luces.
        foreach (GameObject luz in luces)
        {
            if (luz != null)
                luz.SetActive(false);
        }

        // Desactivar el objeto exclusivo del estado 3.
        if (objetoEstado3 != null)
            objetoEstado3.SetActive(false);

        switch (estado)
        {
            case 1:
                // Todas las luces permanecen apagadas.
                break;

            case 2:
                // Encender todas las luces de la lista.
                foreach (GameObject luz in luces)
                {
                    if (luz != null)
                        luz.SetActive(true);
                }
                break;

            case 3:
                // Las luces permanecen apagadas y se activa otro objeto.
                if (objetoEstado3 != null)
                    objetoEstado3.SetActive(true);

                // Detener el movimiento del carro.
                if (car != null)
                    car.enabled = false;
                descativar_objeto.SetActive(false);
                break;

            default:
                Debug.LogWarning(
                    "Estado no válido: " + estado,
                    gameObject
                );
                break;
        }

        estadoAnterior = estado;
    }
}

