using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speaker : MonoBehaviour
{
    public Image speakerImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    void Awake()
    {
        // _gameObject = gameObject;
        // TODO: load setting in to playerref
        
        // Get component for speaker
        // _speakerImage = this.GetComponent<Image>();
    }

    public void HandleRenderSpeakerButton(bool isOn)
    {
        speakerImage.sprite = isOn ? this.musicOnSprite : this.musicOffSprite;
    }
}
