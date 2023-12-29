using System;
using Godot;

public partial class Pawn : ChessPiece {

	public Pawn (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Pawn, color, position, engine) {
		AddMove (new ForwardMove (1));
		AddMove (new InitialForwardVerticalMove (2));
		AddMove (new ForwardDiagonalCapture (1));
	}
}