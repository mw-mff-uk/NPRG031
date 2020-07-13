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
    private const int CHERRY_WIDTH = 30;
    private const int CHERRY_HEIGHT = 30;
    private const double CHERRY_CHANCE = 0.0025;
    private const int HEART_WIDTH = 30;
    private const int HEART_HEIGHT = 30;
    private const double HEART_CHANCE = 0.001;
    private int xCenter;
    private int yCenter;
    private bool isCherry = false;
    public bool IsCherry { get => this.isCherry; }
    private bool isHeart = false;
    public bool IsHeart { get => this.isHeart; }
    private bool collected = false;
    public bool Collected { get => this.collected; }
    public void Reset()
    {
      this.collected = false;

      this.MaybeMakeBonus();

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
    private void MaybeMakeBonus()
    {
      if (MainClass.rnd.NextDouble() < CHERRY_CHANCE)
      {
        this.isCherry = true;

        this.Width = CHERRY_WIDTH;
        this.Height = CHERRY_HEIGHT;

        this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/cherry.png");
      }
      else if (MainClass.rnd.NextDouble() < HEART_CHANCE)
      {
        this.isHeart = true;

        this.Width = HEART_WIDTH;
        this.Height = HEART_HEIGHT;

        this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/heart.png");
      }
      else
      {
        this.Width = WIDTH;
        this.Height = HEIGHT;

        this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/point.png");
      }
    }
    public Collectible(int left, int top)
    {
      this.xCenter = left;
      this.yCenter = top;

      this.MaybeMakeBonus();

      this.Left = this.xCenter - this.Width / 2;
      this.Top = this.yCenter - this.Height / 2;

      this.SizeMode = PictureBoxSizeMode.StretchImage;
      this.BackColor = Color.Transparent;
    }
  }
}
