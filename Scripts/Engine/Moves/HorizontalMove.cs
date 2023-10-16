using Godot;
using System;

public class HorizontalMove : IMove
{
	private int distance;

	public HorizontalMove(int distance)
	{
		this.distance = distance;
	}

	public bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine)
	{
		// Implement the logic for horizontal move validation
		// Use the distance and engine as needed
		return true; // Placeholder
	}
}
