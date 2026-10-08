using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Luces : MonoBehaviour
{
    [Header("Luz")]
    [SerializeField] private Light2D luz;

    [Header("Día")]
    [SerializeField] private Color colorDia = Color.white;
    [SerializeField] private float intensidadDia = 1f;

    [Header("Noche")]
    [SerializeField] private Color colorNoche = new Color(0.3f, 0.4f, 0.8f);
    [SerializeField] private float intensidadNoche = 0.7f;

    [Header("Apocalipsis")]
    [SerializeField] private Color colorApocalipsis = Color.red;
    [SerializeField] private float intensidadApocalipsis = 0.5f;

    [Header("Tiempo")]
    [Tooltip("Tiempo en segundos que tarda la transición entre día y noche.")]
    [SerializeField] private float tiempoDeDia = 60f;

    [Header("Curva de transición")]
    [SerializeField]
    private AnimationCurve aceleracionDesaceleracion =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Estado Admin")]
    [SerializeField] private EstadoAdmin estadoAdmin;

    [Header("Apocalipsis")]
    [SerializeField] private bool apocalipsis = false;

    private bool haciaNoche = true;
    private float tiempoTranscurrido = 0f;
    private float tiempoGlobal = 0f;

    private Color colorInicial;
    private Color colorObjetivo;

    private float intensidadInicial;
    private float intensidadObjetivo;

    private void Start()
    {
        if (luz == null)
            luz = GetComponent<Light2D>();

        if (estadoAdmin == null)
            estadoAdmin = FindObjectOfType<EstadoAdmin>();

        // Comenzamos en el día
        luz.color = colorDia;
        luz.intensity = intensidadDia;

        colorInicial = colorDia;
        colorObjetivo = colorNoche;

        intensidadInicial = intensidadDia;
        intensidadObjetivo = intensidadNoche;

        if (estadoAdmin != null)
            estadoAdmin.estadoActual = 1;
    }

    private void Update()
    {
        // Reloj global del mundo
        tiempoGlobal += Time.deltaTime;

        // Si entramos en apocalipsis, dejamos de hacer el ciclo normal
        if (apocalipsis)
        {
            ActualizarApocalipsis();
            return;
        }

        ActualizarTransicion();
    }

    private void ActualizarTransicion()
    {
        tiempoTranscurrido += Time.deltaTime;

        float progreso = tiempoTranscurrido / tiempoDeDia;

        progreso = Mathf.Clamp01(progreso);

        // Aplicamos la curva de aceleración/desaceleración
        float progresoSuave =
            aceleracionDesaceleracion.Evaluate(progreso);

        // Cambiar color
        luz.color = Color.Lerp(
            colorInicial,
            colorObjetivo,
            progresoSuave
        );

        // Cambiar intensidad
        luz.intensity = Mathf.Lerp(
            intensidadInicial,
            intensidadObjetivo,
            progresoSuave
        );

        // Terminó la transición
        if (progreso >= 1f)
        {
            TerminarTransicion();
        }
    }

    private void TerminarTransicion()
    {
        tiempoTranscurrido = 0f;

        if (haciaNoche)
        {
            // Llegamos a la noche
            luz.color = colorNoche;
            luz.intensity = intensidadNoche;

            if (estadoAdmin != null)
                estadoAdmin.estadoActual = 2;

            // Ahora vamos hacia el día
            colorInicial = colorNoche;
            colorObjetivo = colorDia;

            intensidadInicial = intensidadNoche;
            intensidadObjetivo = intensidadDia;

            haciaNoche = false;
        }
        else
        {
            // Llegamos al día
            luz.color = colorDia;
            luz.intensity = intensidadDia;

            if (estadoAdmin != null)
                estadoAdmin.estadoActual = 1;

            // Ahora vamos hacia la noche
            colorInicial = colorDia;
            colorObjetivo = colorNoche;

            intensidadInicial = intensidadDia;
            intensidadObjetivo = intensidadNoche;

            haciaNoche = true;
        }
    }

    private void ActualizarApocalipsis()
    {
        colorInicial = luz.color;
        intensidadInicial = luz.intensity;

        luz.color = Color.Lerp(
            colorInicial,
            colorApocalipsis,
            Time.deltaTime * 2f
        );

        luz.intensity = Mathf.Lerp(
            luz.intensity,
            intensidadApocalipsis,
            Time.deltaTime * 2f
        );

        if (estadoAdmin != null)
            estadoAdmin.estadoActual = 3;
    }

    public void ActivarApocalipsis()
    {
        apocalipsis = true;
    }

    public void DesactivarApocalipsis()
    {
        apocalipsis = false;

        tiempoTranscurrido = 0f;

        colorInicial = luz.color;
        intensidadInicial = luz.intensity;

        colorObjetivo = colorDia;
        intensidadObjetivo = intensidadDia;

        haciaNoche = true;
    }

    public float ObtenerTiempoDeDia()
    {
        return tiempoDeDia;
    }
    public float ObtenerTiempoGlobal()
    {
        return tiempoGlobal;
    }

}