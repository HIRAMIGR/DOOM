using UnityEngine;
using System.Collections;
using UnityEngine.Events;                                                                                              

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Animator fade;
    [SerializeField]
    private UnityEvent onSceneStart;
    private void Start()
    {
        onSceneStart?.Invoke();
    }
    public void LoadScene(string sceneName)
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); 
    }
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(LoadSceneWithFadeCoroutine(sceneName));
    }
    private IEnumerator LoadSceneWithFadeCoroutine(string sceneName)
    {
        fade.Play("FadeOut");
        yield return new WaitForSeconds(fade.GetCurrentAnimatorStateInfo(0).length);
        LoadScene(sceneName);
    } 
}
