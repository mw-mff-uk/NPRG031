using System;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class InputReader
  {
    class Char
    {
      private int _value;
      public int Value { get => this._value; }
      public bool IsEmpty { get => (this._value == ' ' || this._value == '\n' || this._value == '\t' || this._value == '\r' || this._value == '\v'); }
      public bool IsNum { get => (this._value >= '0' && this._value <= '9'); }
      public bool IsAlph { get => ((this._value >= 'a' && this._value <= 'z') || (this._value >= 'A' && this._value <= 'A')); }
      public bool EOF { get => this._value == -1; }
      public static bool operator ==(Char ch1, Char ch2) => ch1.Value == ch2.Value;
      public static bool operator ==(Char ch1, int ch2) => ch1.Value == ch2;
      public static bool operator !=(Char ch1, Char ch2) => ch1.Value != ch2.Value;
      public static bool operator !=(Char ch1, int ch2) => ch1.Value != ch2;
      public override bool Equals(object obj) => (((Char)obj).Value == this.Value);
      public override int GetHashCode() => this.Value;
      public Char(int value)
      {
        this._value = value;
      }
    }
    private Char[] _buffer;
    private int _cursor;
    private bool _EOF;
    public bool EOF { get { return this._EOF; } }
    private Char GetChar()
    {
      if (this._cursor > 0)
        return this._buffer[--this._cursor];

      return new Char(Console.Read());
    }
    private void ReturnChar(Char ch)
    {
      this._buffer[this._cursor++] = ch;
    }
    public string ReadWord()
    {
      Char ch;
      StringBuilder sb = new StringBuilder();

      // Skip empty symbols
      while ((ch = this.GetChar()).IsEmpty) ;

      // Check for EOF
      if (ch.EOF)
      {
        this._EOF = true;
        return "";
      }

      // Build the string
      sb.Append((char)ch.Value);
      while (ch.IsAlph || ch.IsNum)
      {
        sb.Append((char)ch.Value);
        ch = this.GetChar();
      }

      // Check for EOF
      if (ch.EOF)
        this._EOF = true;
      else
        this.ReturnChar(ch);

      return sb.ToString();
    }
    public double ReadFloat()
    {
      Char ch;
      double x = 0.0;
      int div = 1;
      int sign = 1;

      // Skip empty symbols
      while ((ch = this.GetChar()).IsEmpty) ;

      // Check for EOF
      if (ch.EOF)
      {
        this._EOF = true;
        return 0.0;
      }

      // Check for sign
      if (ch == '+' || ch == '-')
      {
        if (ch == '-')
          sign = -1;

        ch = this.GetChar();
      }

      // Keep reading numbers
      while (ch.IsNum)
      {
        x = x * 10 + ch.Value - '0';
        ch = this.GetChar();
      }

      // Check for decimal symbol (dot, comma)
      if (ch == '.' || ch == ',')
      {
        while ((ch = this.GetChar()).IsNum)
        {
          x = x * 10 + ch.Value - '0';
          div *= 10;
        }
      }

      // Check for EOF
      if (ch.EOF)
        this._EOF = true;
      else
        this.ReturnChar(ch);

      return sign * x / div;
    }
    public int ReadInt()
    {
      return (int)this.ReadFloat();
    }
    public InputReader()
    {
      this._buffer = new Char[100];
      this._cursor = 0;
    }
  }
  class Queue<T>
  {
    class QueueItem<T>
    {
      public T value;
      public QueueItem<T> next;
      public QueueItem(T value, QueueItem<T> next = null)
      {
        this.value = value;
        this.next = next;
      }
    }
    private QueueItem<T> _first;
    private QueueItem<T> _last;
    private int length = 0;
    public int Length { get => this.length; }
    public bool CanDequeue { get => this.length > 0; }
    public Queue<T> Enqueue(T value)
    {
      QueueItem<T> item = new QueueItem<T>(value);

      if (this._first == null)
        this._first = item;

      if (this._last != null)
        this._last.next = item;

      this._last = item;

      this.length++;
      return this;
    }
    public T Dequeue()
    {
      if (!this.CanDequeue)
        throw new Exception("The queue is empty");

      T value = this._first.value;

      if (this.Length == 1)
        this._first = this._last = null;
      else
        this._first = this._first.next;

      this.length--;
      return value;
    }
    public Queue(T[] items = null)
    {
      if (items != null)
        for (int i = 0; i < items.Length; i++)
          this.Enqueue(items[i]);
    }
  }
  class A
  {
    public static int[] Range(int low, int high)
    {
      int[] res = new int[high - low];

      for (int i = low; i < high; i++)
        res[i] = i;

      return res;
    }
    public static int[] Range(int high)
    {
      return A.Range(0, high);
    }
    public static T[] Swap<T>(T[] arr, int i, int j)
    {
      T temp = arr[i];
      arr[i] = arr[j];
      arr[j] = temp;

      return arr;
    }
    public static T[] Shuffle<T>(T[] arr, Random rnd)
    {
      for (int i = 0; i < arr.Length; i++)
        A.Swap(arr, i, rnd.Next(0, arr.Length));

      return arr;
    }
    public static T[] Rep<T>(T[] arr, int times)
    {
      T[] res = new T[arr.Length * times];

      for (int i = 0; i < times; i++)
        for (int j = 0; j < arr.Length; j++)
          res[j + i * arr.Length] = arr[j];

      return res;
    }
    public static T[] Print<T>(T[] arr, string delimiter = " ", string after = "\n")
    {
      for (int i = 0; i < arr.Length; i++)
        Console.Write(arr[i].ToString() + (i == arr.Length - 1 ? "" : delimiter));

      Console.Write(after);

      return arr;
    }
  }
  class Piece : Button
  {
    private int value;
    public int Value { get => this.value; }
    private bool discovered = false;
    public bool Discovered { get => this.discovered; }
    private bool revealed = false;
    public bool Revealed { get => this.revealed; }
    public bool Visible { get => this.discovered || this.revealed; }
    public Piece Reveal()
    {
      if (this.revealed)
        throw new Exception("This piece is already revealed");

      this.revealed = true;
      this.Text = this.value.ToString();

      return this;
    }
    public Piece Hide()
    {
      if (!this.revealed)
        throw new Exception("This piece is already hidden");

      this.revealed = false;
      this.Text = "Pexeso";

      return this;
    }
    public Piece Discover()
    {
      if (this.discovered)
        throw new Exception("This piece is already discovered");

      this.discovered = false;
      this.Text = this.value.ToString();

      return this;
    }
    public Piece(int value)
    {
      this.value = value;
      this.Text = "Pexeso";
    }
  }
  class ScoreBoard : Label
  {
    private int score;
    public int Score { get => this.score; }
    private int moves;
    public int Moves { get => this.moves; }
    private ScoreBoard UpdateText()
    {
      this.Text = String.Format("Score: {0} | Moves: {1}", this.score, this.moves);
      return this;
    }
    public ScoreBoard SetScore(int score)
    {
      this.score = score;
      return this.UpdateText();
    }
    public ScoreBoard IncScore(int inc)
    {
      return this.SetScore(this.score + inc);
    }
    public ScoreBoard SetMoves(int moves)
    {
      this.moves = moves;
      return this.UpdateText();
    }
    public ScoreBoard IncMoves(int inc)
    {
      return this.SetMoves(this.moves + inc);
    }
    public ScoreBoard Reset()
    {
      this.SetScore(0);
      this.SetMoves(0);

      return this;
    }
    public ScoreBoard Spawn(Form parent)
    {
      this.Parent = parent;
      this.Left = 0;
      this.Top = this.Parent.ClientRectangle.Height - 30;
      this.Height = 15;
      this.Width = this.Parent.ClientRectangle.Width;

      return this;
    }
  }
  class Game
  {
    private Queue<Control> toDispose;
    private ScoreBoard scoreBoard;
    private Form board;
    private Piece[,] pieces;
    private int size;
    private int nRevealed;
    private Piece[] revealed;
    private int nPieces { get => this.size * this.size; }
    private void OnScreenChange()
    {
      while (this.toDispose.CanDequeue)
        this.toDispose.Dequeue().Dispose();
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
      btn.Click += this.GameScreen;

      // Fill toDispose Queue
      this.toDispose.Enqueue(btn);
    }
    private void GameOverScreen()
    {
      this.OnScreenChange();

      Label lab = new Label();
      lab.Text = String.Format("Game finished in {0} moves", this.scoreBoard.Moves);
      lab.Left = this.board.ClientRectangle.Width / 2 - 50;
      lab.Top = this.board.ClientRectangle.Height / 2 - 60;
      lab.Width = 100;
      lab.Height = 40;
      lab.Parent = this.board;

      Button btn = new Button();
      btn.Text = "New Game";
      btn.Left = this.board.ClientRectangle.Width / 2 - 50;
      btn.Top = this.board.ClientRectangle.Height / 2 - 20;
      btn.Width = 100;
      btn.Height = 40;
      btn.Parent = this.board;
      btn.Click += this.GameScreen;

      // Fill toDispose Queue
      this.toDispose.Enqueue(lab);
      this.toDispose.Enqueue(btn);
    }
    private void GameScreen(object sender, EventArgs e)
    {
      this.OnScreenChange();

      // Reset and spawn the score board
      this.scoreBoard = new ScoreBoard();
      this.scoreBoard.Reset();
      this.scoreBoard.Spawn(this.board);

      // Reset revealed pieces
      this.revealed = new Piece[2];
      this.nRevealed = 0;

      // Reset and shuffle the pieces
      this.pieces = new Piece[this.size, this.size];
      int[] values = A.Shuffle(A.Rep(A.Range(this.nPieces / 2), 2), MainClass.rnd);

      for (int i = 0; i < this.size; i++)
        for (int j = 0; j < this.size; j++)
          this.pieces[i, j] = new Piece(values[i * this.size + j] + 1);

      // Spawn the pieces
      int sx = this.board.ClientRectangle.Width / this.size;
      int sy = (this.board.ClientRectangle.Height - 30) / this.size;

      for (int i = 0; i < this.size; i++)
      {
        for (int j = 0; j < this.size; j++)
        {
          Piece piece = this.pieces[i, j];

          int x = i * sx;
          int y = j * sy;

          piece.Left = x;
          piece.Top = y;
          piece.Width = sx - 2;
          piece.Height = sy - 2;
          piece.Parent = this.board;
          piece.Click += this.onclick;
        }
      }

      // Fill toDispose Queue
      this.toDispose.Enqueue(this.scoreBoard);
      for (int i = 0; i < this.size; i++)
        for (int j = 0; j < this.size; j++)
          this.toDispose.Enqueue(this.pieces[i, j]);
    }
    private void onclick(object sender, EventArgs e)
    {
      Piece piece = (Piece)sender;

      if (piece.Visible)
        return;

      // Hide pieces when 2 revealed and not matching
      if (this.nRevealed == 2)
      {
        this.revealed[0].Hide();
        this.revealed[1].Hide();

        this.nRevealed = 0;
      }

      // Reveal the clicked piece
      piece.Reveal();
      this.revealed[this.nRevealed++] = piece;
      this.scoreBoard.IncMoves(1);

      // If 2 revealed, compare the values
      if (this.nRevealed == 2)
      {
        if (this.revealed[0].Value == this.revealed[1].Value)
        {
          this.revealed[0].Discover();
          this.revealed[1].Discover();
          this.scoreBoard.IncScore(2);
          this.nRevealed = 0;

          if (this.scoreBoard.Score == this.nPieces)
            this.GameOverScreen();
        }
      }
    }
    public void Run()
    {
      this.WelcomeScreen();
      Application.Run(this.board);
    }
    public Game(int size)
    {
      this.size = size;
      this.board = new Form();
      this.toDispose = new Queue<Control>();
    }
  }
  class MainClass
  {
    public static Random rnd = new Random();
    [STAThread]
    static void Main()
    {
      InputReader inputReader = new InputReader();
      Game game = new Game(6);
      game.Run();
    }
  }
}
