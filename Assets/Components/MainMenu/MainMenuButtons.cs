using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public LogoAnim creditsObj;
    private void Start()
    {
        creditsObj.gameObject.SetActive(false);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        creditsObj.gameObject.SetActive(true);
        creditsObj.AnimStartt();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
