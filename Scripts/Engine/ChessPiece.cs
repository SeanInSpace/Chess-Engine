using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }
public enum PieceColor { White, Black }
public enum Player { White, Black }

public abstract partial class ChessPiece : Sprite2D {
	// Member Variables
	protected PieceType type;
	protected PieceColor color;
	protected Vector2 position;
	protected ChessEngine engine;
	private bool isFlipped;
	protected List<IMove> allowedMoves;
	protected int moveCount;

	// Constructor
	public ChessPiece (PieceType type, PieceColor color, Vector2 position, ChessEngine engine) {
		this.type = type;
		this.color = color;
		this.position = position;
		this.engine = engine;
		allowedMoves = new List<IMove> ();
		Texture = ResourceLoader.Load<Texture2D> ($"res://Assets/Pieces/ChessPieces/{color}{type}.png");
	}

	// Public Properties
	public bool HasMoved { get; private set; } = false;

	// Public Methods
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

	public virtual bool CanMoveTo (Vector2 newPosition) {
		if (allowedMoves.Exists (move => move.IsValidMove (this.position, newPosition, engine))) {
			MarkAsMoved ();
			return true;
		}
		return false;
	}

	public void AddMove (IMove move) {
		allowedMoves.Add (move);
	}

	public void RemoveMove (Type moveType) {
		allowedMoves.RemoveAll (move => move.GetType () == moveType);
	}

	public void ClearAllMoves () {
		allowedMoves.Clear ();
	}

	public void MarkAsMoved () {
		HasMoved = true;
	}

	public void Flip () {
		isFlipped = !isFlipped;
		FlipV = isFlipped;
	}

	// Static Methods
	public static ChessPiece FromFENChar (char c, Vector2 position, ChessEngine engine) {
		PieceColor color = char.IsUpper (c) ? PieceColor.White : PieceColor.Black;
		c = char.ToLower (c);

		switch (c) {
			case 'p':
				return new Pawn (color, position, engine);
			case 'k':
				return new King(color, position, engine);
			case 'q':
				return new Queen(color, position, engine);
			case 'b':
				return new Bishop(color, position, engine);
			case 'n':
				return new Knight(color, position, engine);
			case 'r':
				return new Rook(color, position, engine);
			default:
				return null;
		}
	}
	public int GetMoveCount() {
		return moveCount;
	}
	public void IncrementMoveCount() {
		moveCount++;
	}

}