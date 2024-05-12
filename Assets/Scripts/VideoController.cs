using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public TMP_Text gameOverText;
    public string nextSceneName;

    void Start()
    {
        // Asegúrate de que el VideoPlayer tenga asignado el vídeo que quieres reproducir
        videoPlayer.loopPointReached += OnVideoFinished;
        
        // Desactiva el texto al inicio
        gameOverText.gameObject.SetActive(false);
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        // Cuando el vídeo termine de reproducirse, activa el texto
        gameOverText.gameObject.SetActive(true);
        // Inicia la espera para la entrada del usuario
        StartCoroutine(WaitForInput());
    }

    IEnumerator WaitForInput()
    {
        // Esperar hasta que se presione cualquier tecla
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // Ir a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
