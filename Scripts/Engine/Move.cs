using System;
using Godot;

public abstract partial class Move : Node2D {
  public int Distance { get; set; }
  public abstract Vector2[] GetPotentialMoves (PieceColor color, Vector2 position);
}
