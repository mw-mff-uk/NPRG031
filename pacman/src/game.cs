using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Game
  {
    public const int WIDTH = 763;
    public const int HEIGHT = 843;
    public const int AVATAR_SIZE = 40;
    private const int N_STATES = 3;
    private const int STATE_BEFORE_START = 0;
    private const int STATE_PLAYING = 1;
    private const int STATE_GAME_OVER = 2;
    private int state = Game.STATE_BEFORE_START;
    private Form board;
    private LinkedList<Control> toDispose;
    private Pacman pacman;
    private Map map;
    private Timer timer;
    private KeyboardHandler keyboardHandler;
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
      this.map = new Map(this.gapHorizontal, this.gapVertical);
      this.pacman = new Pacman(
        this.map.PacmanInitialLeft,
        this.map.PacmanInitialTop,
        this.map.PacmanInitialRow,
        this.map.PacmanInitialCol,
        this.map.PacmanInitialDirection
      );

      this.pacman.Spawn(this.board);
      this.map.Spawn(this.board);

      this.toDispose.InsertLast(this.map);
      this.toDispose.InsertLast(this.pacman);

      this.keyboardHandler.Focus();

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

      this.toDispose.InsertLast(btn);
    }
    private void GameOverScreen()
    {

    }
    private void Tick(object sender, EventArgs e)
    {
      if (this.state == Game.STATE_PLAYING)
      {
        MainClass.stopwatch.Reset().Start();

        int step = this.pacman.GetEmptyDistance(this.map.StoppingPoints);
        this.pacman.Move(step);

        if (this.keyboardHandler.IsPressed)
        {
          int direction = Direction.FromKeyCode(this.keyboardHandler.ActiveKey);
          if (this.pacman.CanTurn(direction, this.map.TurningPoints))
            this.pacman.Turn(direction);
        }

        MainClass.stopwatch.Stop();
        Console.Write("{0}ms\r", MainClass.stopwatch.Milliseconds);
      }
    }
    public void Run()
    {
      this.board.Width = Game.WIDTH + 100;
      this.board.Height = Game.HEIGHT + 100;

      this.gapVertical = (this.board.ClientRectangle.Height - Game.HEIGHT) / 2;
      this.gapHorizontal = (this.board.ClientRectangle.Width - Game.WIDTH) / 2;

      Console.WriteLine(" width: {0}px | {1}px gap", this.board.ClientRectangle.Width, this.gapHorizontal);
      Console.WriteLine("height: {0}px | {1}px gap", this.board.ClientRectangle.Height, this.gapVertical);

      this.WelcomeScreen();
      Application.Run(this.board);
    }
    public Game()
    {
      this.toDispose = new LinkedList<Control>();

      this.board = new Form();
      this.board.BackColor = Color.FromArgb(10, 5, 2);

      this.keyboardHandler = new KeyboardHandler();
      this.keyboardHandler.Spawn(this.board);
      this.keyboardHandler.Focus();

      this.timer = new Timer();
      timer.Interval = 20;
      timer.Tick += new System.EventHandler(this.Tick);
      timer.Enabled = false;
    }
  }
}
