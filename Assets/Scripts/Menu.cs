using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private string returnSceneName;

    private float volume;
    public Slider VolumeSlider;

    public AudioSource musicPlayer;

    private void Awake()
    {
        returnSceneName = "MainMenu";
        //VolumeSlider = null;
    }

    public void SetReturnSceneName(string sceneName)
    {
        returnSceneName = sceneName;
    }

    public void OnMouseClickNewGame()
    {
        Debug.Log("OnMouseClickNewGame");
        SceneManager.LoadScene("Forest", LoadSceneMode.Single);
    }

    public void OnMouseClickLoadGame()
    {
        Debug.Log("OnMouseClickLoadGame");
        SceneManager.LoadScene("LoadMenu", LoadSceneMode.Single);
    }

    public void OnMouseClickSettings()
    {
        Debug.Log("OnMouseClickSettings");
        SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Single);
    }

    public void OnMouseClickExit()
    {
        Debug.Log("OnMouseClickExit");
        Application.Quit();
    }

    public void OnMouseClickReturn()
    {
        Debug.Log($"OnMouseClicReturn ({returnSceneName})");
        SceneManager.LoadScene(returnSceneName, LoadSceneMode.Single);
    }

    public void OnValueChangedVolume()
    {
        Debug.Log("OnValueChangedVolume");
        if (VolumeSlider != null && musicPlayer != null)
        {
            volume = VolumeSlider.value;
            Debug.Log($"Volume = {volume}");
            musicPlayer.volume = volume;
            musicPlayer.clip = Resources.Load("autumn-rain-melancholy-Before_The_Steps_To_Infinity") as AudioClip;
            musicPlayer.Play();
        }
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        if (musicPlayer != null)
        {
            musicPlayer.Stop();
        }
    }
}
