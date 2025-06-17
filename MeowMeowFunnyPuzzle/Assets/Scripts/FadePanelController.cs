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
        }
    }
}
