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
    public const int AVATAR_SIZE = 38;
    private const int N_STATES = 3;
    private const int STATE_BEFORE_START = 0;
    private const int STATE_PLAYING = 1;
    private const int STATE_GAME_OVER = 2;
    private int state = Game.STATE_BEFORE_START;
    private Form board;
    private LinkedList<Control> toDispose;
    private Pacman pacman;
    private PictureBox background;
    private Timer timer;
    private KeyboardHandler keyboardHandler;
    private int gapVertical;
    private int gapHorizontal;
    private DirectedPoint[] stoppingPoints;
    private DirectedPoint[] turningPoints;
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
      this.pacman = new Pacman(
        20 + this.gapHorizontal,
        392 - Game.AVATAR_SIZE / 2 + this.gapVertical,
        Direction.RIGHT
      );
      this.pacman.Spawn(this.board);
      this.toDispose.InsertLast(this.pacman);

      this.background = new PictureBox();
      this.background.Width = Game.WIDTH;
      this.background.Height = Game.HEIGHT;
      this.background.Top = this.gapVertical;
      this.background.Left = this.gapHorizontal;
      this.background.Parent = this.board;
      this.background.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/background.jpg");
      this.background.SizeMode = PictureBoxSizeMode.StretchImage;
      this.toDispose.InsertLast(this.background);

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

        if (this.pacman.CanMove(this.stoppingPoints))
          this.pacman.Move();

        if (this.keyboardHandler.IsPressed)
        {
          int direction = Direction.FromKeyCode(this.keyboardHandler.ActiveKey);
          if (this.pacman.CanTurn(direction, this.stoppingPoints))
            this.pacman.Turn(direction);
        }

        MainClass.stopwatch.Stop();
        Console.Write("{0}ms\r", MainClass.stopwatch.Milliseconds);
      }
    }
    private void ConfigureDirectedPoints()
    {
      var dpf = new DirectedPointFactory(this.gapHorizontal, this.gapVertical);

      int r = Direction.RIGHT;
      int l = Direction.LEFT;
      int u = Direction.UP;
      int d = Direction.DOWN;
      int all = r | l | u | d;

      this.stoppingPoints = new DirectedPoint[] {
        dpf.Point( 0, 0, l | u ), // 1
        dpf.Point( 0, 2, u ),     // 2 
        dpf.Point( 0, 4, r | u ), // 3
        dpf.Point( 0, 5, l | u ), // 4
        dpf.Point( 0, 7, u ),     // 5
        dpf.Point( 0, 9, u | r ), // 6
        dpf.Point( 1, 0, l ),     // 7
        dpf.Point( 1, 3, u ),     // 8
        dpf.Point( 1, 4, d ),     // 9
        dpf.Point( 1, 5, d ),     // 10
        dpf.Point( 1, 6, u ),     // 11
        dpf.Point( 1, 9, r ),     // 12
        dpf.Point( 2, 0, d | l ), // 13
        dpf.Point( 2, 2, r ),     // 14
        dpf.Point( 2, 3, d | l ), // 15
        dpf.Point( 2, 4, r | u ), // 16
        dpf.Point( 2, 5, l | u ), // 17
        dpf.Point( 2, 6, d | r ), // 18
        dpf.Point( 2, 7, l ),     // 19
        dpf.Point( 2, 9, r | d ), // 20
        dpf.Point( 3, 3, l | u ), // 21
        dpf.Point( 3, 4, d ),     // 22
        dpf.Point( 3, 5, d ),     // 23
        dpf.Point( 3, 6, r | u ), // 24
        dpf.Point( 4, 2, l ),     // 25
        dpf.Point( 4, 3, r ),     // 26
        dpf.Point( 4, 6, l ),     // 27
        dpf.Point( 4, 7, r ),     // 28
        dpf.Point( 5, 3, l ),     // 29
        dpf.Point( 5, 6, r ),     // 30
        dpf.Point( 6, 0, l | u ), // 31
        dpf.Point( 6, 3, d ),     // 32
        dpf.Point( 6, 4, r | u ), // 33
        dpf.Point( 6, 5, l | u ), // 34
        dpf.Point( 6, 6, d ),     // 35
        dpf.Point( 6, 9, r | u ), // 36
        dpf.Point( 7, 0, d | l ), // 37
        dpf.Point( 7, 1, r | u ), // 38
        dpf.Point( 7, 2, l ),     // 39
        dpf.Point( 7, 4, d ),     // 40
        dpf.Point( 7, 5, d ),     // 41
        dpf.Point( 7, 7, r ),     // 42
        dpf.Point( 7, 8, l | u ), // 43
        dpf.Point( 7, 9, r | d ), // 44
        dpf.Point( 8, 0, l | u ), // 45
        dpf.Point( 8, 1, d ),     // 46
        dpf.Point( 8, 2, d | r ), // 47
        dpf.Point( 8, 3, d | l ), // 48
        dpf.Point( 8, 4, r | u ), // 49
        dpf.Point( 8, 5, u | l ), // 50
        dpf.Point( 8, 6, d | r ), // 51
        dpf.Point( 8, 7, d | l ), // 52
        dpf.Point( 8, 8, d ),     // 53
        dpf.Point( 8, 9, r | u ), // 54
        dpf.Point( 9, 0, l | d ), // 55
        dpf.Point( 9, 9, r | d ), // 56
        dpf.Point( 7, 3, u ),     // 57
        dpf.Point( 7, 6, u )      // 58
      };
    }
    public void Run()
    {
      this.board.Width = Game.WIDTH + 100;
      this.board.Height = Game.HEIGHT + 100;

      this.gapVertical = (this.board.ClientRectangle.Height - Game.HEIGHT) / 2;
      this.gapHorizontal = (this.board.ClientRectangle.Width - Game.WIDTH) / 2;

      this.ConfigureDirectedPoints();

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
