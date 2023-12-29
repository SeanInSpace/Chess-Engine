using System;
using Godot;

public partial class Rook : ChessPiece {

	public Rook (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Rook, color, position, engine) {
		AddMove (new OrthogonalMove (8));
		AddMove (new OrthogonalCapture (8));
	}
}