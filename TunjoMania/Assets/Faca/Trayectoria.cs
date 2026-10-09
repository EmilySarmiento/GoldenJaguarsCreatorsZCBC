
using UnityEngine;
using System.Collections.Generic;

public class Trayectoria : MonoBehaviour
{
    [Header("Direcciones disponibles")]
    public bool derecha;
    public bool izquierda;
    public bool arriba;
    public bool abajo;

    [Header("Puntos de destino")]
    public GameObject squareDerecha;
    public GameObject squareIzquierda;
    public GameObject squareArriba;
    public GameObject squareAbajo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colision");
        Car car = other.GetComponent<Car>();

        if (car == null)
            return;

        List<int> opciones = new List<int>();

        if (derecha && squareDerecha != null)
            opciones.Add(1);

        if (izquierda && squareIzquierda != null)
            opciones.Add(2);

        if (arriba && squareArriba != null)
            opciones.Add(3);

        if (abajo && squareAbajo != null)
            opciones.Add(4);

        if (opciones.Count == 0)
            return;

        int seleccion = opciones[Random.Range(0, opciones.Count)];

        switch (seleccion)
        {
            case 1:
                car.estado = 1;
                car.posicionObjetivo = squareDerecha.transform.position;
                break;

            case 2:
                car.estado = 2;
                car.posicionObjetivo = squareIzquierda.transform.position;
                break;

            case 3:
                car.estado = 3;
                car.posicionObjetivo = squareArriba.transform.position;
                break;

            case 4:
                car.estado = 4;
                car.posicionObjetivo = squareAbajo.transform.position;
                break;
        }

        car.tieneObjetivo = true;
    }
}

