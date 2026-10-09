using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

[Header("Level Manager")]
    public LevelManager levelManager;

    [Header("Acciones al finalizar")]
    public bool cambioEscena;
    public bool menu;

    private void OnEnable()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached += VideoFinalizado;
    }

    private void OnDisable()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= VideoFinalizado;
    }

    private void VideoFinalizado(VideoPlayer vp)
    {
        if (levelManager == null)
            return;

        if (cambioEscena)
            levelManager.CambioEscena();

        if (menu)
            levelManager.MenuInicial();
    }

}
