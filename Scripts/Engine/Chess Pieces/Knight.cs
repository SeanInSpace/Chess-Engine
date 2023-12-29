using System;
using Godot;

public partial class Knight : ChessPiece {

	public Knight (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Knight, color, position, engine) {
		AddMove (new LMove (2, 1));
		AddMove (new LCapture (2, 1));
	}
}