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
    public const int AVATAR_SIZE = 32;
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
    private TextBox keyboardHandler;
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
      this.pacman = new Pacman();
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
        if (this.pacman.CanMove(this.stoppingPoints))
          this.pacman.Move();
      }
    }
    private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (this.state == Game.STATE_PLAYING)
      {
        int direction = Direction.FromKeyCode(e.KeyCode);

        if (this.pacman.CanTurn(direction, this.stoppingPoints))
          this.pacman.Turn(direction);
      }
    }
    private void ConfigureDirectedPoints()
    {
      int gh = this.gapHorizontal = (this.board.ClientRectangle.Height - Game.HEIGHT) / 2;
      int gv = this.gapVertical = (this.board.ClientRectangle.Width - Game.WIDTH) / 2;

      var dpf = new DirectedPointFactory(gh, gv);

      int r = Direction.RIGHT;
      int l = Direction.LEFT;
      int u = Direction.UP;
      int d = Direction.DOWN;
      int all = r | l | u | d;

      this.stoppingPoints = new DirectedPoint[] {
        dpf.Point( 0, 0, l | u ), // 1
        dpf.Point( 0, 2, u ),     // 2 
        dpf.Point( 0, 4, r | u ), // 3
        dpf.Point( 1, 0, l ),     // 7
        dpf.Point( 1, 3, u ),     // 8
        dpf.Point( 1, 4, d ),     // 9
        dpf.Point( 2, 0, d | l ), // 13
        dpf.Point( 1, 9, r ),     // 12
        dpf.Point( 2, 2, r )      // 14 
      };
    }
    public void Run()
    {
      this.board.Width = Game.WIDTH + 60;
      this.board.Height = Game.HEIGHT + 60;

      this.ConfigureDirectedPoints();

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
      timer.Interval = 20;
      timer.Tick += new System.EventHandler(this.Tick);
      timer.Enabled = false;
    }
  }
}
