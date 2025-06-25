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

    void Awake()
    {
        if (gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
            Load(); // Load ngay trong Awake
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        SaveData data = new SaveData();
        data = saveData;
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Save");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
                saveData = formatter.Deserialize(file) as SaveData;
                file.Close();
                Debug.Log("Loaded");

                // Kiểm tra và khởi tạo lại nếu dữ liệu bị lỗi
                ValidateData();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error loading save data: " + e.Message);
                InitializeDefaultData();
            }
        }
        else
        {
            InitializeDefaultData();
        }
    }

    void ValidateData()
    {
        // Kiểm tra nếu dữ liệu bị null hoặc rỗng
        if (saveData == null || saveData.isActive == null || saveData.isActive.Length == 0)
        {
            InitializeDefaultData();
        }
    }

    void InitializeDefaultData()
    {
        saveData = new SaveData();
        // Khởi tạo cho 27 level (3 trang x 9 level)
        int totalLevels = 27;
        saveData.isActive = new bool[totalLevels];
        saveData.stars = new int[totalLevels];
        saveData.highScores = new int[totalLevels];

        // Level 1 mở sẵn
        saveData.isActive[0] = true;

        Debug.Log("Initialized default game data with " + totalLevels + " levels");
        Save(); // Lưu dữ liệu mặc định
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }
}
