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
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            // load như cũ
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Loaded");
        }
        else
        {
            // KHỞI TẠO DỮ LIỆU MẶC ĐỊNH
            saveData = new SaveData();
            int levelCount = 10; // Số lượng level bạn mong muốn, chỉnh lại cho đúng
            saveData.isActive = new bool[levelCount];
            saveData.highScores = new int[levelCount];
            saveData.stars = new int[levelCount];

            // Ví dụ: chỉ mở khóa level 1
            saveData.isActive[0] = true;
            for (int i = 1; i < levelCount; i++)
                saveData.isActive[i] = false;
            for (int i = 0; i < levelCount; i++)
            {
                saveData.highScores[i] = 0;
                saveData.stars[i] = 0;
            }
            Debug.Log("Khởi tạo SaveData mặc định");
            Save(); // Lưu lại file mới luôn
        }
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
