
using UnityEngine;
using System.Collections;

public class Paloma : MonoBehaviour
{
    [Header("Estado")]
    public bool esDeDia = true;

    [Header("Tiempo de espera aleatorio")]
    public float minimoRandom = 2f;
    public float maximoRandom = 5f;

    [Header("Movimiento")]
    public GameObject objeto;
    public Vector3 rango = new Vector3(0.5f, 0.5f, 0.5f);
    public float velocidad = 1f;

    [Header("Animación")]
    public Animator animator;

    [Header("Mirror")]
    public bool usarMirror = true;

    private float tiempoRandom;
    private Vector3 posicionFinal;
    private Coroutine rutinaDia;
    private Vector3 escalaOriginal;

    private void Awake()
    {
        escalaOriginal = transform.localScale;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ActualizarEstado();
    }

    private void Update()
    {
        ActualizarEstado();
    }

    private void ActualizarEstado()
    {
        if (esDeDia && rutinaDia == null)
        {
            rutinaDia = StartCoroutine(TiempoRandom());
        }
        else if (!esDeDia && rutinaDia != null)
        {
            StopCoroutine(rutinaDia);
            rutinaDia = null;

            ActualizarCaminar(false);
        }
    }

    private IEnumerator TiempoRandom()
    {
        while (esDeDia)
        {
            ElegirNumeroRandom();

            yield return new WaitForSeconds(tiempoRandom);

            if (!esDeDia)
                break;

            Mover();

            if (objeto == null)
                break;

            while (esDeDia &&
                   Vector3.Distance(transform.position, posicionFinal) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    posicionFinal,
                    velocidad * Time.deltaTime
                );

                yield return null;
            }

            if (esDeDia)
            {
                transform.position = posicionFinal;
                ActualizarCaminar(false);
            }
        }

        ActualizarCaminar(false);
        rutinaDia = null;
    }

    private void Mover()
    {
        if (objeto == null)
            return;

        Vector3 posicionActual = transform.position;
        Vector3 posicionReferencia = objeto.transform.position;

        posicionFinal = new Vector3(
            posicionReferencia.x + Random.Range(-rango.x, rango.x),
            posicionReferencia.y + Random.Range(-rango.y, rango.y),
            posicionReferencia.z + Random.Range(-rango.z, rango.z)
        );

        // Mirror según la dirección horizontal.
        if (usarMirror && posicionFinal.x != posicionActual.x)
        {
            Vector3 escala = escalaOriginal;

            if (posicionFinal.x < posicionActual.x)
                escala.x = -Mathf.Abs(escalaOriginal.x);
            else
                escala.x = Mathf.Abs(escalaOriginal.x);

            transform.localScale = escala;
        }

        // Activar caminar solamente si debe desplazarse.
        bool debeCaminar =
            Vector3.Distance(posicionActual, posicionFinal) > 0.01f;

        ActualizarCaminar(debeCaminar);
    }

    private void ActualizarCaminar(bool estado)
    {
        if (animator != null)
        {
            animator.SetBool("Caminar", estado);
        }
    }

    private void ElegirNumeroRandom()
    {
        tiempoRandom = Random.Range(minimoRandom, maximoRandom);
    }
}

