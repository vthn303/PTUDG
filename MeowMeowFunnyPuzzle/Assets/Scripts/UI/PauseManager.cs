using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private Board board;
    public bool paused = false;
    public Image soundButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    private SoundManager sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sound = FindObjectOfType<SoundManager>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        pausePanel.SetActive(false);
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOffSprite;
            }
            else
            {
                soundButton.sprite = musicOnSprite;
            }
        }
        else
        {
            soundButton.sprite = musicOnSprite;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (paused && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            board.currentState = GameState.pause;

        }
        if (!paused && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            board.currentState = GameState.move;
        }

    }

    public void SoundButton()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOnSprite;
                PlayerPrefs.SetInt("Sound", 1);
                sound.adjustVolume();
            }
            else
            {
                soundButton.sprite = musicOffSprite;
                PlayerPrefs.SetInt("Sound", 0);
                sound.adjustVolume();
            }
        }
        else
        {
            soundButton.sprite = musicOffSprite;
            PlayerPrefs.SetInt("Sound", 1);
            sound.adjustVolume();
        }
    }

    public void PauseGame()
    {
        paused = !paused;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Splash");
    }
}
