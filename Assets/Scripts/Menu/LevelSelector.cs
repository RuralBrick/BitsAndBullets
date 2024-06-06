using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Toggle[] toggles = new Toggle[9];

    private void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = GameOverManager.instance.GetLevelSelection(i);
        }
    }

    public void SetLevel0(bool selection)
    {
        GameOverManager.instance.SetLevel(0, selection);
    }

    public void SetLevel1(bool selection)
    {
        GameOverManager.instance.SetLevel(1, selection);
    }

    public void SetLevel2(bool selection)
    {
        GameOverManager.instance.SetLevel(2, selection);
    }

    public void SetLevel3(bool selection)
    {
        GameOverManager.instance.SetLevel(3, selection);
    }

    public void SetLevel4(bool selection)
    {
        GameOverManager.instance.SetLevel(4, selection);
    }

    public void SetLevel5(bool selection)
    {
        GameOverManager.instance.SetLevel(5, selection);
    }

    public void SetLevel6(bool selection)
    {
        GameOverManager.instance.SetLevel(6, selection);
    }

    public void SetLevel7(bool selection)
    {
        GameOverManager.instance.SetLevel(7, selection);
    }

    public void SetLevel8(bool selection)
    {
        GameOverManager.instance.SetLevel(8, selection);
    }

    public void ReturnToMainMenu()
    {
        GameOverManager.instance.ReturnToMainMenu();
    }
}
