using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string VolumePrefKey = "VolumePref";
    public AudioSource backgroundMusic;
    public Slider volumeSlider;
    public AudioSource gameAudio;
    public AudioClip pauseClip;
    public AudioClip resumeClip;
    public AudioClip volumeButtonClip;
    public AudioClip returnToMenuClip;
    public Image volumeScreenImage;
    public Image settingsScreenImage;
    public SettingsImage settingsImageScript;
    public PlayerCameraTransition playerCameraTransitionScript;
    public SpawnManager spawnManager;
    public Player playerScript;
    public Image gamePauseImage;
    public bool isGamePaused = false;
    public bool returnToMenu = false;

    private void Awake()
    {
        // Load volume settings and attach listener to volume slider
        LoadVolume();
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure game time is normal and game is not paused at start
        Time.timeScale = 1;
        isGamePaused = false;
    }

    // Method to set the volume based on slider value
    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        SaveVolume(volume);
    }

    // Method to load volume settings from PlayerPrefs
    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            // Load saved volume and set background music volume and slider value
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
            backgroundMusic.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }

        else
        {
            // If no saved volume, set default volume based on slider value
            backgroundMusic.volume = volumeSlider.value;
        }
    }

    // Method to save volume settings to PlayerPrefs
    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
    }

    // Method to restart the game scene
    public void RestartGame()
    {
        Time.timeScale = 1;  // Ensure the game time is running
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to resume the game
    public void ResumeGame()
    {
        gameAudio.PlayOneShot(resumeClip, 1f);

        gamePauseImage.gameObject.SetActive(true);
        settingsScreenImage.gameObject.SetActive(false);

        // Resume the game
        Time.timeScale = 1;

        isGamePaused = false;
    }

    // Method to pause the game
    public void PauseGame()
    {
        isGamePaused = true;

        gameAudio.PlayOneShot(pauseClip, 1f);

        gamePauseImage.gameObject.SetActive(false);

        StartCoroutine(GamePauseDelay());

        settingsScreenImage.gameObject.SetActive(true);

        settingsImageScript.Initialize();
        StartCoroutine(settingsImageScript.ZoomToFinalScale());
    }

    // Coroutine to introduce a delay before pausing the game
    IEnumerator GamePauseDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Time.timeScale = 0;
    }

    // Method to show the volume screen
    public void VolumeScreen()
    {
        gameAudio.PlayOneShot(volumeButtonClip, 1f);
        settingsScreenImage.gameObject.SetActive(false);
        volumeScreenImage.gameObject.SetActive(true);
    }

    // Method to close the volume screen
    public void CloseVolumeScreen()
    {
        gameAudio.PlayOneShot(volumeButtonClip, 1f);
        volumeScreenImage.gameObject.SetActive(false);
        settingsScreenImage.gameObject.SetActive(true);
    }

    // Method to return to the main menu
    public void ReturnToMenu()
    {
        returnToMenu = true;

        // Hide player UI elements
        playerScript.homeButton.gameObject.SetActive(false);
        playerScript.restartButton.gameObject.SetActive(false);
        playerScript.gameOverText.gameObject.SetActive(false);

        settingsScreenImage.gameObject.SetActive(false);
        gameAudio.PlayOneShot(returnToMenuClip, 0.5f);
        backgroundMusic.Stop();
        Time.timeScale = 1;

        StartCoroutine(playerCameraTransitionScript.MovePlayerCamera());
    }
}
