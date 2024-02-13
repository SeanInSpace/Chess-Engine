using Godot;

public class HorizontalCapture : IMove {
	public bool IsValidMove (Vector2 currentPosition, Vector2 newPosition, ChessEngine engine) {
		// Extract the current and new X positions for clarity
		int currentX = (int) currentPosition.X;
		int currentY = (int) currentPosition.Y;
		int newY = (int) newPosition.Y;
		int newX = (int) newPosition.X;

		// The piece should only move horizontally
		if (currentY != newY) return false;

		// Check if the destination square has an opponent's piece
		ChessPiece currentPiece = engine.GetPiece (currentX, currentY);
		ChessPiece targetPiece = engine.GetPiece (newX, newY);

		if (targetPiece == null || targetPiece.GetColor () == currentPiece.GetColor ()) return false;

		return true;
	}
}