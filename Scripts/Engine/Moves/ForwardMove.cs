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

		// Determine the piece's color
		ChessPiece currentPiece = engine.GetPiece (currentX, currentY);
		PieceColor pieceColor = currentPiece.GetColor ();

		// Check if the piece is moving straight forward
		if (currentX != newX) return false; // The piece is trying to move sideways

		// Calculate the expected Y change based on the piece's color and allowed distance
		int expectedYChange = pieceColor == PieceColor.White ? distance : -distance;

		// Make sure the piece is not moving more than the expected distance forward
		// abs (newY - currentY) >= expectedYChange
		if (Math.Abs (newY - currentY) > distance) return false; // The piece is moving too far

		if (pieceColor == PieceColor.White) {
			if (newY - currentY < expectedYChange) return false; // The piece is not moving forward
		} else {
			if (newY - currentY > expectedYChange) return false; // The piece is not moving forward
		}

		// Check if the destination square is unoccupied
		ChessPiece targetPiece = engine.GetPiece (newX, newY);
		if (targetPiece != null) return false; // The destination square is occupied

		return true;
	}
}