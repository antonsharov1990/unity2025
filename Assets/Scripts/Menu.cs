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

    public GameObject SaveNameInputField;

    public GameObject buttonPrefub;

    private void Awake()
    {
        returnSceneName = "MainMenu";
        _saveFileName = "";

        InitializeListOfSaves();
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
        // gamestate.Add("scene", Current.CharacterModel.GetScene());
        // gamestate.Add("x", Current.CharacterModel.GetX());
        // gamestate.Add("y", Current.CharacterModel.GetY());
        // gamestate.Add("z", Current.CharacterModel.GetZ());
        // gamestate.Add("pitch", Current.CharacterModel.GetPitch());
        // gamestate.Add("roll", Current.CharacterModel.GetRoll());
        // gamestate.Add("yaw", Current.CharacterModel.GetYaw());

        string filename = System.IO.Path.Combine(Application.persistentDataPath, saveFileName);
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream stream = System.IO.File.Create(filename);
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    private void DeleteGame(string saveFileName)
    {
        Debug.Log($"Delete {saveFileName}");
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

    private void ClearChildren(Transform savedFilesScrollViewContent)
    {
        //savedFilesScrollViewContent.DetachChildren();

        Debug.Log($"savedFilesScrollViewContent.childCount = {savedFilesScrollViewContent.childCount}");
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[savedFilesScrollViewContent.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in savedFilesScrollViewContent)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            Debug.Log($"InitializeListOfSaves Destroy");
            DestroyImmediate(child.gameObject);
        }

        Debug.Log($"childCount = {savedFilesScrollViewContent.childCount}");
    }

    private void InitializeListOfSaves()
    {
        Debug.Log($"InitializeListOfSaves");

        if (SavedFilesScrollViewContent != null)
        {
            ClearChildren(SavedFilesScrollViewContent);

            string path = Application.persistentDataPath;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] info = dir.GetFiles("*.save");
            int maxCharacterCount = 0;

            foreach (System.IO.FileInfo f in info)
            {
                if (maxCharacterCount < f.Name.Length)
                {
                    maxCharacterCount = f.Name.Length;
                }

                GameObject buttonGO = Instantiate(buttonPrefub, transform.position, Quaternion.identity);
                buttonGO.name = "Button";
                buttonGO.transform.parent = SavedFilesScrollViewContent;

                Button b = buttonGO.GetComponent<Button>();
                b.onClick.AddListener(delegate {_saveFileName = f.Name;});

                RectTransform buttonRT = buttonGO.GetComponent<RectTransform>();
                buttonRT.anchoredPosition = new Vector2(0.5f, 0.5f);
                buttonRT.anchorMin = new Vector2(0f, 1f);
                buttonRT.anchorMax = new Vector2(1f, 1f);
                buttonRT.pivot = new Vector2(0f, 0f);
                buttonRT.localScale = new Vector3(1f, 1f, 1f);

                Transform tr = buttonGO.transform.GetChild(0);
                TextMeshProUGUI tx = tr.GetComponent<TextMeshProUGUI>();
                tx.text = f.Name;
            }

            GridLayoutGroup glg = SavedFilesScrollViewContent.GetComponent<GridLayoutGroup>();
            int paddingHeight = glg.padding.top + glg.padding.bottom;
            int paddingWidth = glg.padding.left + glg.padding.right;
            int cellHeight = (int)Math.Ceiling(glg.cellSize.y);
            int fontSize = (int)Math.Ceiling(buttonPrefub.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize);
            int cellWidth = maxCharacterCount * (fontSize / 5 * 3);
            glg.cellSize = new Vector2(cellWidth, cellHeight);
            int widthSV = (int)Math.Ceiling(SavedFilesScrollViewContent.parent.parent.GetComponent<RectTransform>().sizeDelta.x);
            RectTransform rt = SavedFilesScrollViewContent.GetComponent<RectTransform>();
            rt.sizeDelta =
                new Vector2(cellWidth - widthSV + paddingWidth * 5, (paddingHeight + SavedFilesScrollViewContent.transform.childCount * cellHeight + cellHeight / 2));
        }
    }

    public void OnClickLoad()
    {
        if(string.IsNullOrWhiteSpace(_saveFileName))
        {
            Debug.Log("_saveFileName is null or white space");
            return;
        }

        LoadGame(_saveFileName);
    }

    public void OnClickDelete()
    {
        if(string.IsNullOrWhiteSpace(_saveFileName))
        {
            Debug.Log("_saveFileName is null or white space");
            return;
        }

        DeleteGame(_saveFileName);
        InitializeListOfSaves();
    }

    public void OnClickSave()
    {
        if (SaveNameInputField != null)
        {
            _saveFileName = SaveNameInputField.transform.GetComponent<TMP_InputField>().text;
        }

        if(string.IsNullOrWhiteSpace(_saveFileName))
        {
            _saveFileName = $"{DateTime.UtcNow.ToString("yyyy-MM-ddThh-mm-ss")}";
        }
        _saveFileName += ".save";
        
        SaveGame(_saveFileName);
    }
}
