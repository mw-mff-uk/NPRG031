using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Pacman : Avatar
  {
    private Image imgRight;
    private Image imgLeft;
    private Image imgUp;
    private Image imgDown;
    protected override void AfterTurn()
    {
      switch (this.direction)
      {
        case Direction.RIGHT:
          this.Image = this.imgRight;
          break;

        case Direction.LEFT:
          this.Image = this.imgLeft;
          break;

        case Direction.UP:
          this.Image = this.imgUp;
          break;

        case Direction.DOWN:
          this.Image = this.imgDown;
          break;
      }
    }
    public Pacman(int left, int top, int row, int col, int direction, int step) : base(left, top, row, col, direction, step)
    {
      this.imgRight = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman-right.png");
      this.imgLeft = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman-left.png");
      this.imgUp = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman-up.png");
      this.imgDown = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/pacman-down.png");

      this.AfterTurn();
    }
  }
}
