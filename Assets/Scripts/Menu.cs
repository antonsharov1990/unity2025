using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;


public class Menu : MonoBehaviour
{
    private string returnSceneName;

    private float volume;
    public UnityEngine.UI.Slider VolumeSlider;

    public AudioSource musicPlayer;

    private string _saveFileName;

    public Transform SavedFilesScrollViewContent;

    public GameObject buttonPrefub;

    private void Awake()
    {
        returnSceneName = "MainMenu";
        _saveFileName = "";
        //VolumeSlider = null;

        string saveFileName = "test1";
        saveFileName += ".save";
        string filename = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
        System.IO.File.Create(filename);

        saveFileName = "test2";
        saveFileName += ".save";
        filename = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
        System.IO.File.Create(filename);

        string path = Application.persistentDataPath;
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
        System.IO.FileInfo[] info = dir.GetFiles("*.save");
        
        foreach(System.IO.FileInfo f in info)
        {
            //GameObject buttonGO = new GameObject();
            GameObject buttonGO = Instantiate(buttonPrefub, transform.position, Quaternion.identity);
            buttonGO.name = "Button";
            buttonGO.transform.parent = SavedFilesScrollViewContent;
            // buttonGO.layer = 5;
            // buttonGO.AddComponent<Button>();
            // buttonGO.AddComponent<RectTransform>();
            // buttonGO.AddComponent<CanvasRenderer>();
            // buttonGO.AddComponent<Image>();

            RectTransform buttonRT = buttonGO.GetComponent<RectTransform>();
            buttonRT.anchoredPosition = new Vector2(0.5f, 0.5f);
            buttonRT.anchorMin = new Vector2(0f, 1f);
            buttonRT.anchorMax = new Vector2(1f, 1f);
            buttonRT.position = new Vector3(0f,0f,0f);
            buttonRT.localPosition = new Vector3(0f,-150f,0f);

            //TextMeshPro textMeshPro =  buttonGO.GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshPro>();
            //Debug.Log(textMeshPro.text);
            //textMeshPro.text = "1";//f.Name;
            
            Debug.Log(buttonGO.transform.childCount);
            Transform tr = buttonGO.transform.GetChild(0);
            //TextMeshPro textMeshPro =  tr.GetComponent<TextMeshPro>();
            //textMeshPro.text = "1";//f.Name;
            //Text tx = tr.GetComponent<Text>();
            //tx.text = "1";//f.Name;
            TextMeshProUGUI tx = tr.GetComponent<TextMeshProUGUI>();
            tx.text = "1";//f.Name;
            
            //for Example for move text from unity button to new button - it is work!
            // UnityEngine.Transform button = SavedFilesScrollViewContent.GetChild(0);
            // UnityEngine.Transform textMeshPro =  button.GetChild(0);
            // textMeshPro.transform.parent = buttonGO.transform;
            
            // GameObject textGO = new GameObject();
            // textGO.name = "Text (TMP)";
            // textGO.transform.parent = buttonGO.transform;
            // textGO.layer = 5;
            // textGO.AddComponent<RectTransform>();
            // textGO.AddComponent<CanvasRenderer>();
            // textGO.AddComponent<TextMeshPro>();
            // RectTransform textRT = textGO.GetComponent<RectTransform>();
            // textRT.anchoredPosition = new Vector2(0.5f, 0.5f);
            // textRT.anchorMin = new Vector2(0f, 0f);
            // textRT.anchorMax = new Vector2(1f, 1f);

            // textRT.position = new Vector3(0f,0f,0f);
            // textRT.localPosition = new Vector3(0f,0f,0f);

            // TextMeshPro textT = textGO.GetComponent<TextMeshPro>();
            // textT.text = f.Name;
            // textT.fontSize = 24;
            // textT.color = Color.black;
            // textT.alpha = 1f;
            
        }
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

    

    private void LoadGame(string saveFileName)
    {
        Debug.Log("LoadGame");

        saveFileName += ".save";
        Dictionary<string, object> gamestate = null;
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        string sceneName = "";
        int? x = 0;
        int? y = 0;
        int? z = 0;
        int? pitch = 0;
        int? roll = 0;
        int? yaw = 0;

        try
        {
            if (System.IO.File.Exists(saveFileName))
            {
                System.IO.FileStream stream = System.IO.File.Open(saveFileName, System.IO.FileMode.Open);
                gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
                stream.Close();

                sceneName = gamestate["scene"] as string;
                x = gamestate["x"] as int?;
                y = gamestate["y"] as int?;
                z = gamestate["z"] as int?;
                pitch = gamestate["pitch"] as int?;
                roll = gamestate["roll"] as int?;
                yaw = gamestate["yaw"] as int?;
            }
        }
        catch
        {
            Debug.Log("Loading save is fail!");
        }

        if (string.IsNullOrEmpty(sceneName)
        || x is null
        || y is null
        || z is null
        || pitch is null
        || roll is null
        || yaw is null
        )
        {
            Debug.Log("Loading MainMenu");
            sceneName = "MainMenu";
            x = 0;
            y = 0;
            z = 0;
            pitch = 0;
            roll = 0;
            yaw = 0;
        }

        Current.CharacterModel.SaveCharacterPlace(sceneName, (int)x, (int)y, (int)z, (int)pitch, (int)roll, (int)yaw);
    }

    private void SaveGame(string saveFileName)
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("scene", Current.CharacterModel.GetScene());
        gamestate.Add("x", Current.CharacterModel.GetX());
        gamestate.Add("y", Current.CharacterModel.GetY());
        gamestate.Add("z", Current.CharacterModel.GetZ());
        gamestate.Add("pitch", Current.CharacterModel.GetPitch());
        gamestate.Add("roll", Current.CharacterModel.GetRoll());
        gamestate.Add("yaw", Current.CharacterModel.GetYaw());

        saveFileName += ".save";
        string filename = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream stream = System.IO.File.Create(filename);
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    private void DeleteGame(string saveFileName)
    {
        saveFileName += ".save";
        string filename = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
        System.IO.File.Delete(filename);
    }

    public void OnValueChangedFile()
    {
        Debug.Log("OnValueChangedFile");

        

        if (VolumeSlider != null && musicPlayer != null)
        {
            volume = VolumeSlider.value;
            Debug.Log($"Volume = {volume}");
            musicPlayer.volume = volume;
            musicPlayer.clip = Resources.Load("autumn-rain-melancholy-Before_The_Steps_To_Infinity") as AudioClip;
            musicPlayer.Play();
        }
    }

}
