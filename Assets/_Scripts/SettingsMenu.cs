using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private GameController gameController;

    public AudioMixer audioMixer;
    public AudioMixer audioMixer2;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public GameObject settingsMenu;
    bool paused = false;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        // Set cursor visibility and lock it on center of the screen.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        // Set resolutions to dropdown menu
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)  currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    private void Update()
    {
        // Show pausemenu with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // !settingsMenu.activeSelf
            if (!paused)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        settingsMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        paused = true;
    }

    public void ResumeGame()
    {
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Time.timeScale = 1;
        paused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float musicVolume)
    {
        audioMixer2.SetFloat("musicVolume", musicVolume);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void StartNewGame()
    {
        // Reset save file. Set levelsComplete to 0 and go back to main menu
        gameController.saveData.levelsCompleted = 0;
        SceneManager.LoadScene(0);
    }
}
