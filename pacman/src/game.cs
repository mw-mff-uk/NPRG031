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
    public const int AVATAR_SIZE = 36;
    private const int N_STATES = 3;
    private const int STATE_BEFORE_START = 0;
    private const int STATE_PLAYING = 1;
    private const int STATE_GAME_OVER = 2;
    private int state = Game.STATE_BEFORE_START;
    private Form board;
    private LinkedList<Control> toDispose;
    private Settings settings;
    private Pacman pacman;
    private Monster[] monsters;
    private Map map;
    private ScoreBoard scoreBoard;
    private LivesTracker livesTracker;
    private FpsTracker fpsTracker;
    private Timer timer;
    private KeyboardHandler keyboardHandler;
    private GameClock clock;
    private int gapVertical;
    private int gapHorizontal;
    private void InitScoreBoard()
    {
      this.scoreBoard = new ScoreBoard(this.gapHorizontal, 5);

      this.scoreBoard.Spawn(this.board);
      this.toDispose.InsertLast(this.scoreBoard);
    }
    private void InitLivesTracker()
    {
      this.livesTracker = new LivesTracker(this.gapHorizontal, Game.HEIGHT + this.gapVertical + 5);

      this.livesTracker.Spawn(this.board);
      this.toDispose.InsertLast(this.livesTracker);
    }
    private void InitFpsTracker()
    {
      this.fpsTracker = new FpsTracker(this.gapHorizontal + 675, Game.HEIGHT + this.gapVertical + 5);

      this.fpsTracker.Spawn(this.board);
      this.toDispose.InsertLast(this.fpsTracker);
    }
    private void InitPacman()
    {
      this.pacman = new Pacman(
        this.map.PacmanInitialLeft,
        this.map.PacmanInitialTop,
        this.map.PacmanInitialRow,
        this.map.PacmanInitialCol,
        this.map.PacmanInitialDirection,
        this.settings.PlayerSpeed
      );

      this.pacman.Spawn(this.map);
      this.toDispose.InsertLast(this.pacman);
    }
    private void InitMonsters()
    {
      this.monsters = new Monster[this.settings.Monsters];
      for (int i = 0; i < this.settings.Monsters; i++)
      {
        this.monsters[i] = new Monster(
          this.map.MonsterInitialLeft,
          this.map.MonsterInitialTop,
          this.map.MonsterInitialRow,
          this.map.MonsterInitialCol,
          this.map.MonsterInitialDirection,
          this.settings.MonsterSpeed,
          "/home/wiki/School/NPRG031/pacman/src/images/monster-" + (i + 1) + ".png"
        );
      }

      foreach (Monster monster in this.monsters)
      {
        monster.Spawn(this.map);
        this.toDispose.InsertFirst(monster);
      }

      double lastMonster = 0.0;
      for (int i = 0; i < this.monsters.Length; i++)
      {
        double thisMonster = lastMonster + 1000.0 + MainClass.rnd.NextDouble() * 1000.0;
        this.clock.PlanEvent(new ReviveMonsterEvent(this.monsters[i]), thisMonster);
        lastMonster = thisMonster;
      }
    }
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
      this.InitScoreBoard();
      this.InitLivesTracker();
      this.InitFpsTracker();

      this.map = new Map(this.gapHorizontal, this.gapVertical);

      this.InitPacman();
      this.InitMonsters();

      this.map.Spawn(this.board);
      this.toDispose.InsertLast(this.map);

      this.keyboardHandler.Focus();

      this.clock.Start();
      this.timer.Enabled = true;
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
    private void PlayerMovement()
    {
      this.pacman.MaybeMove(this.map.StoppingPoints, this.clock.LastTickDuration);

      if (this.keyboardHandler.IsPressed)
      {
        int direction = Direction.FromKeyCode(this.keyboardHandler.ActiveKey);
        if (this.pacman.CanTurn(direction, this.map.TurningPoints))
          this.pacman.Turn(direction);
      }
    }
    private void MonstersMovement()
    {
      foreach (Monster monster in this.monsters)
      {
        if (!monster.IsAlive)
          continue;

        bool hasMoved = monster.MaybeMove(this.map.StoppingPoints, this.clock.LastTickDuration);

        // If not moved make sure the monster turns
        if (MainClass.rnd.NextDouble() > (hasMoved ? 0.5 : 1.0))
          continue;

        foreach (int direction in Direction.GetShuffledDirections())
        {
          // Make sure the monster does not turn around randomly
          // Allow this only when in corner (ie. if step is smaller the speed, expr. is false)
          if (hasMoved && direction == monster.OppositeDirection)
            continue;

          if (monster.CanTurn(direction, this.map.TurningPoints))
          {
            monster.Turn(direction);
            break;
          }
        }
      }
    }
    private void CheckCollectibles()
    {
      bool collectedSome = false;

      var iterator = this.map.Collectibles.Iterator();
      while (!iterator.Done)
      {
        Collectible collectible = iterator.Next().Value;

        if (!collectible.Collected && collectible.HasCollision(pacman.BBox))
        {
          collectedSome = true;
          collectible.Collect();

          if (collectible.IsCherry)
          {
            this.pacman.StartFrenzyMode();

            this.clock.PlanEvent(
              new StopFrenzyModeEvent(this.pacman),
              this.settings.FrenzyModeDuration * (MainClass.rnd.NextDouble() + 0.5)
            );
          }
          else if (collectible.IsHeart)
          {
            this.livesTracker.AddLive();
          }
          else
          {
            this.scoreBoard.AddPoint();
          }

        }
      }

      if (collectedSome && this.map.CollectedAll)
      {
        this.map.ResetCollectibles();
        this.map.SpawnCollectibles();
      }
    }
    private void CheckMonsterCollisions()
    {
      if (!this.pacman.IsInvincible)
      {
        foreach (Monster monster in this.monsters)
        {
          if (Box.HasCollision(this.pacman.BBox, monster.BBox))
          {
            monster.Kill();

            this.livesTracker.RemoveLive();
            this.pacman.StartInvincibility();

            this.clock.PlanEvent(
              new StopInvincibilityEvent(this.pacman),
              this.settings.InvincibilityDuration
            );

            this.clock.PlanEvent(
              new ReviveMonsterEvent(monster),
              this.settings.InvincibilityDuration * 2 * MainClass.rnd.NextDouble()
            );
          }
        }
      }
    }
    private void Tick(object sender, EventArgs e)
    {
      if (this.state == Game.STATE_PLAYING)
      {
        this.clock.Tick();

        this.clock.ExecuteEvents();

        this.PlayerMovement();
        this.MonstersMovement();

        this.CheckCollectibles();

        this.CheckMonsterCollisions();

        this.fpsTracker.Update(this.clock.Elapsed);

        if (this.livesTracker.GameOver)
          this.NextState();
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
      this.settings = new Settings();

      this.toDispose = new LinkedList<Control>();

      this.board = new Form();
      this.board.BackColor = Color.FromArgb(10, 5, 2);

      this.keyboardHandler = new KeyboardHandler();
      this.keyboardHandler.Spawn(this.board);
      this.keyboardHandler.Focus();

      this.timer = new Timer();
      this.timer.Interval = this.settings.TickPeriod;
      this.timer.Tick += this.Tick;
      this.timer.Enabled = false;

      this.clock = new GameClock();
    }
  }
}
