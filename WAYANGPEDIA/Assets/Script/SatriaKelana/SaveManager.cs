using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New save manager", menuName = "Manager/Save")]
    public class SaveManager : ScriptableObject
    {
        private readonly List<IPersistent> _items = new();

        public void Register(IPersistent item)
        {
            _items.Add(item);
        }

        public void LoadAll()
        {
            foreach (var item in _items)
            {
                item.Load();
            }
        }

        public void BinarySave<T>(T data, string name)
        {
            var path = GetPath(name);
            using var fs = new FileStream(path, FileMode.Create);
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Error saving file `{path}`: {e.Message}", this);
                throw;
            }
        }

        public bool BinaryLoad<T>(string name, out T output)
        {
            var path = GetPath(name);
            if (!File.Exists(path))
            {
                output = default(T);
                return false;
            }
            using var fs = new FileStream(path, FileMode.Open);
            var formatter = new BinaryFormatter();
            try
            {
                output = (T)formatter.Deserialize(fs);
                return true;
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Error loading file `{path}`: {e.Message}", this);
                output = default(T);
                return false;
            }
        }

        public string GetPath(string name)
        {
            return $"{Application.persistentDataPath}/{name}.wayangpedia";
        }

        public void SaveAll()
        {
            Debug.Log("Saving all data..");
            foreach (var item in _items)
            {
                item.Save();
            }
            Debug.Log("Saving complete");
        }
    }
}