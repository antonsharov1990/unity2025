public class CharacterModel
{
    private string _sceneName;
    private int _x;
    private int _y;
    private int _z;
    private int _pitch;
    private int _roll;
    private int _yaw;

    public CharacterModel()
    {
        _sceneName = "MainMenu";
        _x = 0;
        _y = 0;
        _z = 0;
        _pitch = 0;
        _roll = 0;
        _yaw = 0;
    }

    public void SetCharacterPlace(string sceneName, int x, int y, int z, int pitch, int roll, int yaw)
    {
        _sceneName = sceneName;
        _x = x;
        _y = y;
        _z = z;
        _pitch = pitch;
        _roll = roll;
        _yaw = yaw;
        //UnityEngine.Debug.Log($"SetCharacterPlace {_sceneName} {_x} {_y} {_z} {_pitch} {_roll} {_yaw}");
    }

    public void LoadCharacterPlace(string sceneName, int x, int y, int z, int pitch, int roll, int yaw)
    {
        _sceneName = sceneName;
        _x = x;
        _y = y;
        _z = z;
        _pitch = pitch;
        _roll = roll;
        _yaw = yaw;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public string GetScene()
    {
        //_sceneName = "Forest";
        return _sceneName;
    }

    public int GetX()
    {
        //_x = 140;
        return _x;
    }

    public int GetY()
    {
        //_y = 30;
        return _y;
    }

    public int GetZ()
    {
        //_z = 100;
        //_z = 500;
        return _z;
    }

    public int GetPitch()
    {
        //_pitch = 1;
        return _pitch;
    }

    public int GetRoll()
    {
        //_roll = 2;
        return _roll;
    }

    public int GetYaw()
    {
        //_yaw = 3;
        return _yaw;
    }
}
