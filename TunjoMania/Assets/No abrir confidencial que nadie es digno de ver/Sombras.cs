using UnityEngine;

public class Sombras : MonoBehaviour
{
    [Header("Rotación Z")]
    [SerializeField] private float rotacionInicial = 0f;
    [SerializeField] private float gradosPorTransicion = 180f;

    [Header("Escala inicial")]
    [SerializeField] private Vector3 escalaInicial = Vector3.one;

    [Header("Escala máxima")]
    [SerializeField] private Vector3 escalaMaxima = new Vector3(1.1f, 1.1f, 1.1f);

    [Header("Animación")]
    [SerializeField]
    private AnimationCurve curva =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float rotacionZActual;
    private Vector3 escalaActual;

    private void Start()
    {
        rotacionZActual = rotacionInicial;
        escalaActual = escalaInicial;

        AplicarTransformacion();
    }

    public void ActualizarSombra(float tiempoGlobal, float tiempoDeDia)
    {
        if (tiempoDeDia <= 0f)
            return;

        // ==========================================
        // TIEMPO DEL CICLO
        // ==========================================

        float tiempoDentroDelCiclo =
            tiempoGlobal % (tiempoDeDia * 2f);

        // ==========================================
        // PROGRESO DE LA TRANSICIÓN ACTUAL
        // ==========================================

        float progreso;

        if (tiempoDentroDelCiclo < tiempoDeDia)
        {
            // Día ? Noche
            progreso = tiempoDentroDelCiclo / tiempoDeDia;
        }
        else
        {
            // Noche ? Día
            progreso =
                (tiempoDentroDelCiclo - tiempoDeDia)
                / tiempoDeDia;
        }

        progreso = Mathf.Clamp01(progreso);

        // Aplicar aceleración/desaceleración
        float progresoSuave = curva.Evaluate(progreso);

        // ==========================================
        // ROTACIÓN Z
        // ==========================================

        float rotacionCiclo;

        if (tiempoDentroDelCiclo < tiempoDeDia)
        {
            // 0° ? 180°
            rotacionCiclo =
                gradosPorTransicion * progresoSuave;
        }
        else
        {
            // 180° ? 360°
            rotacionCiclo =
                gradosPorTransicion +
                (gradosPorTransicion * progresoSuave);
        }

        rotacionZActual =
            rotacionInicial + rotacionCiclo;

        // ==========================================
        // ESCALA X, Y, Z
        // ==========================================

        float escalaCurva;

        if (progreso <= 0.5f)
        {
            // 1 ? máxima
            escalaCurva = progreso * 2f;
        }
        else
        {
            // máxima ? 1
            escalaCurva = (1f - progreso) * 2f;
        }

        escalaCurva = Mathf.Clamp01(escalaCurva);

        float escalaSuave =
            curva.Evaluate(escalaCurva);

        escalaActual = Vector3.Lerp(
            escalaInicial,
            escalaMaxima,
            escalaSuave
        );

        AplicarTransformacion();
    }

    private void AplicarTransformacion()
    {
        // Solo modificamos Z de la rotación
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            rotacionZActual
        );

        // Modificamos X, Y y Z de la escala
        transform.localScale = escalaActual;
    }
}