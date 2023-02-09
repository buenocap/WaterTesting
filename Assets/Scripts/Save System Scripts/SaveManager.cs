/*Author: Christian Cerezo */
using UnityEngine;
using System.IO;
public static class SaveManager
{
    //persistant data path for the project
    public static string directory = "/SaveData/";
    //file name to be used to save data
    public static string fileName = "MyData.txt";

    //password used to encrypt/decrypt
    private static string password = "0198oifnvz9283ruf9824hgeapwop8p23";

    /// <summary>
    /// Public fields are saved in JSON format in a text file under the persistentDataPath + directory path
    /// </summary>
    /// <param name="so">Object whose public fields are to be saved </param>
    public static void Save(SaveObject so)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        //JSON generated from public fields of object
        string json = JsonUtility.ToJson(so);

        //JSON is encrypted and saved
        //File.WriteAllText(dir + fileName, json);
        AESEncryptor.Encrypt(json, password, dir + fileName);
    }

    public static SaveObject Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveObject so = new SaveObject();

        if (File.Exists(fullPath))
        {
            string json = AESEncryptor.Decrypt(fullPath, password);
            so = JsonUtility.FromJson<SaveObject>(json);

        }
        else
        {
            Debug.Log("Save file does not exist");
        }

        return so;
    }
}
