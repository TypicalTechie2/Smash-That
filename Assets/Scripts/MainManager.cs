using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Button playButton;
    public Image settingsScreenImage;
    public Image settingsImage;
    public MainCameraController mainCameraController;
    public SettingsImage settingsImageScript;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        playButton.gameObject.SetActive(false);
        settingsImage.gameObject.SetActive(false);
        StartCoroutine(mainCameraController.MoveCamera());
    }

    public void SettingsScreen()
    {
        settingsImage.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsScreenImage.gameObject.SetActive(true);
        settingsImageScript.Initialize();
        StartCoroutine(settingsImageScript.ZoomToFinalScale());
    }

    public void CloseSettingsScreen()
    {
        settingsScreenImage.gameObject.SetActive(false);
        settingsImage.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
    }
}
