using System;
using System.Text;
// {{ using }}

namespace MainNamespace
{
  // {{ classes }}
  class MainClass
  {
    static int Main(string[] args)
    {
      // For Recodex
      bool printToConsole = false;
      foreach (string arg in args)
        if (arg == "--print-to-console")
          printToConsole = true;

      // {{ main }}
    }
  }
}
