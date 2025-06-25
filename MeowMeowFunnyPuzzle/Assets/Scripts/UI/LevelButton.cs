using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;
    private int starsActive;


    [Header("Level UI")]
    public Image[] stars;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;

    private GameData gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
       
        LoadData();
        ActiveStars();
        ShowLevel();
        DecideSprite();

    }

    void LoadData()
    {
        if (gameData != null && gameData.saveData != null)
        {
            // Kiểm tra bounds trước khi truy cập array
            if (gameData.saveData.isActive != null &&
                level > 0 &&
                level - 1 < gameData.saveData.isActive.Length)
            {
                isActive = gameData.saveData.isActive[level - 1];
            }
            else
            {
                isActive = (level == 1); // Chỉ level 1 mở sẵn
                //Debug.LogWarning("Level " + level + " is out of bounds or data not initialized");
            }

            // Kiểm tra bounds cho stars array
            if (gameData.saveData.stars != null &&
                level > 0 &&
                level - 1 < gameData.saveData.stars.Length)
            {
                starsActive = gameData.saveData.stars[level - 1];
            }
            else
            {
                starsActive = 0;
            }
        }
        else
        {
            // Fallback nếu GameData chưa sẵn sàng
            isActive = (level == 1);
            starsActive = 0;
            //Debug.LogWarning("GameData not found or not initialized");
        }
    }


    void ActiveStars()
    {
        for(int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }

    void DecideSprite()
    {
        if (isActive)
        {
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            levelText.enabled = true;
        }
        else
        {
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;

        }
    }

    void ShowLevel()
    {
        levelText.text = "" + level;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   public void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
