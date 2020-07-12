using System;
using System.Text;

namespace MainNamespace
{
  class MainClass
  {
    public static Stopwatch stopwatch = new Stopwatch();
    public static Random rnd = new Random();
    [STAThread]
    static void Main()
    {
      MainClass.stopwatch.Start().Stop().Reset();

      Game game = new Game();
      game.Run();
    }
  }
}
