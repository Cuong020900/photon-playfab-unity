using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class LanguageManager : ILanguageManager
{
    public Dictionary<string, string> languageDictionary;
    private string _currentLanguage;
    
    private void Awake()
    {
        _currentLanguage = "EN";
        LoadMappingFileJson();
    }

    public override string Translate(string text)
    {
        throw new System.NotImplementedException();
    }

    void LoadMappingFileJson()
    {
        // var text = File.ReadAllText();
        // languageDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
    }
    
    public void GetAllLanguages()
    {
        throw new System.NotImplementedException();
    }
    
    public void SetActiveLanguage()
    {
        throw new System.NotImplementedException();
    }
}
