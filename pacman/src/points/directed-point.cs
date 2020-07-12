using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class DirectedPoint
  {
    private int row;
    public int Row { get => this.row; }
    private int col;
    public int Col { get => this.col; }
    private int left;
    private int top;
    private int direction;
    public bool HasDirection(int direction)
    {
      return (this.direction & direction) > 0;
    }
    public bool HasCollision(int direction, Box box)
    {
      if (!this.HasDirection(direction))
        return false;

      bool yCollision = false;
      bool xCollision = false;

      switch (direction)
      {
        case Direction.RIGHT:
          yCollision = this.top >= box.Top && this.top <= box.Top + box.Height;
          xCollision = box.XCenter >= this.left && box.Left <= this.left;
          break;

        case Direction.LEFT:
          yCollision = this.top >= box.Top && this.top <= box.Top + box.Height;
          xCollision = box.XCenter <= this.left && box.Left + box.Width >= this.left;
          break;

        case Direction.DOWN:
          yCollision = this.top <= box.YCenter && this.top >= box.Top;
          xCollision = this.left >= box.Left && this.left <= box.Left + box.Width;
          break;

        case Direction.UP:
          yCollision = this.top >= box.YCenter && this.top <= box.Top + box.Width;
          xCollision = this.left >= box.Left && this.left <= box.Left + box.Width;
          break;
      }

      if (xCollision && yCollision)
      {
        // Console.WriteLine("{0}-{1} {2}", this.row, this.col, this.direction);
        return true;
      }

      return false;
    }
    public DirectedPoint(int row, int col, int left, int top, int direction)
    {
      this.row = row;
      this.col = col;
      this.left = left;
      this.top = top;
      this.direction = direction;
    }
  }
}
