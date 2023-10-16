public class InitialForwardMove : IMove
{
	private int bonusDistance;

	public InitialForwardMove(int bonusDistance)
	{
		this.bonusDistance = bonusDistance;
	}

	public bool IsValidMove(Vector2 currentPosition, Vector2 newPosition, ChessEngine engine)
	{
		// Extract the current and new Y positions for clarity
		int currentY = (int)currentPosition.y;
		int newY = (int)newPosition.y;

		ChessPiece currentPiece = engine.GetPiece((int)currentPosition.x, currentY);

		// Check if the piece has not moved yet
		if (currentPiece.HasMoved) return false;

		// The piece should only move vertically
		if (currentPosition.x != newPosition.x) return false;

		// Check if the piece is moving the bonus distance forward
		int expectedYChange = currentPiece.GetColor() == PieceColor.White ? bonusDistance : -bonusDistance;
		if (newY - currentY != expectedYChange) return false;

		// Check if the destination square is unoccupied
		ChessPiece targetPiece = engine.GetPiece((int)newPosition.x, newY);
		if (targetPiece != null) return false;

		return true;
	}
}
