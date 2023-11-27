using System;
using Godot;

public partial class ChessBoard : Node2D {
  public const int BoardSize = 8;
  public const int SquareSize = 34;
  private readonly Color darkSquareColor = new Color (0.58f, 0.48f, 0.37f);
  private readonly Color lightSquareColor = new Color (.95f, 0.84f, 0.67f);

  public override void _Ready () {
    CreateSquares ();
  }

  protected virtual void CreateSquares () {
    for (int i = 0; i < BoardSize; i++) {
      for (int j = 0; j < BoardSize; j++) {
        var square = new ColorRect {
          Size = new Vector2 (SquareSize, SquareSize),
          Color = ((i + j) % 2 == 0) ? lightSquareColor : darkSquareColor,
          GlobalPosition = new Vector2 (i * SquareSize, j * SquareSize)
        };
        AddChild (square);
      }
    }
  }
}