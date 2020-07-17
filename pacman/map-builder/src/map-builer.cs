using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapBuilderNamespace
{
  class XY
  {
    public int X;
    public int Y;
    public XY(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }
  }
  class Cluster
  {
    private int size = 0;
    public int Size { get => this.size; }
    private int top = Int32.MaxValue;
    public int Top { get => this.top; }
    private int bottom = -1;
    public int Bottom { get => this.bottom; }
    private int left = Int32.MaxValue;
    public int Left { get => this.left; }
    private int right = -1;
    public int Right { get => this.right; }
    public void AddPoint(XY point)
    {
      this.top = Math.Min(this.top, point.Y);
      this.bottom = Math.Max(this.bottom, point.Y);
      this.left = Math.Min(this.left, point.X);
      this.right = Math.Max(this.right, point.X);

      this.size++;
    }
  }
  class MainClass
  {
    const int MIN_BLUE = 200;
    static void Main()
    {
      Bitmap img = new Bitmap("/home/wiki/School/NPRG031/pacman/map-builder/background.jpg");

      // Look for obstacles (look for BLUE color)
      bool[,] obstacles = new bool[img.Width, img.Height];
      for (int x = 0; x < img.Width; x++)
        for (int y = 0; y < img.Height; y++)
          obstacles[x, y] = img.GetPixel(x, y).B >= MIN_BLUE;

      // Create a bitmap of obstacles for illustration
      Bitmap mask = new Bitmap(img.Width, img.Height);
      for (int x = 0; x < img.Width; x++)
        for (int y = 0; y < img.Height; y++)
          mask.SetPixel(x, y, obstacles[x, y] ? Color.Red : Color.Black);
      mask.Save("/home/wiki/School/NPRG031/pacman/map-builder/background-mask.jpg");

      // Scan for clusters
      bool[,] discovered = new bool[img.Width, img.Height];
      for (int x = 0; x < img.Width; x++)
        for (int y = 0; y < img.Height; y++)
          discovered[x, y] = false;

      List<Cluster> clusters = new List<Cluster>();

      for (int x = 0; x < img.Width; x++)
      {
        for (int y = 0; y < img.Height; y++)
        {
          if (!discovered[x, y] && obstacles[x, y])
          {
            discovered[x, y] = true;

            Cluster cluster = new Cluster();
            Queue<XY> queue = new Queue<XY>();

            queue.Enqueue(new XY(x, y));
            while (queue.Count > 0)
            {
              XY point = queue.Dequeue();
              cluster.AddPoint(point);

              XY[] points = new XY[] {
                new XY(point.X + 1, point.Y),
                new XY(point.X - 1, point.Y),
                new XY(point.X, point.Y + 1),
                new XY(point.X, point.Y - 1)
              };

              foreach (XY neighbor in points)
              {
                if (
                  neighbor.X >= 0 &&
                  neighbor.X < img.Width &&
                  neighbor.Y >= 0 &&
                  neighbor.Y < img.Height &&
                  !discovered[neighbor.X, neighbor.Y] &&
                  obstacles[neighbor.X, neighbor.Y]
                )
                {
                  discovered[neighbor.X, neighbor.Y] = true;
                  queue.Enqueue(neighbor);
                }
              }

              // Console.WriteLine(queue.Count);
            }

            // Console.WriteLine(cluster.Size);

            clusters.Add(cluster);
          }
        }
      }

      Console.WriteLine("Discovered {0} clusters", clusters.Count);

      Bitmap clustersMap = new Bitmap(img.Width, img.Height);

      for (int x = 0; x < img.Width; x++)
        for (int y = 0; y < img.Height; y++)
          clustersMap.SetPixel(x, y, Color.Black);

      foreach (Cluster cluster in clusters)
        for (int x = cluster.Left; x <= cluster.Right; x++)
          for (int y = cluster.Top; y <= cluster.Bottom; y++)
            clustersMap.SetPixel(x, y, Color.Red);

      clustersMap.Save("/home/wiki/School/NPRG031/pacman/map-builder/background-segments.jpg");
    }
  }
}
