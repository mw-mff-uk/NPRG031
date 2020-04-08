using System;
using System.Text;
using System.Collections;

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
      public bool IsAlph { get => ((this._value >= 'a' && this._value <= 'z') || (this._value >= 'A' && this._value <= 'Z')); }
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
      {
        Char ch = this._buffer[--this._cursor];
        return ch;
      }

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
    public static T[,] Print2D<T>(T[,] matrix, string colDelimiter = " ", string rowDelimiter = "\n", string after = "")
    {
      int rows = matrix.GetLength(0);
      int cols = matrix.GetLength(1);

      Console.WriteLine("rows: {0}", rows);
      Console.WriteLine("cols: {0}", cols);

      for (int row = 0; row < rows; row++)
        for (int col = 0; col < cols; col++)
          Console.Write(matrix[row, col].ToString() + (col == cols - 1 ? rowDelimiter : colDelimiter));

      Console.Write(after);

      return matrix;
    }
  }
  class Queue<T>
  {
    class QueueItem<TT>
    {
      private TT value;
      public TT Value { get => this.value; }
      public QueueItem<TT> next;
      public QueueItem(TT value, QueueItem<TT> next = null)
      {
        this.value = value;
        this.next = next;
      }
    }
    private QueueItem<T> first;
    private QueueItem<T> last;
    private int length = 0;
    public int Length { get => this.length; }
    public bool CanDequeue { get => this.length > 0; }
    public Queue<T> Enqueue(T value)
    {
      QueueItem<T> item = new QueueItem<T>(value);

      if (this.first == null)
        this.first = item;

      if (this.last != null)
        this.last.next = item;

      this.last = item;

      this.length++;
      return this;
    }
    public T Dequeue()
    {
      if (!this.CanDequeue)
        throw new Exception("The queue is empty");

      T value = this.first.Value;

      if (this.Length == 1)
        this.first = this.last = null;
      else
        this.first = this.first.next;

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
  class State
  {
    private int maxRow;
    private int maxCol;
    public int Moves;
    public int Discovered;
    public char Target;
    public int Row;
    public int Col;
    public bool CanMoveUp { get => this.Row > 0; }
    public bool CanMoveDown { get => this.Row < this.maxRow; }
    public bool CanMoveLeft { get => this.Col > 0; }
    public bool CanMoveRight { get => this.Col < this.maxCol; }
    public State MoveUp(int steps = 1)
    {
      this.Row -= steps;
      this.Moves += steps;
      return this;
    }
    public State MoveDown(int steps = 1)
    {
      this.Row += steps;
      this.Moves += steps;
      return this;
    }
    public State MoveLeft(int steps = 1)
    {
      this.Col -= steps;
      this.Moves += steps;
      return this;
    }
    public State MoveRight(int steps = 1)
    {
      this.Col += steps;
      this.Moves += steps;
      return this;
    }
    public State Clone() => new State(
      this.maxRow,
      this.maxCol,
      this.Moves,
      this.Discovered,
      this.Target,
      this.Row,
      this.Col
    );
    public State(int maxRow, int maxCol, int moves, int discovered, char target, int row, int col)
    {
      this.maxRow = maxRow;
      this.maxCol = maxCol;

      this.Discovered = discovered;
      this.Moves = moves;
      this.Target = target;
      this.Row = row;
      this.Col = col;
    }
  }
  class MainClass
  {
    static void Main()
    {
      InputReader inputReader = new InputReader();

      int cols = inputReader.ReadInt();
      int rows = inputReader.ReadInt();

      // Build the grid
      char[,] grid = new char[rows, cols];
      string gridContent = inputReader.ReadWord();
      for (int i = 0; i < rows; i++)
        for (int j = 0; j < cols; j++)
          grid[i, j] = gridContent[i * cols + j];

      // A.Print2D(grid);

      // Build and fill the alphabet
      bool[] alphabet = new bool[(int)'z' + 1];

      for (int i = (int)'A'; i <= (int)'z'; i++)
        alphabet[i] = false;

      for (int i = 0; i < gridContent.Length; i++)
        alphabet[(int)gridContent[i]] = true;

      // for (int i = (int)'A'; i <= (int)'z'; i++)
      //   Console.WriteLine("{0} --> {1}", (char)i, alphabet[i]);

      // Read and split the target sequence
      // Make sure no unknown letters are in the sequence
      int nLetters = 0;
      char[] inputSequence = inputReader.ReadWord().ToCharArray();
      char[] sequence = new char[inputSequence.Length];
      for (int i = 0; i < inputSequence.Length; i++)
        if (alphabet[inputSequence[i]])
          sequence[nLetters++] = inputSequence[i];

      // Create the queue and start BFS
      Queue<State> queue = new Queue<State>();
      queue.Enqueue(new State(rows - 1, cols - 1, 0, 0, sequence[0], 0, 0));
      while (queue.CanDequeue)
      {
        State state = queue.Dequeue();

        // Cursor is on the target letter
        if (state.Target == grid[state.Row, state.Col])
        {
          state.Discovered++;
          state.Moves++;

          // All letters discovered
          if (state.Discovered == nLetters)
          {
            Console.WriteLine(state.Moves);
            Environment.Exit(0);
          }

          // Move to the next letter
          state.Target = sequence[state.Discovered];
        }

        // Move UP
        if (state.CanMoveUp)
          queue.Enqueue(state.Clone().MoveUp());

        // Move DOWN
        if (state.CanMoveDown)
          queue.Enqueue(state.Clone().MoveDown());

        // Move RIGHT
        if (state.CanMoveRight)
          queue.Enqueue(state.Clone().MoveRight());

        // Move LEFT
        if (state.CanMoveLeft)
          queue.Enqueue(state.Clone().MoveLeft());
      }
    }
  }
}
