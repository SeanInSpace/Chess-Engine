using System;
using Godot;

public partial class ChessBoard : Node2D {
  private const int BoardSize = 8;
  public const int SquareSize = 34;
  private Color darkSquareColor = new Color (0.58f, 0.48f, 0.37f);
  private Color lightSquareColor = new Color (.95f, 0.84f, 0.67f);

  public override void _Ready () {
    CreateSquares ();
  }

  private void CreateSquares () {
    for (int i = 0; i < BoardSize; i++) {
      for (int j = 0; j < BoardSize; j++) {
        var square = new ColorRect ();
        square.Size = new Vector2 (SquareSize, SquareSize);
        square.Color = ((i + j) % 2 == 0) ? lightSquareColor : darkSquareColor;
        square.GlobalPosition = new Vector2 (i * SquareSize, j * SquareSize);
        AddChild (square);
      }
    }
  }
}