using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Dropdown resolutionDropdown;

    public Resolution[] resolutions;

    void Start()
    {
        /*
        resolutions =  Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();


        int currentResolutionIndex = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
                
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        */

        
        resolutions = Screen.resolutions;
        //cree la liste des resolutions disponible pour la machine
        
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10 (volume) *20 );
        //on utilise Mathf pour convertir la valeur du slider min a -80 et max a 0
        //on aurais aussi pu utiliser -80 et 0 comme valeur min et max avec l'inspecteur dirrectement
        //MusicVolume -> valeur exposer du mastervolume
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    
    public void SetFullscreen (bool is_Fullscreen)
    {
        Screen.fullScreen = is_Fullscreen;
    }
   

}


