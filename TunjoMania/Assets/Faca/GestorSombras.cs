using UnityEngine;

public class GestorSombras : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] private string tagJugador = "Player";

    [Header("Sombras")]
    [SerializeField] private string tagSombras = "Shadows";

    [Header("Rango")]
    [Tooltip("Distancia máxima para mantener activa una sombra.")]
    [SerializeField] private float rangoDeActivacion = 20f;

    [Header("Comprobación")]
    [Tooltip("Cada cuánto se comprueba la distancia de las sombras.")]
    [SerializeField] private float tiempoEntreComprobaciones = 0.2f;

    private Transform jugador;
    private Luces luces;

    private GameObject[] objetosSombras;
    private Sombras[] sombras;

    private float tiempoComprobacion;

    private float rangoCuadrado;

    private void Start()
    {
        // ==========================================
        // BUSCAR JUGADOR
        // ==========================================

        GameObject objetoJugador =
            GameObject.FindGameObjectWithTag(tagJugador);

        if (objetoJugador == null)
        {
            Debug.LogWarning(
                "GestorSombras: No se encontró ningún objeto con el Tag '" +
                tagJugador + "'."
            );

            return;
        }

        jugador = objetoJugador.transform;

        // ==========================================
        // BUSCAR LUCES
        // ==========================================

        luces = FindObjectOfType<Luces>();

        if (luces == null)
        {
            Debug.LogWarning(
                "GestorSombras: No se encontró ningún script Luces."
            );

            return;
        }

        // ==========================================
        // BUSCAR TODAS LAS SOMBRAS
        // ==========================================

        objetosSombras =
            GameObject.FindGameObjectsWithTag(tagSombras);

        sombras = new Sombras[objetosSombras.Length];

        for (int i = 0; i < objetosSombras.Length; i++)
        {
            sombras[i] =
                objetosSombras[i].GetComponent<Sombras>();
        }

        // ==========================================
        // RANGO AL CUADRADO
        // ==========================================

        rangoCuadrado =
            rangoDeActivacion * rangoDeActivacion;

        // ==========================================
        // INICIAR SOMBRAS
        // ==========================================

        ActualizarSombras();
    }

    private void Update()
    {
        if (jugador == null || luces == null)
            return;

        tiempoComprobacion += Time.deltaTime;

        if (tiempoComprobacion >= tiempoEntreComprobaciones)
        {
            tiempoComprobacion = 0f;

            ActualizarSombras();
        }
    }

    private void ActualizarSombras()
    {
        float tiempoGlobal =
            luces.ObtenerTiempoGlobal();

        float tiempoDeDia =
            luces.ObtenerTiempoDeDia();

        Vector3 posicionJugador =
            jugador.position;

        for (int i = 0; i < objetosSombras.Length; i++)
        {
            GameObject objetoSombra =
                objetosSombras[i];

            if (objetoSombra == null)
                continue;

            Vector3 diferencia =
                objetoSombra.transform.position -
                posicionJugador;

            float distanciaCuadrada =
                diferencia.sqrMagnitude;

            bool estaDentro =
                distanciaCuadrada <= rangoCuadrado;

            // ==========================================
            // SOMBRA DENTRO DEL RANGO
            // ==========================================

            if (estaDentro)
            {
                // Si estaba desactivada, activarla
                if (!objetoSombra.activeSelf)
                {
                    objetoSombra.SetActive(true);
                }

                // Obtener componente
                Sombras sombra = sombras[i];

                if (sombra != null)
                {
                    // Colocar inmediatamente la sombra
                    // en el punto correcto del ciclo
                    sombra.ActualizarSombra(
                        tiempoGlobal,
                        tiempoDeDia
                    );
                }
            }

            // ==========================================
            // SOMBRA FUERA DEL RANGO
            // ==========================================

            else
            {
                if (objetoSombra.activeSelf)
                {
                    objetoSombra.SetActive(false);
                }
            }
        }
    }
}