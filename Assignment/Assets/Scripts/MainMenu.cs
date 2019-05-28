using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text statText; 

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    // Gets a list of resolutions, formats them and makes the currently used one selected
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        //create formatted string in order to add to options
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameEnd"){

            int deathCount = PlayerController.totalDeathCount;
            int coinCount = PlayerController.totalCoinCount;

            statText.text = "You beat the game while picking up " + coinCount + " coins and only dying " + deathCount + " times!";
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Select Level");
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        Debug.Log(qualityIndex);
    }

    public void SetFullscreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
