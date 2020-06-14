using System;
using System.Text;

namespace MainNamespace
{
  abstract class Command
  {
    public abstract string GetName();
    public abstract void Execute();
    public virtual void Die(string msg)
    {
      Console.WriteLine(msg);
      Environment.Exit(1);
    }
  }
  class TestCommand : Command
  {
    public override string GetName()
    {
      return "test";
    }
    public override void Execute()
    {
      Console.WriteLine("Ahoj, Mirka!");
      this.Die("nevyšlo");
      Console.WriteLine("Ahoj, Mirka!");

    }
    public override void Die(string str)
    {
      Console.WriteLine("{0}, :))", str);
    }
  }
  class HelpCommand : Command
  {
    public override string GetName()
    {
      return "help";
    }
    public override void Execute()
    {
      Console.WriteLine("Available commands:");
      Console.WriteLine("  help - print this help");
      this.Die("ups");
      Console.WriteLine("  test - test if the app works");
      Console.WriteLine("  math - some advanced math");
    }
  }
  class MathCommand : Command
  {
    public override string GetName()
    {
      return "math";
    }
    public override void Execute()
    {
      Console.WriteLine("2 + 2 = 4");
    }
  }
  class CommandHandler
  {
    private Command FromString(string str)
    {
      switch (str)
      {
        case "test":
          return new TestCommand();
        case "help":
          return new HelpCommand();
        case "math":
          return new MathCommand();
        default:
          return null;
      }
    }
    public bool ExecuteCommand(string str)
    {
      Command cmd = this.FromString(str);

      if (cmd == null)
      {
        Console.WriteLine("Unknown command: {0}", str);
        return false;
      }

      Console.WriteLine("Excecuting command: {0}\n", cmd.GetName());
      cmd.Execute();

      return true;
    }
  }
  class MainClass
  {
    static int Main(string[] args)
    {
      CommandHandler handler = new CommandHandler();
      string cmd = Console.ReadLine();

      handler.ExecuteCommand(cmd);

      return 0;
    }
  }
}
