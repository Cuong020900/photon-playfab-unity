using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance = null;
    private readonly IDictionary<KeyboardGameType, KeyCode> _keyCodes = new Dictionary<KeyboardGameType, KeyCode>();

    private void Awake()
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

    void Start()
    {
        // Load local or default config instead
        KeyboardManager.Instance.SetKeyType(KeyCode.A,KeyboardGameType.ONE);
    }
    
    public void SetKeyType(KeyCode key, KeyboardGameType type)
    {
        _keyCodes.Add(type, key);
    }
    
    public void DeleteKeyType(KeyboardGameType type)
    {
        _keyCodes.Remove(type);
    }
    
    public KeyCode GetKeyOfType(KeyboardGameType type)
    {
        if(!_keyCodes.ContainsKey(type))
        {
            throw new DirectoryNotFoundException();
        }

        _keyCodes.TryGetValue(type, out var result);

        return result;
    }
}
