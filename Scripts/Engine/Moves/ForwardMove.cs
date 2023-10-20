using System;
using Godot;

public class ForwardMove : IMove
{
	private int distance;

	public ForwardMove(int distance)
	{
		this.distance = distance;
	}

	public bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine)
	{
		int currentX = (int)currentPosition.X;
		int currentY = (int)currentPosition.Y;

		int newX = (int)newPosition.X;
		int newY = (int)newPosition.Y;

		// Determine the piece's color
		ChessPiece currentPiece = engine.GetPiece(currentX, currentY);
		PieceColor pieceColor = currentPiece.GetColor();

		// Check if the piece is moving straight forward
		if (currentX != newX) return false; // The piece is trying to move sideways

		// Calculate the expected Y change based on the piece's color and allowed distance
		int expectedYChange = pieceColor == PieceColor.White ? distance : -distance;

		// Check if the piece is moving the expected distance forward
		if (newY - currentY != expectedYChange) return false;

		// Check if the destination square is unoccupied
		ChessPiece targetPiece = engine.GetPiece(newX, newY);
		if (targetPiece != null) return false; // The destination square is occupied

		return true;
	}

}
