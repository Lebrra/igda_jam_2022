using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    Toggle sfxButton;
    [SerializeField]
    Toggle musicButton;
    [SerializeField]
    Button titleButton;

    private void Start()
    {
        sfxButton.onValueChanged.AddListener(SetSFX);
        sfxButton.isOn = GameManager.instance.playerdata.sfxEnabled;

        musicButton.onValueChanged.AddListener(SetMusic);
        musicButton.isOn = GameManager.instance.playerdata.musicEnabled;

        titleButton.onClick.AddListener(() =>
        {
            GameManager.ToTitle();
        });
    }

    public void Open()
    {

    }

    public void Close()
    {

    }

    void SetSFX(bool enabled)
    {
        GameManager.instance.playerdata.sfxEnabled = enabled;
    }

    void SetMusic(bool enabled)
    {
        GameManager.instance.playerdata.musicEnabled = enabled;
    }
}
