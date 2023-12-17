using System;
using Godot;

public class HorizontalMove : IMove {
    private int distance;
    public HorizontalMove (int distance) {
        this.distance = distance;
    }

    public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
        if (currentPosition == newPosition) return false; // The positions are the same

        int currentX = (int) currentPosition.X;
        int currentY = (int) currentPosition.Y;
        int newX = (int) newPosition.X;
        int newY = (int) newPosition.Y;
        // Check if the new location has a piece, if so return false
        if (engine.GetPiece (newX, newY) != null) return false;
        // Make sure the piece is not moving more than the expected distance forward
        if (Math.Abs (newX - currentX) > distance) return false; // The piece is moving too far

        if (currentY != newY) return false; // Not moving horizontally
        if (IsPieceBlockingPath (currentX, currentY, newX, engine)) return false; // There is a piece blocking the path

        return true;
    }

    private static bool IsPieceBlockingPath (int currentX, int currentY, int newX, ChessEngine engine) {
        int start = Math.Min (currentX, newX) + 1;
        int end = Math.Max (currentX, newX);
        GD.Print ("start: " + start + ", end: " + end);

        for (int i = start; i < end; i++) {
            if (engine.GetPiece (i, currentY) != null) return true;
        }
        return false;
    }
}