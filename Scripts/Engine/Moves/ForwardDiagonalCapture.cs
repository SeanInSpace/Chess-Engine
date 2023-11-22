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

    if (!IsMovingSideways (currentX, newX)) return false;
    if (!IsMovingWithinDistance (newY, currentY)) return false;
    if (!IsMovingInCorrectDirection (newY, currentY, pieceColor)) return false;
    if (IsPieceBlockingPath (currentX, currentY, newY, pieceColor, engine)) return false;
    if (!IsValidTarget (newX, newY, pieceColor, engine)) return false;

    return true;
  }

  private static bool IsMovingSideways (int currentX, int newX) {
    return currentX != newX;
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

  private static bool IsPieceBlockingPath (int currentX, int currentY, int newY, PieceColor pieceColor, ChessEngine engine) {
    int startY = pieceColor == PieceColor.White ? currentY + 1 : currentY - 1;
    int endY = pieceColor == PieceColor.White ? newY : newY + 1;

    for (int i = startY; i < endY; i++) {
      if (engine.GetPiece (currentX, i) != null) return true;
    }

    return false;
  }

  private static bool IsValidTarget (int newX, int newY, PieceColor pieceColor, ChessEngine engine) {
    ChessPiece targetPiece = engine.GetPiece (newX, newY);
    return targetPiece != null && targetPiece.GetColor () != pieceColor;
  }
}