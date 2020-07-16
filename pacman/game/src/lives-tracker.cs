using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class LivesTracker : Label
  {
    int lives = 3;
    public bool GameOver { get => this.lives <= 0; }
    public void AddLive()
    {
      this.lives++;
      this.Text = this.lives + " LIVES";
    }
    public void RemoveLive()
    {
      this.lives--;
      this.Text = this.lives + " LIVES";
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public LivesTracker(int left, int top)
    {
      this.Left = left;
      this.Top = top;

      this.Width = 150;
      this.Height = 20;

      this.Text = "3 LIVES";

      this.Font = new Font("monospace", 16, FontStyle.Bold);
      this.ForeColor = Color.White;
    }
  }
}
