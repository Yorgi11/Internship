using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton_template<GameManager>
{
    [SerializeField] private GameObject m_gameOverScreen;
    [SerializeField] private Text m_endText;

    public bool GAMEOVER = false;
    public void ShowGameOverScreen(bool win)
    {
        GAMEOVER = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (win) m_endText.text = "You Win!";
        else m_endText.text = "You Lose";
        m_gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}