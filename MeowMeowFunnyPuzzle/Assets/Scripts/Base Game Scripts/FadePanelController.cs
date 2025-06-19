using System.Collections;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    public Animator panelAnim;
    public Animator gameInforAnim;

    public void OK()
    {
        if(panelAnim != null && gameInforAnim != null)
        {
            panelAnim.SetBool("Out", true);
            gameInforAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }
    public void GameOver()
    {
        panelAnim.SetBool("Out", false);
        panelAnim.SetBool("Game Over", true);
    }
    IEnumerator GameStartCo()
    {
        yield return new WaitForSeconds(1f);
        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.move; 
    }
}
