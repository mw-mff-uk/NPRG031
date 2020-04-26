using System;
using System.Text;


namespace MainNamespace
{
  class A
  {
    public static int ORDER_ASC = 0;
    public static int ORDER_DESC = 1;
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
    public static void SortParallel<T>(int[] keys, T[] values, int order = 0)
    {
      if (keys.Length != values.Length)
        throw new Exception("keys and values have different length");

      bool stop = false;
      while (!stop)
      {
        stop = true;
        for (int curr = 1; curr < keys.Length; curr++)
        {
          int prev = curr - 1;
          if ((order == A.ORDER_ASC && keys[prev] > keys[curr]) || (order == A.ORDER_DESC && keys[prev] < keys[curr]))
          {
            A.Swap(keys, prev, curr);
            A.Swap(values, prev, curr);
            stop = false;
          }
        }
      }
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
        Console.WriteLine("Extracting first item: {0}", value.ToString());
        this.first = this.first.Next;

        if (this.first != null)
          this.first.Prev = null;
      }

      // Last item
      else if (item == this.last)
      {
        Console.WriteLine("Extracting last item: {0}", value.ToString());
        this.last = this.last.Prev;

        if (this.last != null)
          this.last.Next = null;
      }

      // Middle item
      else
      {
        Console.WriteLine("Extracting middle item: {0}", value.ToString());
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
  class Keyboard
  {
    private int minChar;
    private int maxChar;
    private int cols;
    private int rows;
    private char[] sequence;
    public string Sequence { get => new string(this.sequence); }
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
    public Keyboard SetContent(char[] content)
    {
      // Get min and max char value
      int maxChar = 0;
      int minChar = -1;
      for (int i = 0; i < content.Length; i++)
      {
        int ch = (int)content[i];

        if (ch > maxChar)
          maxChar = ch;

        if (ch < minChar || minChar == -1)
          minChar = ch;
      }

      return this.SetContent(content, 0, maxChar);
    }
    public Keyboard SetContent(char[] content, int minChar, int maxChar)
    {
      this.minChar = minChar;
      this.maxChar = maxChar;
      this.size = content.Length;

      // Initiate the alphabet
      this.alphabet = new LinkedList<int>[this.maxChar - this.minChar + 1];
      for (int i = 0; i < this.alphabet.Length; i++)
        this.alphabet[i] = new LinkedList<int>();

      // Fill the alphabet
      for (int i = 0; i < this.size; i++)
        this.alphabet[(int)content[i] - this.minChar].InsertLast(i);

      return this;
    }
    public int GetStrokes()
    {
      // Parse the input sequence
      int letters = this.sequence.Length;

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
        LinkedListIterator<int> positions = this.GetLetter(letter).Iterator();

        while (!positions.Done)
        {
          int position = positions.Next().Value;
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

            int distance = this.ManhattanDistance(currPos, nextPos);

            // Look for a faster way to get there
            if (nextMoves == -1 || nextMoves > currMoves + distance)
              layers[nextLayer, nextNode, MOVES_INDEX] = currMoves + distance;
          }
        }
      }

      // Print the layers with moves
      // if (printToConsole)
      // {
      //   for (int layer = 0; layer <= letters; layer++)
      //   {
      //     Console.WriteLine("--------------------------------------------- Layer [{0}]", layer);

      //     char letter = layer == 0 ? '-' : sequence[layer - 1];

      //     for (int node = 0; node < layerSizes[layer]; node++)
      //     {
      //       Console.WriteLine(
      //         "Node {0}: {1} @ {2} in {3} moves",
      //         node,
      //         letter,
      //         this.ToXY(layers[layer, node, POSITION_INDEX]).ToString(),
      //         layers[layer, node, MOVES_INDEX]
      //       );
      //     }
      //   }
      // }

      // Find the optimal path
      int optimal = -1;
      int lastLayer = letters;
      for (int node = 0; node < layerSizes[lastLayer]; node++)
        if (layers[lastLayer, node, MOVES_INDEX] < optimal || optimal == -1)
          optimal = layers[lastLayer, node, MOVES_INDEX];

      return optimal + letters;
    }
    public Keyboard(int cols, int rows, string text)
    {
      this.cols = cols;
      this.rows = rows;
      this.sequence = text.ToCharArray();
    }
  }
  class MainClass
  {
    public static bool printToConsole = false;
    public static InputReader reader = new InputReader();
    public static Stopwatch stopwatch = new Stopwatch();
    public static Random rnd = new Random();
    private static void TaskOne()
    {
      stopwatch.Start();

      int cols = reader.ReadInt();
      int rows = reader.ReadInt();
      string content = reader.ReadLine();
      string text = reader.ReadRest().Substring(1);

      Console.WriteLine("\nSize: {0}x{1}", cols, rows);
      Console.WriteLine("Grid: |{0}|\n", content);

      Keyboard keyboard = new Keyboard(cols, rows, text);
      keyboard.SetContent(content.ToCharArray());

      Console.WriteLine("{0}", keyboard.GetStrokes());
      Console.WriteLine("\nExecution time: {0}ms", stopwatch.Stop().Milliseconds);
    }
    private static void TaskTwo()
    {
      #region task-two-setup
      string text = reader.ReadLine();
      // Console.WriteLine("\nThe text has {0} characters", text.Length);

      const int MIN_CHAR = 0;
      const int MAX_CHAR = 122;
      const int GRID_WIDTH = 8;
      const int GRID_HEIGHT = 8;
      const int GRID_SIZE = GRID_WIDTH * GRID_HEIGHT;

      // Frequency of each letter in text
      int[] frequency = new int[MAX_CHAR + 1];
      // How many times will letter appear on the keyboard
      int[] gridCount = new int[MAX_CHAR + 1];
      for (int i = 0; i < MAX_CHAR + 1; i++)
      {
        frequency[i] = 0;
        gridCount[i] = 0;
      }

      // Compute the letter frequency
      int distinct = 0;
      foreach (char ch in text)
      {
        if ((int)ch == 0)
          continue;

        if (frequency[(int)ch] == 0)
          distinct++;

        frequency[(int)ch]++;
      }

      // Each letter will appear at least once on the keyboard
      int unitFrequency = text.Length / distinct;
      for (int i = 0; i < MAX_CHAR + 1; i++)
        if (frequency[i] > 0)
          gridCount[i] = 1;

      // Make more frequent letter appear more times on the keyboard
      int spare = GRID_SIZE - distinct;
      int run = 2;
      while (spare > 0)
      {
        for (int i = 0; i < MAX_CHAR + 1; i++)
        {
          if (gridCount[i] > 0 && frequency[i] > unitFrequency * (run - 0.5))
          {
            gridCount[i]++;
            spare--;

            if (spare == 0)
              break;
          }
        }

        run++;
      }

      // Build the grid
      string gridText = "";
      for (int i = 0; i < MAX_CHAR + 1; i++)
        for (int j = 0; j < gridCount[i]; j++)
          if (gridText.Length < GRID_SIZE)
            gridText += (char)i;

      // Print the results
      // Console.WriteLine("{0} distinct characters were found\n", distinct);
      // Console.WriteLine("Code\tChar\tFreq\tGrid");
      // for (int i = 0; i < MAX_CHAR + 1; i++)
      //   if (frequency[i] > 0)
      //     Console.WriteLine(
      //       "{0}\t{1}\t{2}\t{3}",
      //       i, (char)i, frequency[i], gridCount[i]
      //     );
      Console.WriteLine("\nFinal grid: |{0}|", gridText);
      #endregion

      // Create keyboard
      var grid = gridText.ToCharArray();
      var keyboard = new Keyboard(GRID_WIDTH, GRID_HEIGHT, text);

      // Select the strategy
      const int STRATEGY = 1;

      while (true)
      {
        // Random shuffle
        A.Shuffle(grid, rnd);
        keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);
        int optimal = keyboard.GetStrokes();

        // Console.WriteLine(
        //   "\n\n{0} Running strategy {1}",
        //   new string(A.Rep(new char[] { '-' }, 110)), STRATEGY
        // );
        // Console.WriteLine(" Initial: |{0}|", new string(grid));
        // Console.WriteLine("Baseline: {0}", optimal);

        // Start optimizing
        int iterations = 1;
        bool stop = false;
        while (!stop)
        {
          // Console.WriteLine("\n\n----- Iteration {0} -----", iterations++);
          stopwatch.Reset().Start();

          stop = true;
          int swaps = 0;
          int before = optimal;

          // section [ONE]
          if (STRATEGY == 1)
          {
            for (int i = 0; i < GRID_SIZE; i++)
            {
              for (int j = i + 1; j < GRID_SIZE; j++)
              {
                // Try swap i <--> j
                A.Swap(grid, i, j);
                keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);

                // Compute the number of strokes
                int strokes = keyboard.GetStrokes();
                if (strokes < optimal)
                {
                  // Console.WriteLine(
                  //   "Grid: |{0}| Strokes: {1}\t [-{2}]",
                  //   new string(grid), strokes, optimal - strokes
                  // );

                  optimal = strokes;
                  stop = false;

                  break;
                }
                else
                {
                  // Get keyboard back to original state
                  A.Swap(grid, i, j);
                  keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);
                }
              }

              // Progress log
              if (i < GRID_SIZE - 1)
              {
                // Console.Write('[');
                // for (int k = 0; k < GRID_SIZE; k++)
                //   Console.Write(k < i ? '=' : k == i ? '>' : ' ');
                // Console.Write(']');
                // Console.Write(" {0}/{1}\r", i, GRID_SIZE);
              }
              else
              {
                // Console.WriteLine(new string(A.Rep(new char[] { ' ' }, 100)));
              }
            }

            // Iteration log
            // Console.WriteLine("        Time: {0}s", Math.Round(stopwatch.Stop().Seconds, 2));
            // Console.WriteLine("       Swaps: {0}", swaps);
            // Console.WriteLine(" Improvement: {0}", before - optimal);
          }

