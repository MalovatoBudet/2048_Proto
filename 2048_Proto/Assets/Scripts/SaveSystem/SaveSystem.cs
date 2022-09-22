using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.sav";
        FileStream filesream = new FileStream(path, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        binaryFormatter.Serialize(filesream, progressData);
        filesream.Close();
    }

    public static ProgressData Load()
    {
        string path = Application.persistentDataPath + "/progress.sav";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream filesream = new FileStream(path, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(filesream) as ProgressData;
            filesream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("Save file not found");
            return null;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/progress.sav";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.Log("Save file not found");
        }        
    }
}
