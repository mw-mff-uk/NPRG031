using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Monster : Avatar
  {
    private bool isAlive = false;
    public bool IsAlive { get => this.isAlive; }
    public void Kill()
    {
      this.isAlive = false;
    }
    public void Revive()
    {
      this.isAlive = true;
    }
    public Monster(int left, int top, int row, int col, int direction, double step, string img) : base(left, top, row, col, direction, step)
    {
      this.Image = Image.FromFile(img);
    }
  }
}
