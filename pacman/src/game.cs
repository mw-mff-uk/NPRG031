using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Game
  {
    private const int TICKS_PER_SECOND = 20;
    public const int WIDTH = 400;
    public const int HEIGHT = 534;
    public const int CORRIDOR_SIZE = 28;
    public const int CORRIDOR_GAP = 2;
    private const int N_STATES = 3;
    private const int STATE_BEFORE_START = 0;
    private const int STATE_PLAYING = 1;
    private const int STATE_GAME_OVER = 2;
    private int state = Game.STATE_BEFORE_START;
    private Form board;
    private LinkedList<Control> toDispose;
    private Pacman pacman;
    private Timer timer;
    private TextBox keyboardHandler;
    private int gapVertical;
    private int gapHorizontal;
    private void NextState(object sender = null, EventArgs e = null)
    {
      while (!this.toDispose.Empty)
        this.toDispose.ExtractFirst().Dispose();

      this.state = (this.state + 1) % Game.N_STATES;

      switch (this.state)
      {
        case Game.STATE_BEFORE_START: this.WelcomeScreen(); break;
        case Game.STATE_PLAYING: this.GameScreen(); break;
        case Game.STATE_GAME_OVER: this.GameOverScreen(); break;
      }

      this.keyboardHandler.Focus();
    }
    private void GameScreen()
    {
      this.pacman = new Pacman();
      this.pacman.Spawn(this.board);
      this.toDispose.InsertLast(this.pacman);

      timer.Enabled = true;
    }
    private void WelcomeScreen()
    {
      Button btn = new Button();

      btn.Text = "New Game";

      btn.Left = this.board.ClientRectangle.Width / 2 - 50;
      btn.Top = this.board.ClientRectangle.Height / 2 - 20;
      btn.Width = 100;
      btn.Height = 40;
      btn.Parent = this.board;
      btn.Click += this.NextState;

      // Fill toDispose Queue
      this.toDispose.InsertLast(btn);
    }
    private void GameOverScreen()
    {

    }
    private void Tick(object sender, EventArgs e)
    {
      if (this.state == Game.STATE_PLAYING)
      {
        this.pacman.Move();
      }
    }
    private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (this.state == Game.STATE_PLAYING)
      {
        switch (e.KeyCode)
        {
          case Keys.Right:
            this.pacman.TurnRight();
            break;

          case Keys.Left:
            this.pacman.TurnLeft();
            break;

          case Keys.Up:
            this.pacman.TurnUp();
            break;

          case Keys.Down:
            this.pacman.TurnDown();
            break;
        }
      }
    }
    public void Run()
    {
      this.board.Width = Game.WIDTH + 100;
      this.board.Height = Game.HEIGHT + 100;

      this.gapHorizontal = (this.board.ClientRectangle.Height - Game.HEIGHT) / 2;
      this.gapVertical = (this.board.ClientRectangle.Width - Game.WIDTH) / 2;

      Console.WriteLine("H: {0} | {1}", this.board.ClientRectangle.Width, this.gapHorizontal);
      Console.WriteLine("V: {0} | {1}", this.board.ClientRectangle.Height, this.gapVertical);

      this.WelcomeScreen();
      Application.Run(this.board);
    }
    public Game()
    {
      this.board = new Form();
      this.toDispose = new LinkedList<Control>();

      this.keyboardHandler = new TextBox();
      this.keyboardHandler.Width = 0;
      this.keyboardHandler.Height = 0;
      this.keyboardHandler.Parent = this.board;
      this.board.ActiveControl = this.keyboardHandler;
      this.keyboardHandler.KeyDown += this.OnKeyDown;

      this.timer = new Timer();
      timer.Interval = 1000 / Game.TICKS_PER_SECOND;
      timer.Tick += new System.EventHandler(this.Tick);
      timer.Enabled = false;
    }
  }
}
