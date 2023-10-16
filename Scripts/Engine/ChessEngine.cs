using System;
using System.Diagnostics;
using Godot;

public partial class ChessEngine : Node2D {
  private ChessPiece[][] board;
  private Player currentPlayer;
  private string startingFEN;
  private Vector2? selectedPiecePosition;
  private Label currentPlayerLabel;

  public ChessEngine () {
	board = new ChessPiece[8][];
	for (int i = 0; i < 8; i++) {
	  board[i] = new ChessPiece[8];
	}
	currentPlayer = Player.White;
	startingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
	selectedPiecePosition = null;
  }

  public void CreateStartingLayout () {
	string[] fenParts = startingFEN.Split (' ');
	string boardFEN = fenParts[0];
	int row = 7;
	int col = 0;

	foreach (char c in boardFEN) {
	  if (char.IsDigit (c)) {
		col += int.Parse (c.ToString ());
	  } else if (c == '/') {
		row--;
		col = 0;
	  } else {
		board[col][row] = ChessPiece.FromFENChar (c, new Vector2 (col, row), this);
		col++;
	  }
	}
  }
  public override void _Input (InputEvent @event) {
	if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed) {
	  Vector2 clickedPosition = eventMouseButton.Position;

	  clickedPosition = GetGlobalMousePosition ();

	  Vector2 boardPosition = (clickedPosition - this.GlobalPosition) / 34;

	  Vector2I boardPosInt = new Vector2I ((int) boardPosition.X, (int) boardPosition.Y);

	  if (selectedPiecePosition.HasValue) {
		if (MovePiece ((int) selectedPiecePosition.Value.X, (int) selectedPiecePosition.Value.Y, boardPosInt.X, boardPosInt.Y)) {
		  currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
		}
		selectedPiecePosition = null;
	  } else {
		ChessPiece clickedPiece = GetPiece (boardPosInt.X, boardPosInt.Y);
		if (clickedPiece != null && clickedPiece.GetColor () == (currentPlayer == Player.White ? PieceColor.White : PieceColor.Black)) {
		  selectedPiecePosition = boardPosInt;
		}
	  }
	}
  }

  public bool MovePiece (int fromX, int fromY, int toX, int toY) {
	if (!board[fromX][fromY].CanMoveTo (new Vector2 (toX, toY))) {
	  GD.Print ("MovePiece = false");
	  return false;
	}

	GD.Print ("MovePiece = true");
	ChessPiece targetPiece = board[toX][toY];
	if (targetPiece != null) {
	  // Remove the target piece
	  RemoveChild (targetPiece);
	  targetPiece.QueueFree (); // Free the node if not needed anymore
	}
	board[toX][toY] = board[fromX][fromY];
	board[fromX][fromY] = null;
	board[toX][toY].Position = new Vector2 (toX * 34 + 17, toY * 34 + 17);
	board[toX][toY].UpdatePosition (new Vector2 (toX, toY));
	selectedPiecePosition = null;
	SwitchPlayer ();

	return true;
  }

  public void CreatePieceSprites () {
	for (int x = 0; x < 8; x++) {
	  for (int y = 0; y < 8; y++) {
		ChessPiece piece = GetPiece (x, y);
		if (piece != null) {
		  AddChild (piece);
		}
	  }
	}
  }
  public void FlipPieces () {
	for (int x = 0; x < 8; x++) {
	  for (int y = 0; y < 8; y++) {
		var piece = GetPiece (x, y);
		if (piece != null) {
		  piece.Flip ();
		}
	  }
	}
  }

  public Player GetCurrentPlayer () {
	return currentPlayer;
  }

  public ChessPiece GetPiece (int x, int y) {
	return board[x][y];
  }
  public void SwitchPlayer () {
	currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
	UpdateCurrentPlayerLabel (); // Update the label when the player is switched
  }

  private void UpdateCurrentPlayerLabel () {
	currentPlayerLabel.Text = $"Current Player: {currentPlayer}";
  }

  public override void _Ready () {
	ChessBoard chessBoard = new ChessBoard ();
	AddChild (chessBoard);

	FlipPieces ();

	CreateStartingLayout ();
	CreatePieceSprites ();
	currentPlayerLabel = GetNode<Label> ("CurrentPlayerLabel");
	UpdateCurrentPlayerLabel ();
  }

}
