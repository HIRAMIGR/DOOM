using UnityEngine;
using UnityEngine.Events;
public class InputManager : MonoBehaviour
{
[SerializeField]
private UnityEvent OnPKeyPressed;
[SerializeField]
private UnityEvent OnScrollUp;
[SerializeField]
private UnityEvent OnScrollDown;
 public bool LeftButtonPressed {get; private set;}
 public bool LeftButtonHeld {get; private set;}
 public bool RightButtonPressed {get; private set;}
 public void Update()
{
   LeftButtonPressed = Input.GetMouseButtonDown(0);
   LeftButtonHeld = Input.GetMouseButton(0);
   RightButtonPressed = Input.GetMouseButtonDown(1);
   if (Input.GetKeyDown(KeyCode.P))
      {
         OnPKeyPressed.Invoke();
      }
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      if (scroll > 0f)
      {
         OnScrollUp?.Invoke();
      }
      else if(scroll < 0f)
      {
         OnScrollDown?.Invoke();
      }
}
}