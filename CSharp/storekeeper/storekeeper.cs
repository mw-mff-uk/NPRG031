using System;
using System.Text;


namespace MainNamespace
{
  class A
  {
    public static int[] Range(int low, int high)
    {
      int[] res = new int[high - low];

      for (int i = low; i < high; i++)
        res[i - low] = i;

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
        Console.Write(arr[i].ToString() + (i == arr.Length - 1 ? after : delimiter));

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
  class LinkedListItem<T>
  {
    private T value;
    public T Value { get => this.value; }
    public LinkedListItem<T> Next;
    public LinkedListItem<T> Prev;
    public LinkedListItem(T value, LinkedListItem<T> next = null, LinkedListItem<T> prev = null)
    {
      this.value = value;

      this.Next = next;
      this.Prev = prev;
    }
  }
  class LinkedList<T>
  {
    private LinkedListItem<T> first;
    public LinkedListItem<T> First { get => this.first; }
    private LinkedListItem<T> last;
    public LinkedListItem<T> Last { get => this.last; }
    private int length = 0;
    public int Length { get => this.length; }
    public bool Empty { get => this.length == 0; }
    public T RemoveItem(LinkedListItem<T> item)
    {
      if (this.Empty)
        throw new Exception("The linked list is empty");

      T value = item.Value;

      // First item
      if (item == this.first)
      {
        // Console.WriteLine("Extracting first item: {0}", value.ToString());
        this.first = this.first.Next;

        if (this.first != null)
          this.first.Prev = null;
      }

      // Last item
      else if (item == this.last)
      {
        // Console.WriteLine("Extracting last item: {0}", value.ToString());
        this.last = this.last.Prev;

        if (this.last != null)
          this.last.Next = null;
      }

      // Middle item
      else
      {
        // Console.WriteLine("Extracting middle item: {0}", value.ToString());
        item.Prev.Next = item.Next;
        item.Next.Prev = item.Prev;
      }

      // Remove references
      item.Prev = null;
      item.Next = null;

      // Adjust first and last items in special cases
      this.length--;
      if (this.length == 1)
        this.last = this.first;
      else if (this.length == 0)
        this.last = this.first = null;

      return value;
    }
    public T ExtractFirst() => this.RemoveItem(this.first);
    public T ExtractLast() => this.RemoveItem(this.last);
    public LinkedListItem<T> InsertLast(T value)
    {
      LinkedListItem<T> item = new LinkedListItem<T>(value, null, this.last);

      if (this.Empty)
        this.first = item;

      if (this.last != null)
        this.last.Next = item;

      this.last = item;

      this.length++;
      return item;
    }
    public LinkedListItem<T> InsertFirst(T value)
    {
      this.first = new LinkedListItem<T>(value, this.first, null);

      if (this.last == null)
        this.last = this.first;

      this.length++;
      return this.first;
    }
    public LinkedListIterator<T> Iterator() => new LinkedListIterator<T>(this.first);
    public void DetailedPrint()
    {
      var item = this.first;
      while (item != null)
      {
        Console.WriteLine(
          "{0} <-- {1} --> {2}",
          item.Prev == null ? "-" : item.Prev.Value.ToString(),
          item.Value.ToString(),
          item.Next == null ? "-" : item.Next.Value.ToString()
        );
        item = item.Next;
      }
    }
    public override string ToString()
    {
      var sb = new StringBuilder();
      var iterator = this.Iterator();

      sb.Append("{ ");
      while (!iterator.Done)
        sb.Append(iterator.Next().Value.ToString() + (iterator.Done ? "" : ", "));
      sb.Append(" }");

      return sb.ToString();
    }
    public bool ContainsValue(T value)
    {
      var iterator = this.Iterator();
      while (!iterator.Done)
        if (Object.Equals(iterator.Next().Value, value))
          return true;

      return false;
    }
    public LinkedList(params T[] args)
    {
      for (int i = 0; i < args.Length; i++)
        this.InsertLast(args[i]);
    }
  }
  class LinkedListIterator<T>
  {
    private LinkedListItem<T> cursor;
    private LinkedListItem<T> first;
    private int iterations;
    public int Iterations { get => this.iterations; }
    public bool Done { get => this.cursor == null; }
    public LinkedListItem<T> Next()
    {
      LinkedListItem<T> temp = this.cursor;
      this.cursor = this.cursor.Next;

      this.iterations++;
      return temp;
    }
    public LinkedListIterator<T> Reset()
    {
      this.cursor = this.first;
      this.iterations = 0;

      return this;
    }
    public LinkedListIterator(LinkedListItem<T> first)
    {
      this.first = first;
      this.Reset();
    }
  }
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

      ch = this.GetChar();

      // In case of new line in stack
      if (ch.NewLine)
        ch = this.GetChar();

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

        if (!ch.EOF)
          sb.Append((char)ch.Value);
        else
          break;
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
  class WarehouseStateTracker
  {
    private Warehouse warehouse;
    private bool[,] discovered;
    public void RegisterState(WarehouseState state)
    {
      this.discovered[state.Storekeeper, state.Box] = true;
    }
    public bool StateDiscovered(WarehouseState state)
    {
      return this.discovered[state.Storekeeper, state.Box];
    }
    public WarehouseStateTracker(Warehouse warehouse)
    {
      this.warehouse = warehouse;

      this.discovered = new bool[this.warehouse.Size, this.warehouse.Size];

      for (int i = 0; i < this.warehouse.Size; i++)
        for (int j = 0; j < this.warehouse.Size; j++)
          discovered[i, j] = false;
    }
  }
  class WarehouseState
  {
    private Warehouse warehouse;
    public int Storekeeper;
    public int Box;
    public int Moves;
    private int[] translations;
    public LinkedList<WarehouseState> Mutate()
    {
      var mutations = new LinkedList<WarehouseState>();

      this.warehouse.SetState(this);

      foreach (int newPos in this.translations)
        if (this.warehouse.CanStorekeeperMoveTo(newPos))
          mutations.InsertLast(this.warehouse.MoveStorekeeperTo(newPos));

      return mutations;
    }
    public void Print()
    {
      char[] toPrint = new char[this.warehouse.Size];

      for (int i = 0; i < this.warehouse.Size; i++)
        toPrint[i] = this.warehouse.IsEmpty(i) ? Warehouse.EMPTY : Warehouse.WALL;

      toPrint[this.Storekeeper] = Warehouse.STOREKEEPER;
      toPrint[this.Box] = Warehouse.BOX;
      toPrint[this.warehouse.Target] = Warehouse.TARGET;

      int nl = this.warehouse.Width; // New line
      for (int i = 0; i < this.warehouse.Size; i++)
        Console.Write("{0}{1}", toPrint[i], i % nl == (nl - 1) ? "\n" : "");

      Console.WriteLine();
    }
    public WarehouseState(Warehouse warehouse, int storekeeper, int box, int moves)
    {
      this.warehouse = warehouse;
      this.Storekeeper = storekeeper;
      this.Box = box;
      this.Moves = moves;

      this.translations = new int[] {
        this.Storekeeper - this.warehouse.Width,   // Up
        this.Storekeeper + this.warehouse.Width,   // Down
        this.Storekeeper - 1,                      // Left
        this.Storekeeper + 1                       // Right
      };
    }
  }
  class Warehouse
  {
    public const char EMPTY = '.';
    public const char WALL = 'X';
    public const char STOREKEEPER = 'S';
    public const char BOX = 'B';
    public const char TARGET = 'C';
    public int Width = 12;
    public int Height = 12;
    public int Size { get => this.Width * this.Height; }
    public int SizeWithoutBoundaries { get => (this.Width - 2) * (this.Height - 2); }
    private bool[] obstacles;
    private int storekeeperInitial;
    public int StorekeeperInitial { get => this.storekeeperInitial; }
    private int boxInitial;
    public int BoxInitial { get => this.boxInitial; }
    private int target;
    public int Target { get => this.target; }
    private WarehouseState initialState;
    public WarehouseState InitialState { get => this.initialState; }
    private WarehouseState currentState;
    public void SetState(WarehouseState state)
    {
      this.currentState = state;
    }
    public bool IsEmpty(int pos) => !this.obstacles[pos];
    public bool IsObstacle(int pos) => this.obstacles[pos];
    public bool IsBox(int pos) => this.currentState.Box == pos;
    public bool ValidPos(int pos) => pos >= 0 && pos < this.Size;
    public bool CanStorekeeperMoveTo(int pos)
    {
      if (!this.ValidPos(pos) || this.IsObstacle(pos))
        return false;

      if (!this.IsBox(pos))
        return true;

      int diff = pos - this.currentState.Storekeeper;
      int boxPos = pos + diff;
      return this.ValidPos(boxPos) && this.IsEmpty(boxPos);
    }
    public WarehouseState MoveStorekeeperTo(int pos)
    {
      var curr = this.currentState;

      if (!this.IsBox(pos))
        return new WarehouseState(this, pos, curr.Box, curr.Moves + 1);

      int diff = pos - curr.Storekeeper;
      int boxPos = pos + diff;
      return new WarehouseState(this, pos, boxPos, curr.Moves + 1);
    }
    public WarehouseStateTracker Tracker() => new WarehouseStateTracker(this);
    public Warehouse(string map)
    {
      if (map.Length != this.SizeWithoutBoundaries)
        throw new Exception("Map is of invalid Length");

      this.obstacles = new bool[this.Size];

      int cursor = 0;
      for (int pos = 0; pos < this.Size; pos++)
      {
        if (
          pos < this.Width ||                       // Top row
          pos >= (this.Size - this.Width) ||        // Bottom row
          pos % this.Width == 0 ||                  // Left row
          pos % this.Height == (this.Width - 1)     // Right row
        )
        {
          this.obstacles[pos] = true;
        }
        else
        {
          switch (map[cursor++])
          {
            case Warehouse.EMPTY: this.obstacles[pos] = false; break;
            case Warehouse.WALL: this.obstacles[pos] = true; break;
            case Warehouse.STOREKEEPER: this.storekeeperInitial = pos; break;
            case Warehouse.BOX: this.boxInitial = pos; break;
            case Warehouse.TARGET: this.target = pos; break;
          }
        }
      }

      this.initialState = new WarehouseState(this, this.storekeeperInitial, this.boxInitial, 0);
    }
  }
  class MainClass
  {
    public static bool printToConsole = false;
    public static InputReader reader = new InputReader();
    static int Main(string[] args)
    {
      var stopwatch = new Stopwatch();
      stopwatch.Start().Stop().Reset().Start();

      // For Recodex
      foreach (string arg in args)
        if (arg == "--dev")
          printToConsole = true;

      WarehouseState state = null, nextState = null;
      var warehouse = new Warehouse(reader.ReadRest().Replace("\n", ""));
      var tracker = warehouse.Tracker();
      var queue = new LinkedList<WarehouseState>();

      int iterations = 0;

      tracker.RegisterState(warehouse.InitialState);
      queue.InsertLast(warehouse.InitialState);

      while (!queue.Empty)
      {
        iterations++;

        state = queue.ExtractFirst();

        if (state.Box == warehouse.Target)
          break;

        var mutations = state.Mutate().Iterator();
        while (!mutations.Done)
        {
          nextState = mutations.Next().Value;
          if (!tracker.StateDiscovered(nextState))
          {
            tracker.RegisterState(nextState);
            queue.InsertLast(nextState);
          }
        }
      }

      Console.WriteLine(state != null && state.Box == warehouse.Target ? state.Moves : -1);

      if (MainClass.printToConsole)
      {
        Console.WriteLine("Finished in {0} iterations", iterations);
        Console.WriteLine("Executed in {0} ms", stopwatch.Stop().Milliseconds);
      }

      return 0;
    }
  }
}
