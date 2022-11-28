using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFXManager : MonoBehaviour
{
    public AudioManager audio;

    public AudioClip clip;
    
    // Update is called once per frame
    void Update()
    {
        KeyCode oneKey;
        try
        {
            oneKey = KeyboardManager.Instance.GetKeyOfType(KeyboardGameType.ONE);
        }
        catch
        {
            throw new KeyNotFoundException();
        }
        
        if (Input.GetKeyDown(oneKey))
        {
            audio.Play(clip);
        }
    }
}
