using UnityEngine;

public class EstadoAdmin : MonoBehaviour
{
    [Header("Estado global")]
    [Range(1, 3)]
    public int estadoActual = 1;

    private int estadoAnterior;

    private Estados[] estadosDelMapa;

    private void Start()
    {
        estadoAnterior = estadoActual;

        BuscarEstados();
        ActualizarTodosLosEstados();
    }

    private void Update()
    {
        if (estadoActual != estadoAnterior)
        {
            ActualizarTodosLosEstados();
            estadoAnterior = estadoActual;
        }
    }

    private void BuscarEstados()
    {
        estadosDelMapa = FindObjectsOfType<Estados>();
    }

    private void ActualizarTodosLosEstados()
    {
        foreach (Estados estado in estadosDelMapa)
        {
            if (estado != null)
            {
                estado.estadoActual = estadoActual;
            }
        }
    }
}