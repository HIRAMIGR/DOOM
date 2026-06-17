using UnityEngine;
 
public class InputManager : MonoBehaviour
{
 public bool LeftButtonPressed {get; private set;}
 public bool LeftButtonHeld {get; private set;}
 public bool RightButtonPressed {get; private set;}
 public void Update()
{
   LeftButtonPressed = Input.GetMouseButtonDown(0);
   LeftButtonHeld = Input.GetMouseButton(0);
   RightButtonPressed = Input.GetMouseButtonDown(1);
}
}