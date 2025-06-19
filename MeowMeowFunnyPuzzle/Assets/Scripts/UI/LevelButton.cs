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

    public Image[] stars;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        ActiveStars();
        ShowLevel();
        DecideSprite();

    }

    void ActiveStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = false;
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
