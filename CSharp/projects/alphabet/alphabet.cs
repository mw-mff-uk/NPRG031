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
      public bool NewLine { get => this._value == '\n'; }
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
    public string ReadLine()
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

      while (!(ch.NewLine || ch.EOF))
      {
        sb.Append((char)ch.Value);
        ch = this.GetChar();
      }

      if (ch.EOF)
        this._EOF = true;
      else
        this.ReturnChar(ch);

      return sb.ToString();
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
    public string ReadRest()
    {
      Char ch;
      StringBuilder sb = new StringBuilder();

      while (true)
      {
        ch = this.GetChar();

        if (ch.EOF)
          break;

        if (!ch.IsEmpty)
          sb.Append((char)ch.Value);
      }

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
  class LinkedListItem<T>
  {
    private T value;
    public T Value { get => this.value; }
    public LinkedListItem<T> next;
    public LinkedListItem(T value, LinkedListItem<T> next = null)
    {
      this.value = value;
      this.next = next;
    }
  }
  class LinkedList<T>
  {
    private LinkedListItem<T> first;
    private LinkedListItem<T> last;
    private int length = 0;
    public int Length { get => this.length; }
    public bool Empty { get => this.length == 0; }
    public LinkedList<T> Append(T value)
    {
      LinkedListItem<T> item = new LinkedListItem<T>(value);

      if (this.first == null)
        this.first = item;

      if (this.last != null)
        this.last.next = item;

      this.last = item;

      this.length++;
      return this;
    }
    public LinkedList<T> Preppend(T value)
    {
      this.first = new LinkedListItem<T>(value, this.first);

      if (this.last == null)
        this.last = this.first;

      this.length++;
      return this;
    }
    public T ExtractFirst()
    {
      if (this.Empty)
        throw new Exception("The linked list is empty");

      T value = this.first.Value;
      this.first = this.first.next;

      this.length--;
      return value;
    }
    public T ExtractLast()
    {
      if (this.Empty)
        throw new Exception("The linked list is empty");

      T value = this.first.Value;

      if (this.Length == 1)
        this.first = this.last = null;
      else
        this.first = this.first.next;

      this.length--;
      return value;
    }
    public LinkedListIterator<T> Iterator() => new LinkedListIterator<T>(this.first);
    public LinkedList(params T[] args)
    {
      for (int i = 0; i < args.Length; i++)
        this.Append(args[i]);
    }
  }
  class LinkedListIterator<T>
  {
    private LinkedListItem<T> cursor;
    private int iterations;
    public int Iterations { get => this.iterations; }
    public bool Done { get => this.cursor == null; }
    public T Next()
    {
      LinkedListItem<T> temp = this.cursor;
      this.cursor = this.cursor.next;

      this.iterations++;
      return temp.Value;
    }
    public LinkedListIterator(LinkedListItem<T> first)
    {
      this.cursor = first;
      this.iterations = 0;
    }
  }
  class Stopwatch
  {
    static long MILLISECONDS_IN_TICK = 10000;
    static long SECONDS_IN_TICK = Stopwatch.MILLISECONDS_IN_TICK * 1000;
    static long MINUTES_IN_TICK = Stopwatch.SECONDS_IN_TICK * 60;
    static long HOURS_IN_TICK = Stopwatch.MINUTES_IN_TICK * 60;
    static long DAYS_IN_TICK = Stopwatch.HOURS_IN_TICK * 24;
    private bool running;
    public bool Running { get => this.running; }
    private long ticks;
    public long Ticks { get => this.ticks + (this.running ? (DateTime.Now - this.start).Ticks : 0); }
    public double Milliseconds { get => (double)this.Ticks / (double)Stopwatch.MILLISECONDS_IN_TICK; }
    public double Seconds { get => (double)this.Ticks / (double)Stopwatch.SECONDS_IN_TICK; }
    public double Minutes { get => (double)this.Ticks / (double)Stopwatch.MINUTES_IN_TICK; }
    public double Hours { get => (double)this.Ticks / (double)Stopwatch.HOURS_IN_TICK; }
    public double Days { get => (double)this.Ticks / (double)Stopwatch.DAYS_IN_TICK; }

    private DateTime start;
    public Stopwatch Start()
    {
      if (!this.running)
      {
        this.running = true;
        this.start = DateTime.Now;
      }

      return this;
    }
    public Stopwatch Stop()
    {
      if (this.running)
      {
        this.running = false;
        this.ticks += (DateTime.Now - this.start).Ticks;
      }

      return this;
    }
    public Stopwatch Reset()
    {
      this.running = false;
      this.ticks = 0;

      return this;
    }
    public Stopwatch()
    {
      this.ticks = 0;
      this.running = false;
    }
  }
  class Keyboard
  {
    private int minChar;
    private int maxChar;
    private int cols;
    private int rows;
    private int size;
    public int Size { get => this.size; }
    private LinkedList<int>[] alphabet;
    public int ManhattanDistance(int a, int b) => Math.Abs(
      // |ax - bx| + |ay - by|
      (a % this.cols) - (b % this.cols)) + Math.Abs((a / this.cols) - (b / this.cols)
    );
    public bool HasLetter(char letter)
    {
      // Console.WriteLine("{0} ({1})", letter, (int)letter);
      int index = (int)letter - this.minChar;

      if (index < 0 || index >= this.alphabet.Length)
        return false;

      return this.alphabet[index].Length > 0;
    }
    public LinkedList<int> GetLetter(char letter) => this.alphabet[(int)letter - this.minChar];
    public Keyboard(int cols, int rows, string content)
    {
      this.cols = cols;
      this.rows = rows;

      this.size = content.Length;

      // Get min and max char value
      this.maxChar = 0;
      this.minChar = -1;
      for (int i = 0; i < this.size; i++)
      {
        int ch = (int)content[i];

        if (ch > this.maxChar)
          this.maxChar = ch;

        if (ch < this.minChar || this.minChar == -1)
          this.minChar = ch;
      }

      // Initiate the alphabet
      this.alphabet = new LinkedList<int>[this.maxChar - this.minChar + 1];
      for (int i = 0; i < this.alphabet.Length; i++)
        this.alphabet[i] = new LinkedList<int>();

      // Fill the alphabet
      for (int i = 0; i < this.size; i++)
        this.alphabet[(int)content[i] - this.minChar].Append(i);

      // Print the grid and alphabet
      // A.Print2D(this.grid);
      // for (int i = 0; i < this.alphabet.Length; i++)
      //   Console.WriteLine("{0} --> {1}", (char)(i + this.minChar), this.alphabet[i].Length);
    }
  }
  class Path
  {
    public int Pos;
    public int Moves;
    public int Next;
    public Path Clone() => new Path(this.Pos, this.Moves, this.Next);
    public Path(int pos, int moves, int next)
    {
      this.Pos = pos;
      this.Moves = moves;
      this.Next = next;
    }
    public Path()
    {
      this.Pos = 0;
      this.Moves = 0;
      this.Next = 0;
    }
  }
  class MainClass
  {
    static int Main(string[] args)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start().Stop().Reset().Start();

      InputReader reader = new InputReader();

      // Create the keyboard
      Keyboard keyboard = new Keyboard(
        reader.ReadInt(),    // cols
        reader.ReadInt(),    // rows
        reader.ReadLine()    // content
      );

      // Parse the input sequence
      int letters = 0;
      char[] sequence = reader.ReadRest().ToCharArray();
      for (int i = 0; i < sequence.Length; i++)
        if (keyboard.HasLetter(sequence[i]))
          sequence[letters++] = sequence[i];

      // Initiate the optimal matrix [position of letter, number of moves]
      int[,] optimal = new int[keyboard.Size, letters];
      for (int pos = 0; pos < keyboard.Size; pos++)
        for (int moves = 0; moves < letters; moves++)
          optimal[pos, moves] = -1;

      // The best path
      int res = -1;

      // Start exploring possible paths
      LinkedList<Path> explorer = new LinkedList<Path>(new Path());
      while (!explorer.Empty)
      {
        Path path = explorer.ExtractFirst();

        // Next letter
        char letter = sequence[path.Next];
        // Iterator for positions of this letter on the keyboard
        LinkedListIterator<int> iterator = keyboard.GetLetter(letter).Iterator();
        while (!iterator.Done)
        {
          int letterPosition = iterator.Next();

          Path newPath = new Path(
            letterPosition,
            path.Moves + keyboard.ManhattanDistance(path.Pos, letterPosition),
            path.Next + 1
          );

          // Console.WriteLine(
          //   "Path [moves: {0}; next: {1}] | {2} --> {3} @ {4} in {5} moves",
          //   newPath.Moves,
          //   newPath.Next,
          //   newPath.Pos,
          //   letter,
          //   letterPosition,
          //   keyboard.ManhattanDistance(newPath.Pos, letterPosition)
          // );

          if (newPath.Next == letters)
          {
            if (newPath.Moves < res || res == -1)
              res = newPath.Moves;
          }
          else
          {
            int opt = optimal[newPath.Pos, newPath.Next];

            if (opt == -1 || opt > newPath.Moves)
            {
              optimal[newPath.Pos, newPath.Next] = newPath.Moves;
              explorer.Append(newPath);
            }
            // else
            // {
            //   Console.WriteLine("Forgetting about path at {0} ({1})", newPath.Pos, newPath.Next);
            // }
          }

        }
      }

      Console.WriteLine(res + letters);

      Console.WriteLine("{0}ms", stopwatch.Stop().Milliseconds);

      return 0;
    }
  }
}
