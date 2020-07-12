using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Monster : Avatar
  {
    public Monster(int left, int top, int row, int col, int direction, int step, string img) : base(left, top, row, col, direction, step)
    {
      this.Image = Image.FromFile(img);
    }
  }
}
