using System;
using Godot;

public class OrthogonalCapture : IMove {
  private readonly int distance;

  public OrthogonalCapture (int distance) {
    this.distance = distance;
  }

  public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
    if (currentPosition == newPosition) return false; // The positions are the same

    int currentX = (int) currentPosition.X;
    int currentY = (int) currentPosition.Y;
    int newX = (int) newPosition.X;
    int newY = (int) newPosition.Y;

    return currentPosition != newPosition &&
      IsMovingOrthogonally (currentX, newX, currentY, newY) &&
      IsMovingWithinDistance (newY, currentY) &&
      !IsPieceBlockingPath (currentX, newX, currentY, newY, engine) &&
      IsValidTarget (newX, newY, engine);
  }

  private static bool IsMovingOrthogonally (int currentX, int newX, int currentY, int newY) {
    return currentX == newX || currentY == newY;
  }

  private bool IsMovingWithinDistance (int newY, int currentY) {
    return Math.Abs (newY - currentY) <= distance;
  }

  private static bool IsPieceBlockingPath (int currentX, int newX, int currentY, int newY, ChessEngine engine) {
    int xDirection = newX > currentX ? 1 : -1;
    int yDirection = newY > currentY ? 1 : -1;

    // If moving along the x-axis
    if (currentY == newY) {
      for (int x = currentX + xDirection; x != newX; x += xDirection) {
        if (engine.GetPiece (x, currentY) != null) {
          return true;
        }
      }
    }
    // If moving along the y-axis
    else if (currentX == newX) {
      for (int y = currentY + yDirection; y != newY; y += yDirection) {
        if (engine.GetPiece (currentX, y) != null) {
          return true;
        }
      }
    }

    return false;
  }

  private static bool IsValidTarget (int newX, int newY, ChessEngine engine) {
    ChessPiece targetPiece = engine.GetPiece (newX, newY);
    return targetPiece != null && targetPiece.GetColor () != engine.GetPiece (newX, newY).GetColor ();
  }
}