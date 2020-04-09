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
  class XY
  {
    public int x;
    public int y;
    override public string ToString() => "(" + this.x + "," + this.y + ")";
    public static bool operator ==(XY a, XY b) => (a.x == b.x && a.y == b.y);
    public static bool operator !=(XY a, XY b) => (a.x != b.x || a.y != b.y);
    public XY(int x, int y)
    {
      this.x = x;
      this.y = y;
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
    private LinkedListItem<T> first;
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
  class Keyboard
  {
    private int minChar;
    private int maxChar;
    private int cols;
    private int rows;
    private int size;
    public int Size { get => this.size; }
    private LinkedList<int>[] alphabet;
    public XY ToXY(int pos) => new XY(pos % this.cols, pos / this.cols);
    public int ManhattanDistance(int a, int b) => Math.Abs(
      // |ax - bx| + |ay - by|
      (a % this.cols) - (b % this.cols)) + Math.Abs((a / this.cols) - (b / this.cols)
    );
    public bool HasLetter(char letter)
    {
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
    }
  }
  class MainClass
  {
    static int Main(string[] args)
    {
      // For Recodex
      bool printToConsole = false;
      foreach (string arg in args)
        if (arg == "--print-to-console")
          printToConsole = true;

      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start().Stop().Reset().Start();

      InputReader reader = new InputReader();

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

      // One layer for each letter in sequence
      const int POSITION_INDEX = 0;
      const int MOVES_INDEX = 1;
      int[] layerSizes = new int[letters + 1];
      int[,,] layers = new int[letters + 1, 11, 2]; // [<layer>, <node> <path data>]

      // First layer (no letters)
      layerSizes[0] = 1;
      layers[0, 0, POSITION_INDEX] = 0;
      layers[0, 0, MOVES_INDEX] = 0;

      // Initiate the layers
      for (int layer = 0; layer < letters; layer++)
      {
        int nextlayer = layer + 1;

        char letter = sequence[layer];
        LinkedListIterator<int> positions = keyboard.GetLetter(letter).Iterator();

        while (!positions.Done)
        {
          int position = positions.Next();
          int nextNode = positions.Iterations - 1;

          layers[nextlayer, nextNode, MOVES_INDEX] = -1;
          layers[nextlayer, nextNode, POSITION_INDEX] = position;
        }

        layerSizes[nextlayer] = positions.Iterations;
      }

      // Compute the distances
      for (int layer = 0; layer < letters; layer++)
      {
        int nextLayer = layer + 1;

        // For each node in a layer
        for (int node = 0; node < layerSizes[layer]; node++)
        {
          int currPos = layers[layer, node, POSITION_INDEX];
          int currMoves = layers[layer, node, MOVES_INDEX];

          for (int nextNode = 0; nextNode < layerSizes[nextLayer]; nextNode++)
          {
            int nextPos = layers[nextLayer, nextNode, POSITION_INDEX];
            int nextMoves = layers[nextLayer, nextNode, MOVES_INDEX];

            int distance = keyboard.ManhattanDistance(currPos, nextPos);

            // Look for a faster way to get there
            if (nextMoves == -1 || nextMoves > currMoves + distance)
              layers[nextLayer, nextNode, MOVES_INDEX] = currMoves + distance;
          }
        }
      }

      // Print the layers with moves
      if (printToConsole)
      {
        for (int layer = 0; layer <= letters; layer++)
        {
          Console.WriteLine("--------------------------------------------- Layer [{0}]", layer);

          char letter = layer == 0 ? '-' : sequence[layer - 1];

          for (int node = 0; node < layerSizes[layer]; node++)
          {
            Console.WriteLine(
              "Node {0}: {1} @ {2} in {3} moves",
              node,
              letter,
              keyboard.ToXY(layers[layer, node, POSITION_INDEX]).ToString(),
              layers[layer, node, MOVES_INDEX]
            );
          }
        }
      }

      // Find the optimal path
      int optimal = -1;
      int lastLayer = letters;
      for (int node = 0; node < layerSizes[lastLayer]; node++)
        if (layers[lastLayer, node, MOVES_INDEX] < optimal || optimal == -1)
          optimal = layers[lastLayer, node, MOVES_INDEX];

      Console.WriteLine(optimal + letters);

      if (printToConsole)
        Console.WriteLine("{0}ms", stopwatch.Stop().Milliseconds);

      return 0;
    }
  }
}
