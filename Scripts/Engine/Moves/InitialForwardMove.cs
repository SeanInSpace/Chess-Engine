using System;
using Godot;

public class InitialForwardVerticalMove : IMove {
	private int distance;

	public InitialForwardVerticalMove (int distance) {
		this.distance = distance;
	}

	public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
		int currentX = (int) currentPosition.X;
		int currentY = (int) currentPosition.Y;
		int newX = (int) newPosition.X;
		int newY = (int) newPosition.Y;

		return currentPosition != newPosition &&
			IsMovingForward (currentX, newX, currentY, newY) &&
			IsMovingWithinDistance (newY, currentY) &&
			!IsPieceBlockingPath (currentX, currentY, newY, engine) &&
			IsValidTarget (newX, newY, engine);
	}
	private static bool IsMovingForward (int currentX, int newX, int currentY, int newY) {
		return currentX == newX && newY > currentY;
	}
	private bool IsMovingWithinDistance (int newY, int currentY) {
		return Math.Abs (newY - currentY) <= distance;
	}
	private static bool IsPieceBlockingPath (int currentX, int currentY, int newY, ChessEngine engine) {
		int yDirection = newY > currentY ? 1 : -1;

		int y = currentY + yDirection;

		while (y != newY) {
			ChessPiece piece = engine.GetPiece (currentX, y);
			if (piece != null) return true;
			y += yDirection;
		}

		return false;
	}
	private static bool IsValidTarget (int newX, int newY, ChessEngine engine) {
		ChessPiece targetPiece = engine.GetPiece (newX, newY);
		return targetPiece == null;
	}
}