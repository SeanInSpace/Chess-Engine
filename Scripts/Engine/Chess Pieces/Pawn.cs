using Godot;
using System;

public partial class Pawn : ChessPiece
{
	private bool hasMoved = false;

	public Pawn(PieceColor color, Vector2 position, ChessEngine engine) : base(PieceType.Pawn, color, position, engine)
	{
		// Add the allowed moves
		AddMove(new ForwardMove(1)); // Single step forward
		AddMove(new InitialForwardMove(2)); // Double step forward if not moved yet
		AddMove(new HorizontalCapture()); // Capture move
	}

	public override bool CanMoveTo(Vector2 newPosition)
	{
		foreach (var move in allowedMoves)
		{
			if (move.IsValidMove(this.position, newPosition, engine))
			{
				// If the move is valid, mark the pawn as having moved
				hasMoved = true;
				return true;
			}
		}
		return false;
	}

	/* 	public bool HasMoved()
		{
			return hasMoved;
		} */
}