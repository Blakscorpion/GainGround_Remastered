using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
       Screen.fullScreen = isFullscreen; 
    }


    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[MappingResolutionIndex[resolutionIndex]];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    // RESOLUTION SYSTEM OF THE SETTINGS MENU
    // ======================================

    Resolution[] resolutions;
    Dictionary<int, int> MappingResolutionIndex = new Dictionary<int, int>();

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        // We check all available monitor resolution of the user, at the same refresh rate as his/her current refresh rate
        int currentResolutionIndex = 0;
        int lastAddedOptionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            
            // We only add into the list the ones of the same refreshrate than the current monitor refreshrate (otherwise, too many resolutions to display...)
            if (resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = lastAddedOptionIndex;
                }
                // We do a mapping between the index of the full resolution list, and the cleaned one that we create (to use SetResolution method above, with the good index)
                MappingResolutionIndex.Add(lastAddedOptionIndex, i);
                lastAddedOptionIndex++;
            }
        }

        //TODO : In case there is no match (highly unprobable) just display all resolutions available

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
}
