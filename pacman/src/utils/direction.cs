using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  static class Direction
  {
    public const int INVALID = -1;
    public const int LEFT = 1;
    public const int RIGHT = 1 << 1;
    public const int UP = 1 << 2;
    public const int DOWN = 1 << 3;
    public static bool IsVertical(int dir) => dir == Direction.UP || dir == Direction.DOWN;
    public static bool IsHorizontal(int dir) => dir == Direction.LEFT || dir == Direction.RIGHT;
    public static int GetOpposite(int dir)
    {
      switch (dir)
      {
        case Direction.DOWN: return Direction.UP;
        case Direction.UP: return Direction.DOWN;
        case Direction.LEFT: return Direction.RIGHT;
        case Direction.RIGHT: return Direction.LEFT;
      }

      return Direction.INVALID;
    }
    public static bool IsOpposite(int dir1, int dir2)
    {
      return dir1 == Direction.GetOpposite(dir2);
    }
    public static bool IsInvalid(int dir)
    {
      return dir == Direction.INVALID;
    }
    public static int FromKeyCode(Keys keyCode)
    {
      switch (keyCode)
      {
        case Keys.Right: return Direction.RIGHT;
        case Keys.Left: return Direction.LEFT;
        case Keys.Up: return Direction.UP;
        case Keys.Down: return Direction.DOWN;
      }

      return Direction.INVALID;
    }
  }
}
