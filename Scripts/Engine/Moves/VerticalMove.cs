using System;
using Godot;

public class VerticalMove : IMove {
  public VerticalMove () { }

  public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
    if (currentPosition == newPosition) return false; // The positions are the same

    int currentX = (int) currentPosition.X;
    int currentY = (int) currentPosition.Y;
    int newX = (int) newPosition.X;
    int newY = (int) newPosition.Y;

    // Check if the new location has a piece, if so return false
    if (engine.GetPiece (newX, newY) != null) return false;

    if (currentX != newX) return false; // Not moving vertically
    if (IsPieceBlockingPath (currentX, currentY, newY, engine)) return false; // There is a piece blocking the path

    return true;
  }

  private static bool IsPieceBlockingPath (int currentX, int currentY, int newY, ChessEngine engine) {
    int start = Math.Min (currentY, newY) + 1;
    int end = Math.Max (currentY, newY);
    GD.Print ("start: " + start + ", end: " + end);

    for (int i = start; i < end; i++) {
      if (engine.GetPiece (currentX, i) != null) return true;
    }
    return false;
  }
}