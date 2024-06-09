using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class OptionData : MonoBehaviour
{
    public static OptionData Instance { get; private set; }

    public float musicVolume { get; private set; }
    public string skinName { get; private set; }

    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Toggle[] skins;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sliderVolume.value = musicVolume;

        skinName = PlayerPrefs.GetString("SkinName", skins[0].gameObject.name);
        foreach (Toggle skin in skins)
        {
            skin.onValueChanged.AddListener((bool on) =>
            {
                if (on)
                {
                    SelectSkin(skin.gameObject.name);
                    UnityEngine.Debug.Log(skin.gameObject.name);
                }
            });

            skin.isOn = (skin.gameObject.name == skinName);
        }
    }

    public void SelectSkin(string name)
    {
        skinName = name;
        foreach (Toggle skin in skins)
        {
            if (skin.gameObject.name == name)
            {
                skin.interactable = false;
            }
            else
            {
                skin.interactable = true;
            }
        }
        PlayerPrefs.SetString("SkinName", skinName);
        PlayerPrefs.Save(); // Save the PlayerPrefs to persist the data
    }


    public void SetVolume()
    {
        musicVolume = sliderVolume.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
