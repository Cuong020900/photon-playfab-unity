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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio.Play(clip);
        }
    }
}
