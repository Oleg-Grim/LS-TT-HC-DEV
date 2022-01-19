using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BombGame
{
    public static class SaveSystem
    {
        private const string PATH = "/stats.sd";

        public static void Save(Configuration data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + PATH;
            FileStream stream = new FileStream(path, FileMode.Create);
            Configuration newData = new Configuration(data);
            formatter.Serialize(stream, newData);
            stream.Close();
        }

        public static Configuration Load()
        {
            if (CheckSave())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + PATH, FileMode.Open);
                Configuration data = formatter.Deserialize(stream) as Configuration;
                stream.Close();

                return data;
            }

            return Generate();
        }

        public static bool CheckSave()
        {
            return File.Exists(Application.persistentDataPath + PATH);
        }

        public static Configuration Generate()
        {
            Debug.Log("Generate");
            return new Configuration();
        }
    }
}