          // section [TWO]
          // Only best swap in inner iteration
          if (STRATEGY == 2)
          {
            for (int i = 0; i < GRID_SIZE; i++)
            {
              int optimalSwap = optimal;
              int optimalSwapIndex = -1;

              // Find optimal swap
              for (int j = 0; j < GRID_SIZE; j++)
              {
                // Try swap i <--> j
                A.Swap(grid, i, j);
                keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);

                // Compute the number of strokes
                int strokes = keyboard.GetStrokes();
                if (strokes < optimalSwap)
                {
                  optimalSwap = strokes;
                  optimalSwapIndex = j;
                }

                // Get keyboard back to original state
                A.Swap(grid, i, j);
                keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);
              }

              // Swap
              if (optimalSwap < optimal)
              {
                A.Swap(grid, i, optimalSwapIndex);
                keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);

                Console.WriteLine(
                  "Grid: |{0}| Strokes: {1}\t[-{2}]",
                  new string(grid), optimalSwap, optimal - optimalSwap
                );

                optimal = optimalSwap;
                stop = false;
              }

              // Progress log
              if (i < GRID_SIZE - 1)
              {
                Console.Write('[');
                for (int k = 0; k < GRID_SIZE; k++)
                  Console.Write(k < i ? '=' : k == i ? '>' : ' ');
                Console.Write(']');
                Console.Write(" {0}/{1}\r", i, GRID_SIZE);
              }
              else
              {
                Console.WriteLine(new string(A.Rep(new char[] { ' ' }, 100)));
              }
            }

            // Iteration log
            Console.WriteLine("        Time: {0}s", Math.Round(stopwatch.Stop().Seconds, 2));
            Console.WriteLine("       Swaps: {0}", swaps);
            Console.WriteLine(" Improvement: {0}", before - optimal);
          }

          // section [THREE]
          // Only best swap in the whole grid
          else if (STRATEGY == 3)
          {
            int optimalSwap = optimal;
            int optimalSwapA = -1;
            int optimalSwapB = -1;

            // Find optimal swap
            for (int i = 0; i < GRID_SIZE; i++)
            {
              for (int j = 0; j < GRID_SIZE; j++)
              {
                A.Swap(grid, i, j);
                keyboard.SetContent(grid);
                int strokes = keyboard.GetStrokes();

                if (strokes < optimalSwap)
                {
                  optimalSwap = strokes;
                  optimalSwapA = i;
                  optimalSwapB = j;
                }

                A.Swap(grid, i, j);
                keyboard.SetContent(grid);
              }

              // Progress log
              if (i < GRID_SIZE - 1)
              {
                Console.Write('[');
                for (int k = 0; k < GRID_SIZE; k++)
                  Console.Write(k < i ? '=' : k == i ? '>' : ' ');
                Console.Write(']');
                Console.Write(" {0}/{1}\r", i, GRID_SIZE);
              }
              else
              {
                Console.WriteLine(new string(A.Rep(new char[] { ' ' }, 100)));
              }
            }

            // Swap
            if (optimalSwap < optimal)
            {
              A.Swap(grid, optimalSwapA, optimalSwapB);
              keyboard.SetContent(grid);

              Console.WriteLine(
                "Grid: |{0}| Strokes: {1}\t[-{2}]\nTime: {3}s\n",
                new string(grid), optimalSwap, optimal - optimalSwap, Math.Round(stopwatch.Stop().Seconds, 2)
              );

              optimal = optimalSwap;
              stop = false;
            }
          }
        }

        // Best solution
        keyboard.SetContent(grid, MIN_CHAR, MAX_CHAR);
        Console.WriteLine("Grid: |{0}| Strokes: {1}", new string(grid), keyboard.GetStrokes());
      }
    }
    static int Main(string[] args)
    {
      // Args parsing
      int task = 1;
      foreach (string arg in args)
        if (arg == "--print-to-console")
          printToConsole = true;
        else if (arg.StartsWith("--task:"))
          task = Convert.ToInt32(arg.Split(":")[1]);

      stopwatch.Start().Stop().Reset();

      switch (task)
      {
        case 1: TaskOne(); break;
        case 2: TaskTwo(); break;
      }

      return 0;
    }
  }
}
