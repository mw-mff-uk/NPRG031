using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Pacman : Avatar
  {
    private ImageSet normalImages;
    private ImageSet frenzyImages;
    private ImageSet invincibleImages;
    private bool isInvincible = false;
    public bool IsInvincible { get => this.isInvincible; }
    private bool frenzyMode = false;
    public bool FrenzyMode { get => this.frenzyMode; }
    public void StartInvincibility()
    {
      this.isInvincible = true;
      this.RefreshImage();
    }
    public void StopInvincibility()
    {
      this.isInvincible = false;
      this.RefreshImage();
    }
    public void StartFrenzyMode()
    {
      this.isInvincible = false;
      this.frenzyMode = true;
      this.stepSize *= 1.15;

      this.RefreshImage();
    }
    public void StopFrenzyMode()
    {
      this.frenzyMode = false;
      this.stepSize /= 1.15;

      this.RefreshImage();
    }
    private void RefreshImage()
    {
      ImageSet imgSet;

      if (this.frenzyMode)
        imgSet = this.frenzyImages;
      else if (this.isInvincible)
        imgSet = this.invincibleImages;
      else
        imgSet = this.normalImages;

      this.Image = imgSet.FromDirection(this.direction);
    }
    protected override void AfterTurn()
    {
      this.RefreshImage();
    }
    public Pacman(int left, int top, int row, int col, int direction, double step) : base(left, top, row, col, direction, step)
    {
      this.normalImages = new ImageSet(
        Image.FromFile(FileManager.GetImage("pacman-up.png")),
        Image.FromFile(FileManager.GetImage("pacman-down.png")),
        Image.FromFile(FileManager.GetImage("pacman-left.png")),
        Image.FromFile(FileManager.GetImage("pacman-right.png"))
      );

      this.frenzyImages = new ImageSet(
        Image.FromFile(FileManager.GetImage("pacman-up-frenzy.png")),
        Image.FromFile(FileManager.GetImage("pacman-down-frenzy.png")),
        Image.FromFile(FileManager.GetImage("pacman-left-frenzy.png")),
        Image.FromFile(FileManager.GetImage("pacman-right-frenzy.png"))
      );

      this.invincibleImages = new ImageSet(
        Image.FromFile(FileManager.GetImage("pacman-up-invincible.png")),
        Image.FromFile(FileManager.GetImage("pacman-down-invincible.png")),
        Image.FromFile(FileManager.GetImage("pacman-left-invincible.png")),
        Image.FromFile(FileManager.GetImage("pacman-right-invincible.png"))
      );

      this.RefreshImage();
    }
  }
}
