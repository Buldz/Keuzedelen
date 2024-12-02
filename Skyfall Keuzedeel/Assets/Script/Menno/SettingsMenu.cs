using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] private TMP_Text volumeTextUI = null;

    public TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    private void Start()//instantly load changed settings
    {
        LoadValues();

        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions(); //clear out all the options in the reselutions dropdown

        List<string> resOptions = new List<string>(); //create a list of stings whats gonne be oure options 

        int currentReselutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) // loop trie each ellemt in the arry
        {
            string resOption = resolutions[i].width + "x" + resolutions[i].height; //create for each a nice formated sting that displays te reselution
            resOptions.Add(resOption);// here we add that to the options to the list

            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentReselutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(resOptions); //when done looping add the reselutions list to the dropdown
        resolutionDropDown.value = currentReselutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    private void Update()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
 
    //Fulscreen or windowed Settings
    public void SetFullscreen(bool IsFullscreen)
    {
        Screen.fullScreen = IsFullscreen;
    }

    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString(".00");
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}