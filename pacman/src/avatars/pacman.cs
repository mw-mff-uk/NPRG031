using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Pacman : PictureBox
  {
    private int direction;
    private int stepSize = 5;
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
          if (point.HasCollision(this.direction, box))
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
    public bool CanTurn(int direction, DirectedPoint[] turningPoints)
    {
      if (Direction.IsInvalid(direction))
        return false;

      if (Direction.IsOpposite(this.direction, direction))
        return true;

      Box box = this.GetBox();

      foreach (var point in turningPoints)
        if (point.HasDirection(Direction.GetOpposite(direction)) && point.HasCollision(this.direction, box))
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
    public Pacman(int left, int top, int row, int col, int direction)
    {
      this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman.jpg");
      this.SizeMode = PictureBoxSizeMode.StretchImage;

      this.Left = left;
      this.Top = top;
      this.row = row;
      this.col = col;
      this.direction = direction;

      this.Width = Game.AVATAR_SIZE;
      this.Height = Game.AVATAR_SIZE;
    }
  }
}
