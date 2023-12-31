using System;
using Godot;

public class ForwardDiagonalCapture : IMove {
  private readonly int distance;

  public ForwardDiagonalCapture (int distance) {
    this.distance = distance;
  }

  public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
    if (currentPosition == newPosition) return false; // The positions are the same

    int currentX = (int) currentPosition.X;
    int currentY = (int) currentPosition.Y;
    int newX = (int) newPosition.X;
    int newY = (int) newPosition.Y;

    ChessPiece currentPiece = engine.GetPiece (currentX, currentY);
    PieceColor pieceColor = currentPiece.GetColor ();

    return currentPosition != newPosition &&
      IsMovingDiagonally (currentX, newX, currentY, newY) &&
      IsMovingWithinDistance (newY, currentY) &&
      IsMovingInCorrectDirection (newY, currentY, pieceColor) &&
      !IsPieceBlockingPath (currentX, newX, currentY, newY, engine) &&
      IsValidTarget (newX, newY, pieceColor, engine);
  }

  private static bool IsMovingDiagonally (int currentX, int newX, int currentY, int newY) {
    // if |x1 - x2| == |y1 - y2| then the move is diagonal
    return Math.Abs (currentX - newX) == Math.Abs (currentY - newY);
  }

  private bool IsMovingWithinDistance (int newY, int currentY) {
    return Math.Abs (newY - currentY) <= distance;
  }

  private bool IsMovingInCorrectDirection (int newY, int currentY, PieceColor pieceColor) {
    int expectedYChange = pieceColor == PieceColor.White ? distance : -distance;
    if (pieceColor == PieceColor.White) {
      return newY - currentY >= expectedYChange;
    } else {
      return newY - currentY <= expectedYChange;
    }
  }

  private static bool IsPieceBlockingPath (int currentX, int newX, int currentY, int newY, ChessEngine engine) {
    int xDirection = newX > currentX ? 1 : -1;
    int yDirection = newY > currentY ? 1 : -1;

    int x = currentX + xDirection;
    int y = currentY + yDirection;

    while (x != newX && y != newY) {
      ChessPiece piece = engine.GetPiece (x, y);
      if (piece != null) return true;
      x += xDirection;
      y += yDirection;
    }

    return false;
  }

  private static bool IsValidTarget (int newX, int newY, PieceColor pieceColor, ChessEngine engine) {
    ChessPiece targetPiece = engine.GetPiece (newX, newY);
    return targetPiece != null && targetPiece.GetColor () != pieceColor;
  }
}