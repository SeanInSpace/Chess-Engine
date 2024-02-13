using System;
using Godot;

public class ForwardMove : IMove {
	private int distance;

	public ForwardMove (int distance) {
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

		if (currentX != newX) return false;

		int expectedYChange = pieceColor == PieceColor.White ? distance : -distance;

		if (Math.Abs (newY - currentY) > distance) return false;

		if (pieceColor == PieceColor.White) {
			if (newY - currentY < expectedYChange) return false;
		} else {
			if (newY - currentY > expectedYChange) return false;
		}

		ChessPiece targetPiece = engine.GetPiece (newX, newY);
		if (targetPiece != null) return false;

		return true;
	}
}