using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start");

        string scene = Current.CharacterModel.GetScene();
        int x = Current.CharacterModel.GetX();
        int y = Current.CharacterModel.GetY();
        int z = Current.CharacterModel.GetZ();
        int pitch = Current.CharacterModel.GetPitch();
        int roll = Current.CharacterModel.GetRoll();
        int yaw = Current.CharacterModel.GetYaw();

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene && player != null)
        {
            Transform player_transform = player.GetComponent<Transform>();
            player_transform.position = new Vector3(x, y, z);
            player_transform.rotation = new Quaternion(pitch, roll, yaw, 0);
            Debug.Log($"Start loaded");
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
