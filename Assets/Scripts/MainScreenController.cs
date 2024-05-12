using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Update()
    {
        if(Input.anyKeyDown)
        {
            // Ir a la siguiente escena
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
