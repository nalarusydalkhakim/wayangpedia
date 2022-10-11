using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SatriaKelana
{
    public class SaveManager : MonoBehaviour
    {
        List<IPersistent> _items;
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

        public T BinaryLoad<T>(string name)
        {
            var path = GetPath(name);
            using var fs = new FileStream(path, FileMode.Create);
            var formatter = new BinaryFormatter();
            try
            {
                return (T)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Error loading file `{path}`: {e.Message}", this);
                throw;
            }
        }

        public string GetPath(string name)
        {
            return $"{Application.persistentDataPath}/{name}.wayangpedia";
        }

        private void OnApplicationQuit()
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