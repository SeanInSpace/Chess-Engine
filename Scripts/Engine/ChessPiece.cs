using System;
using Godot;

public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }
public enum PieceColor { White, Black }
public enum Player { White, Black }

public abstract partial class ChessPiece : Sprite2D {
  protected PieceType type;
  protected PieceColor color;
  protected Vector2 position;
  protected ChessEngine engine;
  private bool isFlipped;

  public static ChessPiece FromFENChar (char c, Vector2 position, ChessEngine engine) {
	PieceColor color = char.IsUpper (c) ? PieceColor.White : PieceColor.Black;
	c = char.ToLower (c);

	switch (c) {
	  case 'p':
		return new Pawn (color, position, engine);
	  case 'k':
		return new King (color, position, engine);
	  case 'q':
		return new Queen (color, position, engine);
	  case 'b':
		return new Bishop (color, position, engine);
	  case 'n':
		return new Knight (color, position, engine);
	  case 'r':
		return new Rook (color, position, engine);
	  default:
		return null;
	}
  }
  public void Flip () {
	isFlipped = !isFlipped;
	FlipV = isFlipped;
  }

	public bool HasMoved { get; private set; } = false;

	// Method to call when a piece moves
	public void MarkAsMoved()
	{
		HasMoved = true;
	}
	
  public ChessPiece (PieceType type, PieceColor color, Vector2 position, ChessEngine engine) {
	this.type = type;
	this.color = color;
	this.position = position;
	this.engine = engine;
  }
  public PieceType GetPieceType () {
	return type;
  }
  public PieceColor GetColor () {
	return color;
  }
  public Vector2 GetPosition () {
	return position;
  }
  public void UpdatePosition (Vector2 newPosition) {
	this.position = newPosition;
  }
  public abstract bool CanMoveTo (Vector2 newPosition);
}
