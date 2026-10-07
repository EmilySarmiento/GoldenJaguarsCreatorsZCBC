using UnityEngine;
using UnityEngine.UI; // Necesario para controlar componentes de UI

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Componentes de Audio")]
    [SerializeField] private AudioSource musicSource;

    [Header("Componentes de UI (Imágenes normales)")]
    [SerializeField] private RawImage buttonRawImage; // Arrastra aquí el RawImage del botón
    [SerializeField] private Texture textureWhenOn;   // Imagen normal de música encendida
    [SerializeField] private Texture textureWhenOff;  // Imagen normal de música apagada

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateButtonTexture();
    }

    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.Play();
        }

        UpdateButtonTexture();
    }

    private void UpdateButtonTexture()
    {
        if (buttonRawImage != null)
        {
            // Cambia la textura según el estado del audio
            buttonRawImage.texture = musicSource.isPlaying ? textureWhenOn : textureWhenOff;
        }
    }
}

