using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;

}


public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        }
        else
        {
            Destroy(this.gameObject);

        }
        Load();

    }


    private void Start()
    {

    }

    public void Save()
    {
        //create binary fommatter to read binary files
        BinaryFormatter formatter = new BinaryFormatter();
        // create route from program to the file
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);

        //create blank save
        SaveData data = new SaveData();
        data = saveData;
        //save data in the file
        formatter.Serialize(file, data);
        //clode data stream
        file.Close();
        Debug.Log("Save");


    }

    public void Load()
    {
        //check the save game file exists
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            //create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Loaded");
        }
        else
        {
            saveData = new SaveData();
            saveData.isActive = new bool[100];
            saveData.stars = new int[100];
            saveData.highScores = new int[100];
            saveData.isActive[0] = true;
        }

    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
