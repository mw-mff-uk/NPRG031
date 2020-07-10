using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class DirectedPointFactory
  {
    private int gapHorizontal;
    private int gapVertical;
    static int[] COLS = new int[] { 44, 0, 175, 220, 340, 0, 0, 0, 0, 720 };
    static int[] ROWS = new int[] { 44, 148, 228, 0, 0, 0, 0, 0, 0, 0 };
    public DirectedPoint Point(int row, int col, int direction)
    {
      int top = DirectedPointFactory.ROWS[row] + this.gapVertical;
      int left = DirectedPointFactory.COLS[col] + this.gapHorizontal;

      DirectedPoint point = new DirectedPoint(row, col, left, top, direction);

      return point;
    }
    public DirectedPointFactory(int gapHorizontal, int gapVertical)
    {
      this.gapHorizontal = gapHorizontal;
      this.gapVertical = gapVertical;
    }
  }
  class DirectedPoint
  {
    private int row;
    public int Row { get => this.row; }
    private int col;
    public int Col { get => this.col; }
    private int left;
    private int top;
    private int direction;
    public bool HasCollision(int direction, Box box)
    {
      if ((this.direction & direction) == 0)
        return false;

      bool yCollision = false;
      bool xCollision = false;

      switch (direction)
      {
        case Direction.RIGHT:
          yCollision = this.top >= box.Top && this.top <= box.Top + box.Height;
          xCollision = box.XCenter >= this.left;
          break;

        case Direction.LEFT:
          yCollision = this.top >= box.Top && this.top <= box.Top + box.Height;
          xCollision = box.XCenter <= this.left;
          break;

        case Direction.DOWN:
          yCollision = this.top <= box.YCenter;
          xCollision = this.left >= box.Left && this.left <= box.Left + box.Width;
          break;

        case Direction.UP:
          yCollision = this.top >= box.YCenter;
          xCollision = this.left >= box.Left && this.left <= box.Left + box.Width;
          break;
      }

      if (xCollision && yCollision)
      {
        Console.WriteLine("{0}-{1} {2}", this.row, this.col, this.direction);
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
