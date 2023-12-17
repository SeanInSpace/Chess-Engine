using System;
using Godot;

public partial class Rook : ChessPiece {
	private bool hasMoved = false;

	public Rook (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Rook, color, position, engine) {
		// Add the allowed moves
		AddMove (new ForwardMove (1)); // Single step forward
		AddMove (new InitialForwardMove (2)); // Double step forward if not moved yet
		AddMove (new HorizontalMove (8)); // Horizontal move
	}
}