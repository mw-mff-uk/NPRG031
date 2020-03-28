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
  class Printer
  {
    public static void APrint<T>(T[] arr)
    {
      for (int i = 0; i < arr.Length; i++)
        Console.Write(arr[i] + " ");

      Console.WriteLine();
    }
  }
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
  class Queue<T>
  {
    private QueueItem<T> _first;
    private QueueItem<T> _last;
    private int _length = 0;
    public int Length { get => this._length; }
    public bool CanDequeue { get => this._length > 0; }
    public Queue<T> Enqueue(T value)
    {
      QueueItem<T> item = new QueueItem<T>(value);

      if (this._first == null)
        this._first = item;

      if (this._last != null)
        this._last.next = item;

      this._last = item;

      this._length++;
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

      this._length--;
      return value;
    }
    public Queue(T[] items = null)
    {
      if (items != null)
        for (int i = 0; i < items.Length; i++)
          this.Enqueue(items[i]);
    }
  }
  class Cups
  {
    private int[] capacity;
    private int[] volume;
    public int[] Capacity { get => this.capacity; }
    public int[] Volume { get => this.volume; }
    public Cups Swap(int from, int to)
    {
      int move = Math.Min(this.volume[from], this.capacity[to] - this.volume[to]);

      this.volume[from] -= move;
      this.volume[to] += move;

      return this;
    }
    public override String ToString() => String.Format(
      "{0} {1} {2} | {3} {4} {5}",
      this.capacity[0], this.capacity[1], this.capacity[2], // Capacity
      this.volume[0], this.volume[1], this.volume[2]        // Volume
    );
    public static Cups Copy(Cups cups) => new Cups(
      cups.Capacity[0], cups.Capacity[1], cups.Capacity[2],  // Capacity
      cups.Volume[0], cups.Volume[1], cups.Volume[2]         // Volume
    );
    public Cups(int capA, int capB, int capC, int volA, int volB, int volC)
    {
      this.capacity = new int[3] { capA, capB, capC };
      this.volume = new int[3] { volA, volB, volC };
    }
  }
  class MainClass
  {
    static void Main()
    {
      const int MAX_VOLUME = 10;

      Cups cups, cups2;
      InputReader reader = new InputReader();

      int[] optimal = new int[MAX_VOLUME + 1];
      for (int i = 0; i <= MAX_VOLUME; i++)
        optimal[i] = -1;

      int[,,] moves = new int[MAX_VOLUME + 1, MAX_VOLUME + 1, MAX_VOLUME + 1];
      for (int i = 0; i <= MAX_VOLUME; i++)
        for (int j = 0; j <= MAX_VOLUME; j++)
          for (int k = 0; k <= MAX_VOLUME; k++)
            moves[i, j, k] = -1;

      cups = new Cups(
        reader.ReadInt(), reader.ReadInt(), reader.ReadInt(), // Capacity
        reader.ReadInt(), reader.ReadInt(), reader.ReadInt()  // Volume
      );

      moves[cups.Volume[0], cups.Volume[1], cups.Volume[2]] = 0;

      for (int i = 0; i < 3; i++)
        optimal[cups.Volume[i]] = 0;

      Queue<Cups> queue = new Queue<Cups>();
      queue.Enqueue(cups);
      while (queue.CanDequeue)
      {
        cups = queue.Dequeue();
        int move = moves[cups.Volume[0], cups.Volume[1], cups.Volume[2]];

        // Console.WriteLine(cups.ToString());

        for (int from = 0; from < 3; from++)
        {
          for (int to = 0; to < 3; to++)
          {
            if (from != to)
            {
              cups2 = Cups.Copy(cups).Swap(from, to);

              if (moves[cups2.Volume[0], cups2.Volume[1], cups2.Volume[2]] == -1)
              {
                moves[cups2.Volume[0], cups2.Volume[1], cups2.Volume[2]] = move + 1;

                for (int i = 0; i < 3; i++)
                  if (optimal[cups2.Volume[i]] == -1)
                    optimal[cups2.Volume[i]] = move + 1;

                queue.Enqueue(cups2);
              }
            }
          }
        }
      }

      for (int i = 0; i <= MAX_VOLUME; i++)
        if (optimal[i] > -1)
          Console.Write("{0}:{1} ", i, optimal[i]);

      Console.WriteLine();
    }
  }
}
