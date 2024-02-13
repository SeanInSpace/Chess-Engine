using System;
using System.Diagnostics;
using Godot;

public partial class ChessEngine : Node2D {
  private DebugMovePieces debugMovePieces;
  private ChessPiece[][] board;
  private Player currentPlayer;
  private string startingFEN;
  private Vector2? selectedPiecePosition;
  private Label currentPlayerLabel;

  private const int SquareSize = 34;
  private const int PieceOffset = 17;
  private const float PieceScale = .12f;
  private const int InitialRow = 7;

  public ChessEngine () {
    board = new ChessPiece[8][];
    for (int i = 0; i < 8; i++) {
      board[i] = new ChessPiece[8];
    }
    currentPlayer = Player.White;
    startingFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    //startingFEN = "pppppppp/pppppppp/pppppppp/pppppppp/1pp2pp1/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    selectedPiecePosition = null;
  }

  public void CreateStartingLayout () {
    string[] fenParts = startingFEN.Split (' ');
    string boardFEN = fenParts[0];
    int row = InitialRow;
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
      HandleMouseClick ();
    }
  }

  private void HandleMouseClick () {
    Vector2 clickedPosition = GetGlobalMousePosition ();
    Vector2 boardPosition = (clickedPosition - this.GlobalPosition) / SquareSize;
    Vector2I boardPosInt = new Vector2I ((int) boardPosition.X, (int) boardPosition.Y);

    if (selectedPiecePosition.HasValue) {
      HandleSelectedPieceMove (boardPosInt);
    } else {
      HandlePieceSelection (boardPosInt);
    }
  }

  private void HandleSelectedPieceMove(Vector2I boardPosInt) {
    ChessPiece selectedPiece = GetPiece((int)selectedPiecePosition.Value.X, (int)selectedPiecePosition.Value.Y);
    if (debugMovePieces.IsToggled || selectedPiece.CanMoveTo(new Vector2(boardPosInt.X, boardPosInt.Y))) {
      MovePiece((int)selectedPiecePosition.Value.X, (int)selectedPiecePosition.Value.Y, boardPosInt.X, boardPosInt.Y);
      SwitchPlayer();
    }
    selectedPiecePosition = null;
  }

  private void HandlePieceSelection (Vector2I boardPosInt) {
    ChessPiece clickedPiece = GetPiece (boardPosInt.X, boardPosInt.Y);
    if (clickedPiece != null && clickedPiece.GetColor () == (currentPlayer == Player.White ? PieceColor.White : PieceColor.Black)) {
      selectedPiecePosition = boardPosInt;

    }
  }

  public void MovePiece(int fromX, int fromY, int toX, int toY)
  {
      ChessPiece selectedPiece = board[fromX][fromY];
      ChessPiece targetPiece = board[toX][toY];
      if (targetPiece != null && targetPiece != selectedPiece)
      {
          RemoveChild(targetPiece); // Remove the target piece
          targetPiece.QueueFree(); // Free the node if not needed anymore
      }
      board[toX][toY] = selectedPiece;
      board[fromX][fromY] = null;
      board[toX][toY].Position = new Vector2(toX * SquareSize + PieceOffset, toY * SquareSize + PieceOffset);
      board[toX][toY].UpdatePosition(new Vector2(toX, toY));
      selectedPiecePosition = null;
  }

  public void CreatePieceSprites () {
    for (int x = 0; x < 8; x++) {
      for (int y = 0; y < 8; y++) {
        ChessPiece piece = GetPiece (x, y);
        if (piece != null) {
          // Set the sprite's position
          piece.Position = new Vector2 (x * SquareSize + PieceOffset, y * SquareSize + PieceOffset);
          // Shrink the sprite to fit the square
          piece.Scale = new Vector2 (PieceScale, PieceScale);
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

  public ChessPiece GetPiece(int x, int y) {
      if (x >= 0 && x < 8 && y >= 0 && y < 8) {
          return board[x][y];
      } else {
          return null;
      }
  }

  public void SwitchPlayer () {
    currentPlayer = currentPlayer == Player.White ? Player.Black : Player.White;
  }

  public override void _Ready () {
    ChessBoard chessBoard = new ChessBoard ();
    AddChild (chessBoard);
    FlipPieces ();
    CreateStartingLayout ();
    CreatePieceSprites ();
    debugMovePieces = (DebugMovePieces)GetNode("Camera2D/CanvasLayer/GridContainer/CheckButton");
  }

  public void HighlightPiece(ChessPiece piece)
  {
    // Change the modulate property of the sprite to highlight the piece
    piece.Modulate = new Color(1, 1, 1, 1); // white color
  }

  public void UnhighlightPiece(ChessPiece piece)
  {
    // Reset the modulate property of the sprite to unhighlight the piece
    piece.Modulate = new Color(1, 1, 1, 0.5f); // semi-transparent white color
  }
}