using System;
using System.Text;

namespace MainNamespace
{
  class MainClass
  {
    public static Random rnd = new Random();
    [STAThread]
    static void Main()
    {
      Game game = new Game();
      game.Run();
    }
  }
}
