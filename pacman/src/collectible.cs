using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Collectible : PictureBox
  {
    private const int WIDTH = 6;
    private const int HEIGHT = 6;
    private const int CHERRY_WIDTH = 20;
    private const int CHERRY_HEIGHT = 20;
    private const double CHERRY_CHANCE = 0.0;
    private int xCenter;
    private int yCenter;
    private bool isCherry = false;
    public bool IsCherry { get => this.isCherry; }
    private bool collected = false;
    public bool Collected { get => this.collected; }
    public void Reset()
    {
      this.collected = false;
      this.Left = this.xCenter - this.Width / 2;
      this.Top = this.yCenter - this.Height / 2;
    }
    public void Collect()
    {
      this.collected = true;
      this.Left = 9999;
      this.Top = 9999;
    }
    public bool HasCollision(Box box)
    {
      return (
        this.xCenter >= box.Left && this.xCenter <= box.Right &&
        this.yCenter >= box.Top && this.yCenter <= box.Bottom
      );
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public Collectible(int left, int top)
    {
      this.xCenter = left;
      this.yCenter = top;

      this.Width = Collectible.WIDTH;
      this.Height = Collectible.HEIGHT;

      if (MainClass.rnd.NextDouble() < Collectible.CHERRY_CHANCE)
      {
        this.isCherry = true;

        this.Width = Collectible.CHERRY_WIDTH;
        this.Height = Collectible.CHERRY_HEIGHT;
      }

      this.Left = this.xCenter - this.Width / 2;
      this.Top = this.yCenter - this.Height / 2;

      this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/point.jpg");
      this.SizeMode = PictureBoxSizeMode.StretchImage;
    }
  }
}
