using System;
using Godot;

public partial class Bishop : ChessPiece {

	public Bishop (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Bishop, color, position, engine) {
		AddMove (new DiagonalMove (8));
		AddMove (new DiagonalCapture (8));
	}
}