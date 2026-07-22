using UnityEngine;
using System.Collections;
 
public static class AnimatorExtensions
{
    public static IEnumerator WaitForCurrentAnimation(this Animator animator, int layer = 0)
    {
        while (animator.IsInTransition(layer))
        {
            yield return null;
        }
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        while (!animator.IsInTransition(layer) && stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        }
    }
 
}
 
 