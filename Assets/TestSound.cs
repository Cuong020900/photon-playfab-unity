using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip BattleMusic;
    void Start() {
        AudioManager.Instance.PlayMusic(BattleMusic);
    }

    public void OpenFacebook()
    {
        Debug.Log("OpenFacebook");
        Application.OpenURL("http://facebook.com/");
    }
}
