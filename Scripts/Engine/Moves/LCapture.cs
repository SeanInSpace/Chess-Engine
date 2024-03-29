using System;
using Godot;
public class LCapture : IMove {
  private int forward;
  private int sideways;

  public LCapture (int forward, int sideways) {
    this.forward = forward;
    this.sideways = sideways;
  }

  public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
    int currentX = (int) currentPosition.X;
    int currentY = (int) currentPosition.Y;
    int newX = (int) newPosition.X;
    int newY = (int) newPosition.Y;
    ChessPiece currentPiece = engine.GetPiece (currentX, currentY);
    ChessPiece targetPiece = engine.GetPiece (newX, newY);

    return currentPosition != newPosition &&
      IsMovingInLShape (currentX, newX, currentY, newY) &&
      IsValidTarget (currentPiece, targetPiece);
  }

  private bool IsMovingInLShape (int currentX, int newX, int currentY, int newY) {
    return (Math.Abs (newX - currentX) == forward && Math.Abs (newY - currentY) == sideways) ||
      (Math.Abs (newX - currentX) == sideways && Math.Abs (newY - currentY) == forward);
  }

  private static bool IsValidTarget (ChessPiece currentPiece, ChessPiece targetPiece) {
    return targetPiece != null && targetPiece.GetColor () != currentPiece.GetColor ();
  }
}