using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Database
{
    public class LocalDatabase
    {
        public static string persistentDataPath = Application.persistentDataPath;

        public static void SaveJsonData<T>(T file, string path, string fileName)
        {
            var content = path + fileName;
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            
            File.WriteAllText(content, JsonConvert.SerializeObject(file));
        }
        
        
        public static T LoadJsonData<T>(string path, string fileName)
        {
            var content = path +fileName;
            if (!new DirectoryInfo(path).Exists) return default; // file doesNot Exist
            return !new FileInfo(content).Exists ? default : JsonConvert.DeserializeObject<T>(File.ReadAllText(content));
        }

        public static async void SaveJsonDataAsync<T>(T file, string path, string fileName, Action<string> result = null)
        {
            var content = path + fileName;
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();
            try
            {
                await File.WriteAllTextAsync(content, JsonConvert.SerializeObject(file));
                if (result == null) return;
                result.Invoke($"Data Saved Successfully At {content}");
            }
            catch (Exception io)
            {
                if (result == null) return;
                result.Invoke($"Error Saving the File {io.Message}");
            }
        }
        
        public static async void LoadJsonDataAsync<T>(string path, string fileName, Action<T> result = null)
        {
            var content = path + fileName;
            if (!new DirectoryInfo(path).Exists) return; // file doesNot Exist
            if (!new FileInfo(content).Exists) return;
            Debug.Log("Reading data from "+ fileName);
            var data = await File.ReadAllTextAsync(content);
            result?.Invoke(JsonConvert.DeserializeObject<T>(data));
        }
    }
}