using System;
using System.Collections.Generic;
using System.Drawing;

namespace MapBuilderNamespace
{
  class Box
  {
    public int Left;
    public int Top;
    public int Width;
    public int Height;
    public int Right { get => this.Left + this.Width - 1; }
    public int Bottom { get => this.Top + this.Height - 1; }
    public Box(int left, int top, int width, int height)
    {
      this.Left = left;
      this.Top = top;
      this.Width = width;
      this.Height = height;
    }
  }
  class XY
  {
    public int X;
    public int Y;
    public bool IsInsideBox(Box box)
    {
      return (
        this.X >= box.Left &&
        this.X <= box.Right &&
        this.Y >= box.Top &&
        this.Y <= box.Bottom
      );
    }
    public XY[] GetNeighbors()
    {
      return new XY[] {
        new XY(this.X + 1, this.Y),
        new XY(this.X - 1, this.Y),
        new XY(this.X, this.Y + 1),
        new XY(this.X, this.Y - 1)
      };
    }
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
  class BinaryMatrix
  {
    private bool[,] items;
    private int width;
    public int Width { get => this.width; }
    private int height;
    public int Height { get => this.height; }
    public int Size { get => this.width * this.height; }
    public Bitmap ToBitmap(Color falseColor, Color trueColor)
    {
      Bitmap map = new Bitmap(this.width, this.height);

      for (int x = 0; x < this.width; x++)
        for (int y = 0; y < this.height; y++)
          map.SetPixel(x, y, this.items[x, y] ? trueColor : falseColor);

      return map;
    }
    public bool this[int x, int y]
    {
      get => this.items[x, y];
      set => this.items[x, y] = value;
    }
    public bool this[XY point]
    {
      get => this.items[point.X, point.Y];
      set => this.items[point.X, point.Y] = value;
    }
    public BinaryMatrix(int width, int height)
    {
      this.width = width;
      this.height = height;

      this.items = new bool[width, height];
      for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
          this[x, y] = false;
    }
  }
  class Map
  {
    private Bitmap img;
    private Box box;
    public Box Box { get => this.box; }
    private int width;
    public int Width { get => this.width; }
    private int height;
    public int Height { get => this.height; }
    public BinaryMatrix ScanObstacles(int blueThreshold)
    {
      BinaryMatrix obstacles = new BinaryMatrix(this.width, this.height);

      for (int x = 0; x < this.width; x++)
        for (int y = 0; y < this.height; y++)
          obstacles[x, y] = this.img.GetPixel(x, y).B >= blueThreshold;

      return obstacles;
    }
    public Map(string imgSrc)
    {
      this.img = new Bitmap(imgSrc);

      this.width = img.Width;
      this.height = img.Height;

      this.box = new Box(0, 0, this.width, this.height);
    }
  }
  class MainClass
  {
    const string FILE_BASE = "/home/wiki/School/NPRG031/pacman/map-builder/";
    static void PrintObstacleMap(Map map, int blueThreshold)
    {
      map
        .ScanObstacles(blueThreshold)
        .ToBitmap(Color.Black, Color.Red)
        .Save(FILE_BASE + "background-mask.jpg");
    }
    static void PrintSegmentMap(Map map, int blueThreshold)
    {
      BinaryMatrix discovered = new BinaryMatrix(map.Width, map.Height);
      BinaryMatrix obstacles = map.ScanObstacles(blueThreshold);
      List<Cluster> clusters = new List<Cluster>();

      for (int x = 0; x < map.Width; x++)
      {
        for (int y = 0; y < map.Height; y++)
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

              foreach (XY neighbor in point.GetNeighbors())
              {
                if (
                  neighbor.IsInsideBox(map.Box) &&
                  !discovered[neighbor] &&
                  obstacles[neighbor]
                )
                {
                  discovered[neighbor] = true;
                  queue.Enqueue(neighbor);
                }
              }
            }

            clusters.Add(cluster);
          }
        }
      }

      Console.WriteLine("Discovered {0} clusters", clusters.Count);

      Bitmap clustersMap = new Bitmap(map.Width, map.Height);

      for (int x = 0; x < map.Width; x++)
        for (int y = 0; y < map.Height; y++)
          clustersMap.SetPixel(x, y, Color.Black);

      foreach (Cluster cluster in clusters)
        for (int x = cluster.Left; x <= cluster.Right; x++)
          for (int y = cluster.Top; y <= cluster.Bottom; y++)
            clustersMap.SetPixel(x, y, Color.Red);

      clustersMap.Save(FILE_BASE + "background-segments.jpg");
    }
    static void PrintCorridorMap(Map map, XY initial, int blueThreshold)
    {
      BinaryMatrix discovered = new BinaryMatrix(map.Width, map.Height);
      BinaryMatrix corridors = new BinaryMatrix(map.Width, map.Height);
      BinaryMatrix obstacles = map.ScanObstacles(blueThreshold);
      Queue<XY> queue = new Queue<XY>();

      queue.Enqueue(initial);
      discovered[initial.X, initial.Y] = true;

      while (queue.Count > 0)
      {
        XY point = queue.Dequeue();
        corridors[point] = true;

        foreach (XY neighbor in point.GetNeighbors())
        {
          if (
            neighbor.IsInsideBox(map.Box) &&
            !discovered[neighbor] &&
            !obstacles[neighbor]
          )
          {
            discovered[neighbor] = true;
            queue.Enqueue(neighbor);
          }
        }
      }

      corridors.ToBitmap(Color.Black, Color.Green).Save(FILE_BASE + "background-corridors.jpg");
    }
    static void Main()
    {
      Map map = new Map(FILE_BASE + "background.jpg");

      PrintObstacleMap(map, 180);
      PrintSegmentMap(map, 180);
      PrintCorridorMap(map, new XY(120, 789), 80);
    }
  }
}
