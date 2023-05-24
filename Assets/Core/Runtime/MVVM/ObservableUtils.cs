using Newtonsoft.Json;
using UnityEngine;

namespace Core.Runtime.MVVM
{
    public static class ObservableUtils
    {
        public static void ConnectPref<T>(this ObservableData<T> self, string name, T def = default)
        {
            object result = def;
            switch (def)
            {
                case bool b:
                    result = PlayerPrefs.GetInt(name, b ? 1 : 0) > 0;
                    break;
                
                case int i:
                    result = PlayerPrefs.GetInt(name, i);
                    break;

                case float f:
                    result = PlayerPrefs.GetFloat(name, f);
                    break;
                    
                case string s:
                    result = PlayerPrefs.GetString(name,s);
                    break;
                    
                default:
                    if (PlayerPrefs.HasKey(name))
                    {
                        var json = PlayerPrefs.GetString(name);
                        result = JsonConvert.DeserializeObject<T>(json);
                    }
                    else
                    {
                        result = def;
                    }
                    break;
            }
            self.Value = (T)result;

            self.OnValue += a =>
            {
                switch (a)
                {
                    case bool b:
                        PlayerPrefs.SetInt(name, b ? 1 : 0);
                        break;
                    
                    case int i:
                        PlayerPrefs.SetInt(name, i);
                        break;

                    case float f:
                        PlayerPrefs.SetFloat(name, f);
                        break;
                    
                    case string s:
                        PlayerPrefs.SetString(name, s);
                        break;
                    
                    default:
                        PlayerPrefs.SetString(name, JsonConvert.SerializeObject(a));
                        break;
                }
            };
        }
    }
}