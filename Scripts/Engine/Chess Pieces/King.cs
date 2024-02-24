using System;
using Godot;

public partial class King : ChessPiece {

	public King (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.King, color, position, engine) {
		AddMove (new DiagonalMove (1));
		AddMove (new DiagonalCapture (1));
		AddMove (new OrthogonalMove (1));
		AddMove (new OrthogonalCapture (1));
	}
}
