using System;
using System.Collections.Generic;
using System.IO;
using CJ.Exp.BusinessLogic;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Hosting.Internal;
using Newtonsoft.Json;

namespace CJ.Exp.LanguageProvider
{
  public class TextCache : ILanguage
  {
    private bool _isInitialised = false;
    private Dictionary<string, string> _textCache;
    private DateTime _textCacheLastModified;
    private bool _checkLastModified;
    
    public void Initialise(bool checkLastModified = false)
    {
      _isInitialised = true;
      _checkLastModified = checkLastModified;
      populateTextCache();
    }

    public string GetText(string path)
    {
      if (!_isInitialised)
      {
        throw new CjExpDeveloperException("Language provider not initialised");
      }

      if (_checkLastModified && _textCacheLastModified < File.GetLastWriteTime(GetTextPath()))
      {
        populateTextCache();
      }

      if (_textCache.TryGetValue(path, out var textValue))
      {
        return textValue;
      }

      throw new CjExpDeveloperException($"Text not found for {path}");
      
    }

    private void populateTextCache()
    {
      string textPath = GetTextPath();
      _textCacheLastModified = File.GetLastWriteTime(textPath);

      using (StreamReader sr = new StreamReader(textPath))
      {
        _textCache = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
      }
    }

    private string GetTextPath()
    {
      return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "text.json");
    }
  }
}
