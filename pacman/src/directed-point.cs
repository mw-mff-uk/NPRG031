using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class DirectedPoint
  {
    private const double COLLISION_TOLERANCE = 6.0;
    private int row;
    public int Row { get => this.row; }
    private int col;
    public int Col { get => this.col; }
    private int left;
    private int top;
    private int direction;
    public bool HasDirection(int direction)
    {
      if (Direction.IsInvalid(direction))
        return false;

      return (this.direction & direction) > 0;
    }
    public bool HasCollision(int direction, Box box, double step)
    {
      if (!this.HasDirection(direction))
        return false;

      bool yCollision = true;
      bool xCollision = true;

      step += DirectedPoint.COLLISION_TOLERANCE;

      // Less strict direction
      if (Direction.IsHorizontal(direction))
        yCollision = this.top >= box.Top && this.top <= box.Bottom;
      else
        xCollision = this.left >= box.Left && this.left <= box.Right;

      if (!(yCollision && xCollision))
        return false;

      // More strict direction
      switch (direction)
      {
        case Direction.RIGHT:
          xCollision = box.XCenter >= this.left && box.XCenter - step <= this.left;
          break;

        case Direction.LEFT:
          xCollision = box.XCenter <= this.left && box.XCenter + step >= this.left;
          break;

        case Direction.DOWN:
          yCollision = box.YCenter >= this.top && box.YCenter - step <= this.top;
          break;

        case Direction.UP:
          yCollision = box.YCenter <= this.top && box.YCenter + step >= this.top;
          break;
      }

      return yCollision && xCollision;
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
