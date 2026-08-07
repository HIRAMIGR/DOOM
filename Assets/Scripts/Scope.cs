using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
[SerializeField]
private Animator animator;
[SerializeField]
private Color idleColor;
[SerializeField]
private Color aimingColor;
[SerializeField]
private Image scopeImage;
private bool isAiming = true;
private void Awake()
    {
        ChangeToIdleColor();
    }
public void ChangeToIdleColor()
    {
        if (!isAiming) return;
        isAiming = false;
        scopeImage.color = idleColor;
    }
    public void ChangeToAimingColor()
    {
        if (isAiming)return;
        scopeImage.color = aimingColor;
        isAiming = true;
    }
    public void PlayFireAnimation()
    {
        animator.Play("Fire", 0, 0f);
    }
}
