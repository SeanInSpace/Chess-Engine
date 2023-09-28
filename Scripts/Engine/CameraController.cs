using System;
using System.Diagnostics;
using Godot;

public partial class CameraController : Camera2D {
  private const int BoardSize = 8;
  private const float SquareSize = 34.0f;
  private float targetZoom = 0.4f;
  private Vector2 dragOrigin;
  public override void _Ready () {
    UpdateZoom ();
    CenterCamera ();
  }

  private void UpdateZoom () {
    Vector2 screenSize = GetViewport ().GetVisibleRect ().Size;

    float boardWidth = BoardSize * SquareSize;
    float boardHeight = BoardSize * SquareSize;

    float zoomX = screenSize.X / boardWidth;
    float zoomY = screenSize.Y / boardHeight;

    targetZoom = Mathf.Min (zoomX, zoomY);
    Zoom = new Vector2 (targetZoom, targetZoom);

  }

  private void CenterCamera () {
    float boardWidth = BoardSize * SquareSize;
    float boardHeight = BoardSize * SquareSize;
    Position = new Vector2 (boardWidth / 2, boardHeight / 2);
  }

  private void IncreaseZoom () {
    targetZoom += 0.1f;
    Zoom = new Vector2 (targetZoom, targetZoom);
  }
  private void DecreaseZoom () {
    targetZoom -= 0.1f;
    Zoom = new Vector2 (targetZoom, targetZoom);
  }

  public override void _Input (InputEvent @event) {

    if (@event.IsActionPressed ("centerCamera")) {
      CenterCamera ();
    }
    if (@event.IsActionPressed ("increaseZoom")) {
      IncreaseZoom ();
    }
    if (@event.IsActionPressed ("decreaseZoom")) {
      DecreaseZoom ();
    }
    if (@event is InputEventMouseButton buttonEvent && buttonEvent.ButtonIndex == MouseButton.Left && buttonEvent.Pressed) {
      dragOrigin = GetGlobalMousePosition ();
    }

    if (@event is InputEventMouseMotion motionEvent && Input.IsMouseButtonPressed (MouseButton.Left)) {
      Vector2 diff = dragOrigin - GetGlobalMousePosition ();
      Position += diff;
    }
  }
}