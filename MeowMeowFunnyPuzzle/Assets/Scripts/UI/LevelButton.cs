﻿using UnityEngine;
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
        if(gameData != null)
        {
            // decide if the level is active
            if (gameData.saveData.isActive[level - 1])
            {
                isActive = true;

            }
            else
            {
                isActive = false;
            }

            //decide how many stars to active
            starsActive = gameData.saveData.stars[level - 1];
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
