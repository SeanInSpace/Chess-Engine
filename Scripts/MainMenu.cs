using System;
using Godot;

public partial class MainMenu : Control {
  enum MenuState { PLAY, MULTIPLAYER, OPTIONS, EXIT, MAIN }

  MenuState state = MenuState.MAIN;

  public override void _Ready () {

  }

  public void _Process (float delta) {
    switch (state) {
      case MenuState.PLAY:
        // Display main menu
        break;
      case MenuState.MULTIPLAYER:
        // Display multiplayer menu
        break;
      case MenuState.OPTIONS:
        // Display options menu
        break;
      case MenuState.EXIT:
        // Exit the game
        GetTree ().Quit ();
        break;
    }
  }

  public void OnPlayPressed () {
    state = MenuState.PLAY;
  }

  public void OnOptionsPressed () {
    state = MenuState.OPTIONS;
  }

  public void OnExitPressed () {
    state = MenuState.EXIT;
  }

  public void OnBackPressed () {
    state = MenuState.MAIN;
  }
}