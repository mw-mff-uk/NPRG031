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
    public Box GetBox()
    {
      return new Box(this.Width, this.Height, this.Left, this.Top);
    }
    public bool CanMove(DirectedPoint[] stoppingPoints)
    {
      Box box = this.GetBox();
      bool yCollision, xCollision;

      foreach (var point in stoppingPoints)
        if (point.HasCollision(this.direction, box))
          return false;

      return true;
    }
    public new void Move()
    {
      switch (this.direction)
      {
        case Direction.LEFT:
          this.Left -= this.stepSize;
          break;

        case Direction.RIGHT:
          this.Left += this.stepSize;
          break;

        case Direction.UP:
          this.Top -= this.stepSize;
          break;

        case Direction.DOWN:
          this.Top += this.stepSize;
          break;
      }
    }
    public bool CanTurn(int direction, DirectedPoint[] turningPoints)
    {
      if (Direction.IsOpposite(this.direction, direction))
        return true;

      Box box = this.GetBox();

      foreach (var point in turningPoints)
        if (point.HasCollision(this.direction, box))
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
    public Pacman(int left, int top, int direction)
    {
      this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman.jpg");
      this.SizeMode = PictureBoxSizeMode.StretchImage;

      this.Left = left;
      this.Top = top;
      this.direction = direction;

      this.Width = Game.AVATAR_SIZE;
      this.Height = Game.AVATAR_SIZE;
    }
  }
}
