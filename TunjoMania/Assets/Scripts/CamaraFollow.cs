using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float velocidad = 5f;

    void LateUpdate()
    {
        Vector3 posicion = player.position;

        posicion.z = transform.position.z;

        transform.position = Vector3.Lerp(
            transform.position,
            posicion,
            velocidad * Time.deltaTime
        );
    }
}
