using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Game
  {
    private Form board;
    private LinkedList<Control> toDispose;
    private void OnScreenChange()
    {
      while (!this.toDispose.Empty)
        this.toDispose.ExtractFirst().Dispose();
    }
    private void GameScreen(object sender, EventArgs e)
    {
      this.OnScreenChange();
    }
    private void WelcomeScreen()
    {
      this.OnScreenChange();

      Button btn = new Button();

      btn.Text = "New Game";

      btn.Left = this.board.ClientRectangle.Width / 2 - 50;
      btn.Top = this.board.ClientRectangle.Height / 2 - 20;
      btn.Width = 100;
      btn.Height = 40;
      btn.Parent = this.board;
      btn.Click += this.GameScreen;

      // Fill toDispose Queue
      this.toDispose.InsertLast(btn);
    }
    public void Run()
    {
      this.WelcomeScreen();
      Application.Run(this.board);
    }
    public Game()
    {
      this.board = new Form();
      this.toDispose = new LinkedList<Control>();
    }
  }
}
