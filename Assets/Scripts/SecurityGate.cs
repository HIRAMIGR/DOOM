using UnityEngine;
 
public class SecurityGate : MonoBehaviour
{
    [SerializeField]
    private string soundName;
    [SerializeField]
    private Animator animator;
    public void OpenGate()
    {
        animator.Play("Open", 0, 0f);
        SoundManager.instance.Play(soundName);
    }
    public void CloseGate()
    {
        animator.Play("Close", 0, 0f);
        SoundManager.instance.Play(soundName);
    }
}
 