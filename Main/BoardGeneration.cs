using System;
using Godot;

public partial class BoardGeneration : Node2D {
  [Export] public Sprite2D boardSprite2D;
  [Export] public Node2D cell_container;
  [Export] public int BoardSizeXY = 32;
  [Export] public bool ViewNoise = false;
  [Export] public bool RegenerateCells = false;
  [Export] public int SquareSize = 16;
  public Vector2 CellPositionOffset;
  [Export] public Color colorBlack = new Color (0, 0, 0);
  [Export] public Color colorWhite = new Color (1, 1, 1);
  [Export] public Color colorGray = new Color (0.5f, 0.5f, 0.5f);
  [Export] public int NoiseSeed = 0;
  private FastNoiseLite BaseNoise;
  private Image BoardImage;
  private ImageTexture BoardTexture;

  public override void _Ready () {
    BaseNoise = new FastNoiseLite ();
    CellPositionOffset = new Vector2 (SquareSize, SquareSize);
    InitNoise ();
  }

  public override void _Input (InputEvent @event) {
    if (Input.IsActionJustPressed ("regenerateBoard")) {
      InitNoise ();
    }
  }

  private void InitNoise () {
    BaseNoise.Seed = NoiseSeed;
    BoardImage = BaseNoise.GetImage (BoardSizeXY, BoardSizeXY);

    if (ViewNoise) {
      BoardTexture = new ImageTexture ();
      BoardTexture = ImageTexture.CreateFromImage (BoardImage);
      boardSprite2D.Texture = BoardTexture;
    }

    if (RegenerateCells) {
      GenerateCells (BaseNoise);
    }
  }

  private void GenerateCells (FastNoiseLite fromNoise) {
    for (int i = 0; i < BoardImage.GetHeight (); i++) {
      for (int j = 0; j < BoardImage.GetWidth (); j++) {
        Vector2 cellPos = new Vector2 (i, j);
        ColorRect cell = new ColorRect ();
        cell.Name = "Cell: " + cellPos.ToString ();
        cell.Size = new Vector2 (16, 16);
        cell.Position = cellPos * CellPositionOffset;
        cell.Color = GetCellColor (cellPos);
        cell_container.AddChild (cell);
      }
    }
  }

  private Color GetCellColor (Vector2 cellPos) {
    int posVal = (int) (cellPos.X + cellPos.Y);
    return posVal % 2 == 0 ? colorWhite : colorBlack;
  }
}