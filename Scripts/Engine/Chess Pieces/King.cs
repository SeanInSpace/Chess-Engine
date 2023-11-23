using System;
using Godot;

public partial class King : ChessPiece {
	private bool hasMoved = false;

	public King (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.King, color, position, engine) {
		// Add the allowed moves
		AddMove (new ForwardMove (1)); // Single step forward
		AddMove (new InitialForwardMove (2)); // Double step forward if not moved yet
		AddMove (new ForwardDiagonalCapture (1)); // Capture move
	}
}