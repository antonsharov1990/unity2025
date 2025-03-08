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

    public void SaveCharacterPlace(string sceneName, int x, int y, int z, int pitch, int roll, int yaw)
    {
        _sceneName = sceneName;
        _x = x;
        _y = y;
        _z = z;
        _pitch = pitch;
        _roll = roll;
        _yaw = yaw;
    }

    public string GetScene()
    {
        return _sceneName;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }

    public int GetZ()
    {
        return _z;
    }

    public int GetPitch()
    {
        return _pitch;
    }

    public int GetRoll()
    {
        return _roll;
    }

    public int GetYaw()
    {
        return _yaw;
    }
}
