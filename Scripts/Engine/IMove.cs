using Godot;
using System;

public interface IMove : Node
{
		bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine);
}
