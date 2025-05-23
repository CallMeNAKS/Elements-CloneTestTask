using System.IO;
using _Elements.CodeBase.Gameplay.Grid.Data;
using UnityEngine;

namespace _Elements.CodeBase.Infrastructure.Service.Save
{
    public class SaveService
    {
        private const string SaveFileName = "save.json";
        private string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

        public void Save(int level, GridElementType[,] gridData)
        {
            var data = new SaveData(level, gridData);
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(SavePath, json);
        }

        public SaveData Load()
        {
            if (!File.Exists(SavePath)) return null;

            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        public void ClearSave()
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);
        }
    }
}