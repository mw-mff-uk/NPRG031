using System;

namespace MainNamespace
{
  class Char
  {
    private int _value;
    public int Value { get { return this._value; } }

    public bool IsEmpty
    {
      get
      {
        return (
          this._value == ' ' ||
          this._value == '\n' ||
          this._value == '\t' ||
          this._value == '\r' ||
          this._value == '\v'
        );
      }
    }

    public bool IsNum
    {
      get
      {
        return (this._value >= '0' && this._value <= '9');
      }
    }

    public bool IsAlph
    {
      get
      {
        return (
          (symbol >= 'a' && symbol <= 'z') ||
          (symbol >= 'A' && symbol <= 'A')
        );
      }
    }

    public Char(int value)
    {
      this._value = value;
    }
  }

  class InputReader
  {
    private int[] _buffer;


    public static int ReadInt()
    {
      int factor = 1;
      int s = Console.Read();

      while (this)

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

    public InputReader()
    {
      this._buffer = new int[100];
    }
  }

  class MainClass
  {
    static void Main()
    {
      // Your code goes here...
    }
  }
}
