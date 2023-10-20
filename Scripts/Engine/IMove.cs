using Godot;
using System;

public interface IMove
{
	bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine);
}
