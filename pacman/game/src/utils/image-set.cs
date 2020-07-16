using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class ImageSet
  {
    public Image Up;
    public Image Down;
    public Image Left;
    public Image Right;
    public Image FromDirection(int direction)
    {
      switch (direction)
      {
        case Direction.UP: return this.Up;
        case Direction.DOWN: return this.Down;
        case Direction.LEFT: return this.Left;
        case Direction.RIGHT: return this.Right;
      }

      return null;
    }
    public ImageSet(Image up, Image down, Image left, Image right)
    {
      this.Up = up;
      this.Down = down;
      this.Left = left;
      this.Right = right;
    }
  }
}
