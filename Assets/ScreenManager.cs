using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject settingObj;
    public GameObject mainScreenObj;
    public static ScreenManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSetting()
    {
        settingObj.SetActive(true);
        mainScreenObj.SetActive(false);
    }
    
    public void HideSetting()
    {
        settingObj.SetActive(false);
        mainScreenObj.SetActive(true);
    }
}
