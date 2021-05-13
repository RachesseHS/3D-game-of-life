using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;  
    public Dropdown textureQualityDropdown;
    public Slider musicVolumeSlider;

    public AudioSource musicSource;
    public Resolution[] resolutions;//array de resolutions -> on stoque toute les resolutions disponible sur la machine actuel 
    public GameSettings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        
        gameSettings = new GameSettings();

        //on code en dure les fonctionalités de l'inspecotr d'unity pour eviter la casse
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });



        resolutions = Screen.resolutions;
        //cree la liste des resolutions disponible pour la machine
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFullscreenToggle()
    {

        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;

    }



    public void OnResolutionChange()
    {

    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musicVolumeSlider.value;
    }

    public void SaveSettings()
    {

    }

    public void LoadSettings()
    {

    }
}
