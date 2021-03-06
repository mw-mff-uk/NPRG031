using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{

  class ScoreBoard : Label
  {
    private int score = 0;
    public int Score { get => this.score; }
    public void AddPoint(int inc = 1)
    {
      this.score += inc;
      this.Text = this.score + " POINTS";
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public ScoreBoard(int left = 0, int top = 0)
    {
      this.Left = left;
      this.Top = top;

      this.Width = 200;
      this.Height = 20;

      this.Text = "0 POINTS";

      this.Font = new Font("monospace", 16, FontStyle.Bold);
      this.ForeColor = Color.White;
    }
  }
}
