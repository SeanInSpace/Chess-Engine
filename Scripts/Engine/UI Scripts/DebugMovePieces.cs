using Godot;
using System;

public partial class DebugMovePieces : CheckButton
{
  private bool isToggled;

  public bool IsToggled {
    get { return isToggled; }
  }
  public void OnToggled(bool buttonState) {
    isToggled = buttonState;
    GD.Print("CheckButton pressed: " + buttonState);
  }
}
