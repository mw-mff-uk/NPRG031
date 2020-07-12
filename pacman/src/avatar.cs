using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Avatar : PictureBox
  {
    private int direction;
    public int CurrentDirection { get => this.direction; }
    public int OppositeDirection { get => Direction.GetOpposite(this.direction); }
    private int stepSize;
    private int col = -1;
    public int Col { get => this.col; }
    private int row = -1;
    public int Row { get => this.row; }
    public Box GetBox()
    {
      return new Box(this.Width, this.Height, this.Left, this.Top);
    }
    public int GetEmptyDistance(DirectedPoint[] stoppingPoints)
    {
      for (int d = 0; d < this.stepSize; d++)
      {
        Box box = this.GetBox();
        box.Left += d;

        foreach (var point in stoppingPoints)
          if (point.HasCollision(this.direction, box, this.stepSize))
            return d;
      }

      return this.stepSize;
    }
    public new void Move(int step = -1)
    {
      if (step <= -1)
        step = this.stepSize;

      switch (this.direction)
      {
        case Direction.LEFT:
          this.Left -= step;
          break;

        case Direction.RIGHT:
          this.Left += step;
          break;

        case Direction.UP:
          this.Top -= step;
          break;

        case Direction.DOWN:
          this.Top += step;
          break;
      }
    }
    public int MaybeMove(DirectedPoint[] stoppingPoints)
    {
      int step = this.GetEmptyDistance(stoppingPoints);
      this.Move(step);
      return step;
    }
    public bool CanTurn(int direction, DirectedPoint[] turningPoints)
    {
      if (Direction.IsInvalid(direction))
        return false;

      if (Direction.IsOpposite(this.direction, direction))
        return true;

      Box box = this.GetBox();

      foreach (var point in turningPoints)
        if (point.HasDirection(Direction.GetOpposite(direction)) && point.HasCollision(this.direction, box, this.stepSize))
          return true;

      return false;
    }
    public void Turn(int direction)
    {
      this.direction = direction;
    }
    public void Spawn(Form parent)
    {
      this.Parent = parent;
    }
    public Avatar(int left, int top, int row, int col, int direction, int step, string img)
    {
      this.Image = Image.FromFile(img);
      this.SizeMode = PictureBoxSizeMode.StretchImage;

      this.Left = left;
      this.Top = top;
      this.row = row;
      this.col = col;
      this.direction = direction;
      this.stepSize = step;

      this.Width = Game.AVATAR_SIZE;
      this.Height = Game.AVATAR_SIZE;
    }
  }
}
