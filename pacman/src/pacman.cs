using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Pacman : PictureBox
  {
    private enum DIRECTION
    {
      UP,
      RIGHT,
      DOWN,
      LEFT
    };
    private DIRECTION direction = DIRECTION.RIGHT;
    public new void Move(int step = 5)
    {
      switch (this.direction)
      {
        case DIRECTION.LEFT:
          this.Left -= step;
          break;

        case DIRECTION.RIGHT:
          this.Left += step;
          break;

        case DIRECTION.UP:
          this.Top -= step;
          break;

        case DIRECTION.DOWN:
          this.Top += step;
          break;
      }
    }
    public void TurnLeft()
    {
      this.direction = DIRECTION.LEFT;
    }
    public void TurnRight()
    {
      this.direction = DIRECTION.RIGHT;
    }
    public void TurnUp()
    {
      this.direction = DIRECTION.UP;
    }
    public void TurnDown()
    {
      this.direction = DIRECTION.DOWN;
    }
    public void Spawn(Form parent)
    {
      this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman.png");
      this.SizeMode = PictureBoxSizeMode.StretchImage;

      this.Left = 20;
      this.Top = 54;
      this.Width = 26;
      this.Height = 26;

      this.Parent = parent;
      this.Text = "P";
    }
  }
}
