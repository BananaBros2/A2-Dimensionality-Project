using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Save_Manager : MonoBehaviour
{
    
    public static Save_Manager instance; // this just allows any script to access this (if it's running in the scene)
    public SaveData saveData; // the data to save to file

    public bool hasLoaded = false; // bool to check globally so enything using this can check if this is active

    private void Awake()
    {
        instance = this; // this will make a instance so the 

        Load();
    }

    public void Save() // This saves any data set on this script to the file
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + saveData.saveName + ".datafile", FileMode.Create);
        serializer.Serialize(stream, saveData);
        stream.Close();
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + saveData.saveName + ".datafile"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + saveData.saveName + ".datafile", FileMode.Open);
            saveData = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loaded" + dataPath + "/" + saveData.saveName + ".datafile");
            hasLoaded = true;
        }
        else
        {
            Debug.LogError("COULD NOT FIND SAVE! on path ~ " + dataPath + "/" + saveData.saveName + ".datafile | Generating new blank slate. This is not recommened!");
            hasLoaded = false;

            //saveData.masterVolumeSave = 1f; >examples<

            saveData.FullscreenMode = 4;

            saveData.isTimerVisible = false;

            //saveData.ScreenResolution = 3;

            //saveData.HighScore = 0;

            //saveData.FOV = 100;

            //saveData.MainMouseSensitivity = 1f;

            Save();
            Load();
        }
    }

    public void DeleteSaveData() // Call this to delete EVERY THING in the save
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + saveData.saveName + ".datafile"))
        {
            File.Delete(dataPath + "/" + saveData.saveName + ".datafile");
        }
    }
}

//Save Data for saving
[System.Serializable]
public class SaveData
{
    public string saveName = "GAMEDATA";


    //public float masterVolumeSave; >examples<

    public int FullscreenMode;

    public bool isTimerVisible;

    //public int ScreenResolution;

    //public int HighScore;

    //public float FOV;

    //public float MainMouseSensitivity;

    //public float Rounds;
}
