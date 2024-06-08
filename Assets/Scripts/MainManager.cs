using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Button playButton;
    public Image ExitImage;
    public MainCameraController mainCameraController;
    public AudioSource mainMenuAudio;
    public AudioClip playButtonClip;
    public AudioClip exitSoundClip;

    public void StartGame()
    {
        mainMenuAudio.PlayOneShot(playButtonClip, 1f);
        playButton.gameObject.SetActive(false);
        ExitImage.gameObject.SetActive(false);
        StartCoroutine(mainCameraController.MoveCamera());
    }

    public void ExitGame()
    {
        StartCoroutine(DelayExit());

        Application.Quit();
    }

    IEnumerator DelayExit()
    {
        mainMenuAudio.PlayOneShot(exitSoundClip, 1f);

        yield return new WaitForSeconds(4f);
    }
}
