using System;
using Godot;

public partial class Queen : ChessPiece {

	public Queen (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Queen, color, position, engine) {
		AddMove (new DiagonalMove (8));
		AddMove (new DiagonalCapture (8));
		AddMove (new OrthogonalMove (8));
		AddMove (new OrthogonalCapture (8));
	}
}