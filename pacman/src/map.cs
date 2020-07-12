using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Map : PictureBox
  {
    static int[] COLS = new int[] { 44, 95, 176, 258, 340, 423, 504, 588, 666, 720 };
    static int[] ROWS = new int[] { 44, 148, 228, 312, 392, 474, 556, 636, 716, 798 };
    private int gapHorizontal;
    private int gapVertical;
    private DirectedPoint[] stoppingPoints;
    public DirectedPoint[] StoppingPoints { get => this.stoppingPoints; }
    private DirectedPoint[] turningPoints;
    public DirectedPoint[] TurningPoints { get => this.turningPoints; }
    public int PacmanInitialLeft { get => 20 + this.gapHorizontal; }
    public int PacmanInitialTop { get => 392 - Game.AVATAR_SIZE / 2 + this.gapVertical; }
    public int PacmanInitialDirection { get => Direction.RIGHT; }
    private DirectedPoint Point(int row, int col, int direction)
    {
      int top = Map.ROWS[row] + this.gapVertical;
      int left = Map.COLS[col] + this.gapHorizontal;

      DirectedPoint point = new DirectedPoint(row, col, left, top, direction);

      return point;
    }
    public void Spawn(Form parent)
    {
      this.Parent = parent;
    }
    private void GenerateStoppingPoints()
    {
      int r = Direction.RIGHT;
      int l = Direction.LEFT;
      int u = Direction.UP;
      int d = Direction.DOWN;
      int all = r | l | u | d;

      this.stoppingPoints = new DirectedPoint[] {
        this.Point( 0, 0, l | u ), // 1
        this.Point( 0, 2, u ),     // 2 
        this.Point( 0, 4, r | u ), // 3
        this.Point( 0, 5, l | u ), // 4
        this.Point( 0, 7, u ),     // 5
        this.Point( 0, 9, u | r ), // 6
        this.Point( 1, 0, l ),     // 7
        this.Point( 1, 3, u ),     // 8
        this.Point( 1, 4, d ),     // 9
        this.Point( 1, 5, d ),     // 10
        this.Point( 1, 6, u ),     // 11
        this.Point( 1, 9, r ),     // 12
        this.Point( 2, 0, d | l ), // 13
        this.Point( 2, 2, r ),     // 14
        this.Point( 2, 3, d | l ), // 15
        this.Point( 2, 4, r | u ), // 16
        this.Point( 2, 5, l | u ), // 17
        this.Point( 2, 6, d | r ), // 18
        this.Point( 2, 7, l ),     // 19
        this.Point( 2, 9, r | d ), // 20
        this.Point( 3, 3, l | u ), // 21
        this.Point( 3, 4, d ),     // 22
        this.Point( 3, 5, d ),     // 23
        this.Point( 3, 6, r | u ), // 24
        this.Point( 4, 2, l ),     // 25
        this.Point( 4, 3, r ),     // 26
        this.Point( 4, 6, l ),     // 27
        this.Point( 4, 7, r ),     // 28
        this.Point( 5, 3, l ),     // 29
        this.Point( 5, 6, r ),     // 30
        this.Point( 6, 0, l | u ), // 31
        this.Point( 6, 3, d ),     // 32
        this.Point( 6, 4, r | u ), // 33
        this.Point( 6, 5, l | u ), // 34
        this.Point( 6, 6, d ),     // 35
        this.Point( 6, 9, r | u ), // 36
        this.Point( 7, 0, d | l ), // 37
        this.Point( 7, 1, r | u ), // 38
        this.Point( 7, 2, l ),     // 39
        this.Point( 7, 4, d ),     // 40
        this.Point( 7, 5, d ),     // 41
        this.Point( 7, 7, r ),     // 42
        this.Point( 7, 8, l | u ), // 43
        this.Point( 7, 9, r | d ), // 44
        this.Point( 8, 0, l | u ), // 45
        this.Point( 8, 1, d ),     // 46
        this.Point( 8, 2, d | r ), // 47
        this.Point( 8, 3, d | l ), // 48
        this.Point( 8, 4, r | u ), // 49
        this.Point( 8, 5, u | l ), // 50
        this.Point( 8, 6, d | r ), // 51
        this.Point( 8, 7, d | l ), // 52
        this.Point( 8, 8, d ),     // 53
        this.Point( 8, 9, r | u ), // 54
        this.Point( 9, 0, l | d ), // 55
        this.Point( 9, 9, r | d ), // 56
        this.Point( 7, 3, u ),     // 57
        this.Point( 7, 6, u )      // 58
      };
    }
    public Map(int gapHorizontal, int gapVertical)
    {
      this.gapHorizontal = gapHorizontal;
      this.gapVertical = gapVertical;

      this.Width = Game.WIDTH;
      this.Height = Game.HEIGHT;
      this.Top = this.gapVertical;
      this.Left = this.gapHorizontal;
      this.Image = Image.FromFile("/home/wiki/School/NPRG031/pacman/src/images/background.jpg");
      this.SizeMode = PictureBoxSizeMode.StretchImage;

      this.GenerateStoppingPoints();
    }
  }
}
