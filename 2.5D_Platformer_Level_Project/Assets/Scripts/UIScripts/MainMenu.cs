using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    public void StartGame() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame() {
        if(Application.isEditor) {
            //UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}
