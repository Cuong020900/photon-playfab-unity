using Newtonsoft.Json;
using UnityEngine;

public class SoundSettingModel
{
    public float musicVolume = 0;
    public float effectVolume = 0;
    const string KEY_DATA = "SoundConfig";

    public SoundSettingModel(float _musicVolume = 0f, float _effectVolume = 0f)
    {
        musicVolume = _musicVolume;
        effectVolume = _effectVolume;
    }

    public void Save()
    {
        string s = JsonConvert.SerializeObject(this);

        PlayerPrefs.SetString(KEY_DATA, s);
    }

    public static SoundSettingModel LoadData()
    {
        string s = PlayerPrefs.GetString(KEY_DATA);

        return string.IsNullOrEmpty(s) ? new SoundSettingModel() : JsonConvert.DeserializeObject<SoundSettingModel>(s);
    }
}