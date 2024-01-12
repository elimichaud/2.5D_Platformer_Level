using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool isGamePaused = false;

    public GameObject menuUI;
    public GameObject player;
    public GameObject pauseButtonUI;

    public Sprite pauseSprite;
    public Sprite playSprite;


    //Managers
    [Space(5)]
    [Header("Managers")]
    private SoundManager m_SoundManager;

    private void Start()
    {
        m_SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()) {
            player.GetComponent<PlayerController>().canAttack = false;
        } else {
            player.GetComponent<PlayerController>().canAttack = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isGamePaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    void ResumeGame() {
        isGamePaused = false;
        Time.timeScale = 1f;

        menuUI.SetActive(false);
        pauseButtonUI.GetComponent<Image>().sprite = pauseSprite;
    }
    
    void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0f;
        menuUI.SetActive(true);
        pauseButtonUI.GetComponent<Image>().sprite = playSprite;
        
    }

    public void OnUIButtonClick() {
        m_SoundManager.PlayClickSound();
        if(isGamePaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
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
