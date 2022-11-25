using UnityEngine;

public class TestkeyInput : InputManager
{
    public string channelName;
    public KeyCode key;

    void Start()
    {
        inputChanels = new string[1];
        inputChanels[0] = channelName;
        InitChanels();
    }

    // Update is called once per frame
    void Update()
    {
        inputs[channelName].Active = Input.GetKey(key);
        inputs[channelName].Pressed = Input.GetKeyDown(key);
        inputs[channelName].Released = Input.GetKeyUp(key);
    }
}
