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
    public LinkedList<T> Clone()
    {
      var res = new LinkedList<T>();
      var iterator = this.Iterator();
      while (!iterator.Done)
        res.InsertLast(iterator.Next().Value);

      return res;
    }
    public LinkedList<T> RemoveAt(int at)
    {
      int i = 0;

      var item = this.first;
      while (item != null && i++ < at)
        item = item.Next;

      this.RemoveItem(item);

      return this;
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
  class Domino
  {
    // Format: NNHHTT
    // H - head
    // T - tail
    // N - number of dominos
    public int Head;
    public int Tail;
    public int Number;
    public static int Encode(Domino domino)
    {
      return domino.Number * 10000 + domino.Head * 100 + domino.Tail;
    }
    public static Domino Decode(int domino)
    {
      return new Domino(
        domino / 10000,               // Number
        (domino % 10000) / 100,       // Head
        domino % 100                  // Tail
      );
    }
    public Domino(int number, int head, int tail)
    {
      this.Number = number;
      this.Head = head;
      this.Tail = tail;
    }
  }
  class Search
  {
    public int Max = 0;
    public void RunSearch(LinkedList<int> dominos, int prev = -1, int length = 0)
    {
      if (length > this.Max)
        this.Max = length;

      var iterator = dominos.Iterator();
      while (!iterator.Done)
      {
        var domino = Domino.Decode(iterator.Next().Value);

        if (prev == -1 || prev == domino.Head || prev == domino.Tail)
        {
          var newDominos = dominos.Clone().RemoveAt(iterator.Iterations - 1);

          if (domino.Number > 1)
          {
            domino.Number--;
            newDominos.InsertLast(Domino.Encode(domino));
          }

          if (prev == -1 || prev == domino.Head)
            this.RunSearch(newDominos, domino.Tail, length + 1);

          if (prev == -1 || prev == domino.Tail)
            this.RunSearch(newDominos, domino.Head, length + 1);
        }
      }
    }
  }
  class MainClass
  {
    public static bool printToConsole = false;
    public static InputReader reader = new InputReader();
    static int Main(string[] args)
    {
      // For Recodex
      foreach (string arg in args)
        if (arg == "--dev")
          printToConsole = true;

      const int MAX_VALUE = 39;

      int[,] matrix = new int[MAX_VALUE, MAX_VALUE];
      for (int i = 0; i < MAX_VALUE; i++)
        for (int j = 0; j < MAX_VALUE; j++)
          matrix[i, j] = 0;

      var nDominos = reader.ReadInt();
      var initial = new LinkedList<int>();

      for (int i = 0; i < nDominos; i++)
      {
        int head = reader.ReadInt();
        int tail = reader.ReadInt();

        if (head < tail)
        {
          int temp = head;
          head = tail;
          tail = temp;
        }

        matrix[head, tail]++;
      }

      for (int i = 0; i < MAX_VALUE; i++)
        for (int j = 0; j < MAX_VALUE; j++)
          if (matrix[i, j] > 0)
            initial.InsertLast(Domino.Encode(new Domino(matrix[i, j], i, j)));

      var search = new Search();
      search.RunSearch(initial);

      Console.WriteLine(search.Max);

      return 0;
    }
  }
}
