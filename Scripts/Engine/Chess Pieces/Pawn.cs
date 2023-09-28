using Godot;

public partial class Pawn : ChessPiece {
  public Pawn (PieceColor color, Vector2 position, ChessEngine engine) : base (PieceType.Pawn, color, position, engine) {
    Texture = (Texture2D) ResourceLoader.Load<Texture> (color == PieceColor.White ? "res://Assets/Pieces/Chess Pieces/Pawn (1).png" : "res://Assets/Pieces/Chess Pieces/Pawn (2).png");
    Position = position * 34 + new Vector2 (17, 17);

  }
  private const int BoardSize = 8;
  private const int SingleStep = 1;
  private const int DoubleStep = 2;

  public override bool CanMoveTo (Vector2 newPosition) {
    GD.Print ("-------------");
    int currentX = (int) GetPosition ().X;
    GD.Print ($"Current X: {currentX}");
    int currentY = (int) GetPosition ().Y;
    GD.Print ($"Current Y: {currentY}");
    string currentPosChess = $"{(char)(currentX + 'A')}{currentY + 1}";
    string newPosChess = $"{(char)(newPosition.X + 'A')}{newPosition.Y + 1}";

    GD.Print ($"Current position: {currentPosChess}");
    GD.Print ($"New position: {newPosChess}");

    // Diagonal capture
    if (Mathf.Abs (newPosition.X - currentX) == SingleStep && Mathf.Abs (newPosition.Y - currentY) == SingleStep) {
      ChessPiece targetPiece = engine.GetPiece ((int) newPosition.X, (int) newPosition.Y);
      if (targetPiece != null && targetPiece.GetColor () != this.color) {
        GD.Print ("Diagonal capture");
        return true;
      }
    }

    // Single step forward
    if (newPosition.Y - currentY == (this.color == PieceColor.White ? SingleStep : -SingleStep)) {
      ChessPiece targetPiece = engine.GetPiece ((int) newPosition.X, (int) newPosition.Y);

      if (Mathf.Abs (newPosition.X - currentX) == 0 && targetPiece == null) {
        GD.Print ("Single step forward");
        return true;
      }
    }

    // Double step forward from pawn's initial position
    if (currentY == (this.color == PieceColor.White ? SingleStep : BoardSize - SingleStep - 1) && Mathf.Abs (newPosition.X - currentX) == 0) {
      ChessPiece targetPiece = engine.GetPiece ((int) newPosition.X, (int) newPosition.Y);
      ChessPiece targetPiece1 = engine.GetPiece ((int) newPosition.X, (int) (currentY + (color == PieceColor.White ? SingleStep : -SingleStep)));
      GD.Print ($"Target piece 1 at {(int)newPosition.X},{(int)(currentY + (color == PieceColor.White ? SingleStep : -SingleStep))}: {targetPiece1}");
      GD.Print ($"Target piece 2 at {(int)newPosition.X},{(int)newPosition.Y}: {targetPiece}");
      if (targetPiece1 == null && targetPiece == null && Mathf.Abs (newPosition.Y - currentY) == DoubleStep) {
        GD.Print ("Double step forward from pawn's initial position");
        return true;
      }
    }

    GD.Print ("Invalid move");
    return false;
  }

}