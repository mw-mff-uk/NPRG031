using System;
using System.IO;

namespace MainNamespace
{
  static class FileManager
  {
    private static string rootDir = AppDomain.CurrentDomain.BaseDirectory;
    public static string GetRoot()
    {
      return Path.Combine(FileManager.rootDir, "src");
    }
    public static string GetImage(string file)
    {
      return Path.Combine(FileManager.GetRoot(), "images", file);
    }
    public static string GetData(string file)
    {
      return Path.Combine(FileManager.GetRoot(), "data", file);
    }
  }
}
