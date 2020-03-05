using System;

namespace MainNamespace
{
  class InputReader
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
  }

  class MainClass
  {
    static void Main()
    {
      int num = InputReader.ReadInt();
      int div = 2;

      Console.Write(num);
      Console.Write("=");

      while (true)
      {
        if (num % div == 0)
        {
          Console.Write(div);
          num /= div;
          div = 1;

          if (num == 1)
            break;

          Console.Write("*");
        }

        div++;
      }
    }
  }
}
