using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    private SoundManager m_SoundManager;

    private void Start()
    {
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void RestartGame() {
        m_SoundManager.PlayClickSound();
        SceneManager.LoadScene(sceneToLoad);
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        m_SoundManager.PlayClickSound();
        if(Application.isEditor) {
            //UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}
