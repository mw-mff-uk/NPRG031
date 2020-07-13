using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Avatar : PictureBox
  {
    public const double MAX_STEP_MULTIPLIER = 40.0;
    protected int direction;
    public int CurrentDirection { get => this.direction; }
    public int OppositeDirection { get => Direction.GetOpposite(this.direction); }
    protected double stepSize;
    protected int col = -1;
    public int Col { get => this.col; }
    protected int row = -1;
    public int Row { get => this.row; }
    private Box bBox;
    public Box BBox { get => this.bBox; }
    public bool CanMove(LinkedList<DirectedPoint> stoppingPoints, double multiplier)
    {
      double step = this.stepSize * Math.Min(MAX_STEP_MULTIPLIER, multiplier);

      var iterator = stoppingPoints.Iterator();
      while (!iterator.Done)
        if (iterator.Next().Value.HasCollision(this.direction, this.bBox, step))
          return false;

      return true;
    }
    public new void Move(double multiplier)
    {
      double step = this.stepSize * Math.Min(MAX_STEP_MULTIPLIER, multiplier);

      switch (this.direction)
      {
        case Direction.LEFT:
          this.bBox.Left -= step;
          break;

        case Direction.RIGHT:
          this.bBox.Left += step;
          break;

        case Direction.UP:
          this.bBox.Top -= step;
          break;

        case Direction.DOWN:
          this.bBox.Top += step;
          break;
      }

      this.Left = (int)Math.Round(this.bBox.Left);
      this.Top = (int)Math.Round(this.bBox.Top);
    }
    public bool MaybeMove(LinkedList<DirectedPoint> stoppingPoints, double multiplier)
    {
      if (this.CanMove(stoppingPoints, multiplier))
      {
        this.Move(multiplier);
        return true;
      }

      return false;
    }
    public bool CanTurn(int direction, LinkedList<DirectedPoint> turningPoints)
    {
      if (Direction.IsInvalid(direction))
        return false;

      if (Direction.IsOpposite(this.direction, direction))
        return true;

      var iterator = turningPoints.Iterator();
      while (!iterator.Done)
      {
        DirectedPoint point = iterator.Next().Value;

        if (!point.HasDirection(Direction.GetOpposite(direction)))
          continue;

        if (point.HasCollision(this.direction, this.bBox, this.stepSize))
          return true;
      }

      return false;
    }
    protected virtual void AfterTurn() { }
    public void Turn(int direction)
    {
      this.direction = direction;
      this.AfterTurn();
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public Avatar(int left, int top, int row, int col, int direction, double step)
    {
      this.SizeMode = PictureBoxSizeMode.StretchImage;
      this.BackColor = Color.Transparent;

      this.Left = left;
      this.Top = top;
      this.row = row;
      this.col = col;
      this.direction = direction;
      this.stepSize = step;

      this.Width = Game.AVATAR_SIZE;
      this.Height = Game.AVATAR_SIZE;

      this.bBox = new Box(this.Width, this.Height, this.Left, this.Top);
    }
  }
}
