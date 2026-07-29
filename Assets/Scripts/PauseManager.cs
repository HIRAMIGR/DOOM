using UnityEngine;

public class PauseManager : MonoBehaviour
{
[SerializeField]
private Animator pauseScreenAnimator;
[SerializeField]
private string pauseSoundName;
[SerializeField]
private GameObject pauseFrame;
[SerializeField]
private FirstPersonLook firstPersonLook;
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
        firstPersonLook.enabled = false;
        Time.timeScale = 0f;
        pauseScreenAnimator.Play("Show", 0, 0f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void ResumeGame()
    {
        firstPersonLook.enabled = true;
        Time.timeScale = 1f;
        pauseScreenAnimator.Play("Hide", 0, 0f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnscaleTime()
    {
        Time.timeScale = 1f;
    }
}
