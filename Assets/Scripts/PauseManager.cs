using UnityEngine;

public class PauseManager : MonoBehaviour
{
[SerializeField]
private Animator pauseScreenAnimator;
[SerializeField]
private string pauseSoundName;
[SerializeField]
private GameObject pauseFrame;
private bool isPaused = false;
public void TogglePause()
    {
        isPaused = !isPaused;
        pauseFrame.SetActive(!isPaused);
        if (isPaused)
        {
            PauseGame();
        } 
        else
        {
            ResumeGame();
        }
        SoundManager.instance.Play(pauseSoundName);
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreenAnimator.Play("Show", 0, 0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreenAnimator.Play("Hide", 0, 0f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
