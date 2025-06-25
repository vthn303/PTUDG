using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject currentPanel;
    public int page;
    private GameData gameData;
    public int currentLevel = 0;

    void Start()
    {
        gameData = FindObjectOfType<GameData>();

        // Ẩn tất cả panels
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        // Tìm level cao nhất đã mở
        if (gameData != null && gameData.saveData.isActive != null)
        {
            for (int i = 0; i < gameData.saveData.isActive.Length; i++)
            {
                if (gameData.saveData.isActive[i])
                {
                    currentLevel = i;
                }
            }
        }

        // Tính trang hiện tại (9 level mỗi trang)
        page = Mathf.Clamp((int)Mathf.Floor(currentLevel / 9), 0, panels.Length - 1);

        // Hiển thị trang hiện tại
        if (page < panels.Length)
        {
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }

    public void PageRight()
    {
        if (page < panels.Length - 1)
        {
            currentPanel.SetActive(false);
            page++;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }

    public void PageLeft()
    {
        if (page > 0)
        {
            currentPanel.SetActive(false);
            page--;
            currentPanel = panels[page];
            currentPanel.SetActive(true);
        }
    }
}
