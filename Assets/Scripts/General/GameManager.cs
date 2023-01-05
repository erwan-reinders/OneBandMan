using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject startUI;
    public GameObject endUI;
    public GameObject pauseUI;

    private XRNode controller = XRNode.LeftHand;
    private InputFeatureUsage<bool> pauseInputFeature = CommonUsages.menuButton;
    private InputDevice pasuseInputController;

    private bool canPause;
    private bool inPause;
    private bool wasPressingPause = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : there is more than one GameManager !");
        }

        List<InputDevice> inputControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
        if (inputControllers.Count > 0)
        {
            pasuseInputController = inputControllers[0];
        }
        InputDevices.deviceConnected += InputDevices_deviceConnected;

        UserSettings userSettings;

        string text = Util.ReadFile(Util.GetDataPath() + "/userSettings.json");
        if (text.Equals(""))
        {
            userSettings = new UserSettings();
        }
        else
        {
            userSettings = UserSettings.LoadFromJSON(text);
        }

        canPause = userSettings.canPause;
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        List<InputDevice> inputControllers = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(controller, inputControllers);
        if (inputControllers.Count > 0)
        {
            pasuseInputController = inputControllers[0];
        }
    }
    private bool CheckDeviceInput()
    {
        bool featureState;
        return pasuseInputController.TryGetFeatureValue(pauseInputFeature, out featureState)
            && featureState;
    }

    void Update()
    {
        bool pressingPausingInput = Conductor.Instance.musicStarted && canPause && CheckDeviceInput();
        bool pausingInput = !wasPressingPause && pressingPausingInput;
        if (pausingInput)
        {
            inPause = !inPause;
            SetPause(inPause);
        }
        wasPressingPause = pressingPausingInput;
    }

    public void SetPause(bool pauseState)
    {
        inPause = pauseState;
        pauseUI.SetActive(inPause);
        Conductor.Instance.SetPause(inPause);
    }

    public static void StartGame()
    {
        instance.endUI.SetActive(false);
        instance.startUI.SetActive(false);
        Conductor.Instance.Play();
    }

    public static void EndGame(bool stopSong)
    {
        instance.endUI.SetActive(true);
        Conductor.Instance.Stop(stopSong);
        ScoreManager.instance.SaveCurentScore();
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
