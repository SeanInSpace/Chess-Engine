using System;
using Godot;

public class DiagonalMove : IMove {
  private readonly int distance;

  public DiagonalMove (int distance) {
    this.distance = distance;
  }

  public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
    int currentX = (int) currentPosition.X;
    int currentY = (int) currentPosition.Y;
    int newX = (int) newPosition.X;
    int newY = (int) newPosition.Y;

    return currentPosition != newPosition &&
      IsMovingDiagonally (currentX, newX, currentY, newY) &&
      IsMovingWithinDistance (newY, currentY) &&
      !IsPieceBlockingPath (currentX, newX, currentY, newY, engine) &&
      IsValidTarget (newX, newY, engine);
  }

  private static bool IsMovingDiagonally (int currentX, int newX, int currentY, int newY) {
    return Math.Abs (currentX - newX) == Math.Abs (currentY - newY);
  }

  private bool IsMovingWithinDistance (int newY, int currentY) {
    return Math.Abs (newY - currentY) <= distance;
  }

  private static bool IsPieceBlockingPath (int currentX, int newX, int currentY, int newY, ChessEngine engine) {
    int xDirection = newX > currentX ? 1 : -1;
    int yDirection = newY > currentY ? 1 : -1;

    int x = currentX + xDirection;
    int y = currentY + yDirection;

    while (x != newX && y != newY) {
      ChessPiece piece = engine.GetPiece (x, y);
      if (piece != null) return true;
      GD.Print (piece);
      x += xDirection;
      y += yDirection;
    }

    return false;
  }

  private static bool IsValidTarget (int newX, int newY, ChessEngine engine) {
    ChessPiece targetPiece = engine.GetPiece (newX, newY);
    return targetPiece == null;
  }
}