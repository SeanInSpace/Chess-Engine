public class HorizontalCapture : IMove
{
	public bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine)
	{
		// Extract the current and new X positions for clarity
		int currentX = (int)currentPosition.x;
		int newY = (int)newPosition.y;
		int newX = (int)newPosition.x;

		// The piece should only move horizontally
		if (currentPosition.y != newY) return false;

		// Check if the destination square has an opponent's piece
		ChessPiece currentPiece = engine.GetPiece(currentX, (int)currentPosition.y);
		ChessPiece targetPiece = engine.GetPiece(newX, newY);

		if (targetPiece == null || targetPiece.GetColor() == currentPiece.GetColor()) return false;

		return true;
	}
}
