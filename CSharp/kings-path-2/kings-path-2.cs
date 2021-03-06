using System;

namespace MainNamespace
{

  class Printer
  {
    public static void Repeat(int width, string str)
    {
      while (width-- > 0)
        Console.Write(str);

      Console.WriteLine();
    }
  }


  class Reader
  {
    public static int ReadInt()
    {
      int factor = 1;
      int z = Console.Read();
      while ((z < '0') || (z > '9'))
      {
        if (z == '-')
        {

          factor = -1;
          z = Console.Read();
          break;
        }
        z = Console.Read();
      }

      if ((z < '0') || (z > '9'))
        throw new Exception("Invalid character after minus sign");

      int x = 0;
      while ((z >= '0') && (z <= '9'))
      {
        x = 10 * x + z - '0';
        z = Console.Read();
      }

      return factor * x;
    }

    public static XY ReadXY(int add)
    // Read X first, Y last
    {
      int x = Reader.ReadInt();
      int y = Reader.ReadInt();
      return new XY(x + add, y + add);
    }

    public static XY ReadYX(int add)
    // Read Y first, X last
    {
      int y = Reader.ReadInt();
      int x = Reader.ReadInt();
      return new XY(x + add, y + add);
    }
  }


  class XY
  {
    public int x;
    public int y;

    override public string ToString()
    {
      return "(" + this.x + "," + this.y + ")";
    }

    public static bool Compare(XY a, XY b)
    {
      return a.x == b.x && a.y == b.y;
    }

    public XY(int x, int y)
    {
      this.x = x;
      this.y = y;
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

    public int Length
    {
      get
      {
        return this._length;
      }
    }

    public bool CanDequeue
    {
      get
      {
        return this._length > 0;
      }
    }

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

  class MainClass
  {
    static void Main()
    {
      XY[,] predecessors = new XY[8, 8];
      int[,] board = new int[8, 8];
      for (int x = 0; x < 8; x++)
        for (int y = 0; y < 8; y++)
        {
          board[x, y] = -1;
          predecessors[x, y] = null;
        }

      int nObstacles = Reader.ReadInt();

      for (int i = 0; i < nObstacles; i++)
      {
        XY xy = Reader.ReadXY(-1);
        board[xy.x, xy.y] = -2;
      }

      XY source = Reader.ReadXY(-1);
      XY target = Reader.ReadXY(-1);

      board[source.x, source.y] = 0;

      Queue<XY> queue = new Queue<XY>();
      queue.Enqueue(source);

      while (queue.CanDequeue)
      {
        XY pos = queue.Dequeue();

        for (int x = -1; x < 2; x++)
          for (int y = -1; y < 2; y++)
          {
            XY newPos = new XY(x + pos.x, y + pos.y);

            if (newPos.x < 0 || newPos.y < 0 || newPos.x > 7 || newPos.y > 7 || board[newPos.x, newPos.y] != -1)
              continue;

            board[newPos.x, newPos.y] = board[pos.x, pos.y] + 1;
            predecessors[newPos.x, newPos.y] = pos;
            queue.Enqueue(newPos);
          }
      }

      if (board[target.x, target.y] == -1)
      {
        Console.WriteLine(-1);
      }
      else
      {
        XY[] path = new XY[32];
        int cursor = 0;

        XY pos = target;
        while (predecessors[pos.x, pos.y] != null)
        {
          pos = predecessors[pos.x, pos.y];
          path[cursor++] = pos;
        }

        while (--cursor >= 0)
          Console.WriteLine((path[cursor].x + 1) + " " + (path[cursor].y + 1));

        Console.WriteLine((target.x + 1) + " " + (target.y + 1));
      }
    }
  }
}
