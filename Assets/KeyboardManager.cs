using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private readonly IDictionary<KeyboardGameType, KeyCode> _keyCodes = new Dictionary<KeyboardGameType, KeyCode>();

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
