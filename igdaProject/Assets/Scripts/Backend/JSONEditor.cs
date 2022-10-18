using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JSONEditor
{
    /// <summary>
    /// If JSON save data exists, pull and load it. Else make new save data
    /// </summary>
    public static T JSONToData<T>(string fileName)
    {
        if (DoesFileExist(fileName))
        {
            // get from file
            var file = File.ReadAllText(Application.persistentDataPath + "/" + fileName + ".json");
            T data = JsonUtility.FromJson<T>(file);
            return data;
        }
        else
        {
            // form default data
            return default(T);
        }
    }

    /// <summary>
    /// Convert SaveData to JSON and save to persistentDataPath
    /// </summary>
    public static string DataToJSON<T>(T data, string fileName)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", json);

        return json;
    }

    /// <summary>
    /// Compares if file exists or not. Returns bool.
    /// </summary>
    public static bool DoesFileExist(string fileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + fileName + ".json");
    }

    /// <summary>
    /// Deletes data if exists
    /// </summary>
    public static void DeleteData(string fileName)
    {
        if (DoesFileExist(fileName))
        {
            // get from file
            File.Delete(Application.persistentDataPath + "/" + fileName + ".json");
        }
    }
}