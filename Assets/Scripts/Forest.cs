using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{

    public GameObject player;
    private bool _mustLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start");

        string scene = Current.CharacterModel.GetScene();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene && player != null)
        {
            _mustLoad = true;
        }
    }

    
    void Load()
    {
        Debug.Log($"Load");

        string scene = Current.CharacterModel.GetScene();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene && player != null)
        {
            int x = Current.CharacterModel.GetX();
            int y = Current.CharacterModel.GetY();
            int z = Current.CharacterModel.GetZ();
            int pitch = Current.CharacterModel.GetPitch();
            int roll = Current.CharacterModel.GetRoll();
            int yaw = Current.CharacterModel.GetYaw();
            //Transform player_transform = player.GetComponent<Transform>();
            //player_transform.position = new Vector3(x, y, z);
            //player_transform.rotation = new Quaternion(pitch, roll, yaw, 0);
            UnityEngine.Debug.Log($"Start loading {scene} {x} {y} {z} {pitch} {roll} {yaw}");

            Vector3 position = new Vector3(x, y, z);
            Vector3 eulerAngles = new Vector3(pitch, roll, yaw);
            //GameObject new_pl = Instantiate(player, position, rotation) as GameObject;
            //player = new_pl;
            player.GetComponent<Transform>().position = position;
            player.GetComponent<Transform>().eulerAngles = eulerAngles;

            x = (int)player.GetComponent<Transform>().position.x;
            y = (int)player.GetComponent<Transform>().position.y;
            z = (int)player.GetComponent<Transform>().position.z;
            pitch = (int)player.GetComponent<Transform>().eulerAngles.x;
            roll = (int)player.GetComponent<Transform>().eulerAngles.x;
            yaw = (int)player.GetComponent<Transform>().eulerAngles.x;
            UnityEngine.Debug.Log($"Loaded {scene} {x} {y} {z} {pitch} {roll} {yaw}");
        }

        _mustLoad = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (_mustLoad)
        {
            Load();
        }
        else
        {
            UpdatePlayerData();
        }
    }

    void UpdatePlayerData()
    {
        if (player != null)
        {
            Transform player_transform = player.GetComponent<Transform>();
            Current.CharacterModel.SetCharacterPlace("Forest",
                                                     (int)player_transform.position.x,
                                                     (int)player_transform.position.y,
                                                     (int)player_transform.position.z,
                                                     (int)player_transform.eulerAngles.x,
                                                     (int)player_transform.eulerAngles.y,
                                                     (int)player_transform.eulerAngles.z
                                                    );
        }
    }

    public void OnCallMenu()
    {
        Debug.Log("PauseMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("PauseMenu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void OnState()
    {
        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int x = (int)player.GetComponent<Transform>().position.x;
        int y = (int)player.GetComponent<Transform>().position.y;
        int z = (int)player.GetComponent<Transform>().position.z;
        int pitch = (int)player.GetComponent<Transform>().eulerAngles.x;
        int roll = (int)player.GetComponent<Transform>().eulerAngles.y;
        int yaw = (int)player.GetComponent<Transform>().eulerAngles.z;
        UnityEngine.Debug.Log($"OnState {scene} {x} {y} {z} {pitch} {roll} {yaw}");

        scene = Current.CharacterModel.GetScene();
        x = Current.CharacterModel.GetX();
        y = Current.CharacterModel.GetY();
        z = Current.CharacterModel.GetZ();
        pitch = Current.CharacterModel.GetPitch();
        roll = Current.CharacterModel.GetRoll();
        yaw = Current.CharacterModel.GetYaw();
        UnityEngine.Debug.Log($"OnState = {scene} {x} {y} {z} {pitch} {roll} {yaw}");
    }
}
