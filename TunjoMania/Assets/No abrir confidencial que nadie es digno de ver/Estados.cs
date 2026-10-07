using UnityEngine;

public class Estados : MonoBehaviour
{
    [Header("Estado actual")]
    [Min(1)]
    public int estadoActual = 1;

    [Header("Objetos de cada estado")]
    public GameObject[] estados;

    private int estadoAnterior;

    private void Start()
    {
        estadoAnterior = estadoActual;
        ActualizarEstado();
    }

    private void Update()
    {
        if (estadoActual != estadoAnterior)
        {
            ActualizarEstado();
            estadoAnterior = estadoActual;
        }
    }

    private void ActualizarEstado()
    {
        // Apagar todos los estados
        for (int i = 0; i < estados.Length; i++)
        {
            if (estados[i] != null)
                estados[i].SetActive(false);
        }

        // Activar solamente el estado seleccionado
        int indice = estadoActual - 1;

        if (indice >= 0 && indice < estados.Length)
        {
            if (estados[indice] != null)
                estados[indice].SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "El estado seleccionado no existe en el objeto " + gameObject.name
            );
        }
    }
}