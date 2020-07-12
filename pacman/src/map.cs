using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class Map : PictureBox
  {
    private int[] cols = new int[] { 44, 95, 176, 258, 340, 423, 504, 588, 666, 720 };
    private int[] rows = new int[] { 44, 148, 228, 312, 392, 474, 556, 636, 716, 798 };
    private int gapHorizontal;
    private int gapVertical;
    private LinkedList<DirectedPoint> stoppingPoints;
    public LinkedList<DirectedPoint> StoppingPoints { get => this.stoppingPoints; }
    private LinkedList<DirectedPoint>[] stoppingPointsByCol;
    private LinkedList<DirectedPoint>[] stoppingPointsByRow;
    private LinkedList<DirectedPoint> turningPoints;
    public LinkedList<DirectedPoint> TurningPoints { get => this.turningPoints; }
    private LinkedList<DirectedPoint>[] turningPointsByCol;
    private LinkedList<DirectedPoint>[] turningPointsByRow;
    private LinkedList<Collectible> collectibles;
    public LinkedList<Collectible> Collectibles { get => this.collectibles; }
    public int PacmanInitialLeft { get => 20; }
    public int PacmanInitialTop { get => 392 - Game.AVATAR_SIZE / 2; }
    public int PacmanInitialDirection { get => Direction.RIGHT; }
    public int PacmanInitialRow { get => 4; }
    public int PacmanInitialCol { get => -1; }
    public int MonsterInitialLeft { get => 740; }
    public int MonsterInitialTop { get => 374; }
    public int MonsterInitialDirection { get => Direction.LEFT; }
    public int MonsterInitialRow { get => -1; }
    public int MonsterInitialCol { get => -1; }
    public bool CollectedAll
    {
      get
      {
        var iterator = this.collectibles.Iterator();
        while (!iterator.Done)
          if (!iterator.Next().Value.Collected)
            return false;

        return true;
      }
    }
    private void AddStoppingPoint(int row, int col, int direction)
    {
      int top = this.rows[row];
      int left = this.cols[col];

      DirectedPoint point = new DirectedPoint(row, col, left, top, direction);

      this.stoppingPoints.InsertLast(point);
      this.stoppingPointsByCol[col].InsertLast(point);
      this.stoppingPointsByRow[row].InsertLast(point);
    }
    public LinkedList<DirectedPoint> GetStoppingPointsByCol(int col)
    {
      if (col < 0 || col >= this.stoppingPointsByCol.Length)
        return null;

      return this.stoppingPointsByCol[col];
    }
    public LinkedList<DirectedPoint> GetStoppingPointsByRow(int row)
    {
      if (row < 0 || row >= this.stoppingPointsByRow.Length)
        return null;

      return this.stoppingPointsByRow[row];
    }
    private void AddTurningPoint(int row, int col, int direction)
    {
      int top = this.rows[row];
      int left = this.cols[col];

      DirectedPoint point = new DirectedPoint(row, col, left, top, direction);

      this.turningPoints.InsertLast(point);
      this.turningPointsByCol[col].InsertLast(point);
      this.turningPointsByRow[row].InsertLast(point);
    }
    public LinkedList<DirectedPoint> GetTurningPointsByCol(int col)
    {
      if (col < 0 || col >= this.turningPointsByCol.Length)
        return null;

      return this.turningPointsByCol[col];
    }
    public LinkedList<DirectedPoint> GetTurningPointsByRow(int row)
    {
      if (row < 0 || row >= this.turningPointsByRow.Length)
        return null;

      return this.turningPointsByRow[row];
    }
    private void AddCollectible(int left, int top)
    {
      Collectible collectible = new Collectible(left, top);

      this.collectibles.InsertLast(collectible);
    }
    public void Spawn(Form parent)
    {
      this.SpawnCollectibles();
      this.Parent = parent;
    }
    public void SpawnCollectibles()
    {
      var iterator = this.collectibles.Iterator();
      while (!iterator.Done)
        iterator.Next().Value.Spawn(this); // todo: rewrite the iterator so that I dont have to use .Value
    }
    public void ResetCollectibles()
    {
      this.collectibles = new LinkedList<Collectible>();
      for (int x = 0; x < this.cols[4] - 25; x += 25)
        this.AddCollectible(this.cols[0] + x, this.rows[0]);
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

      int r = Direction.RIGHT;
      int l = Direction.LEFT;
      int u = Direction.UP;
      int d = Direction.DOWN;
      int all = r | l | u | d;

      #region stopping-points
      this.stoppingPoints = new LinkedList<DirectedPoint>();

      this.stoppingPointsByCol = new LinkedList<DirectedPoint>[this.cols.Length];
      for (int col = 0; col < this.cols.Length; col++)
        this.stoppingPointsByCol[col] = new LinkedList<DirectedPoint>();

      this.stoppingPointsByRow = new LinkedList<DirectedPoint>[this.rows.Length];
      for (int row = 0; row < this.rows.Length; row++)
        this.stoppingPointsByRow[row] = new LinkedList<DirectedPoint>();

      this.AddStoppingPoint(0, 0, l | u); // 1
      this.AddStoppingPoint(0, 2, u);     // 2 
      this.AddStoppingPoint(0, 4, r | u); // 3
      this.AddStoppingPoint(0, 5, l | u); // 4
      this.AddStoppingPoint(0, 7, u);     // 5
      this.AddStoppingPoint(0, 9, u | r); // 6
      this.AddStoppingPoint(1, 0, l);     // 7
      this.AddStoppingPoint(1, 3, u);     // 8
      this.AddStoppingPoint(1, 4, d);     // 9
      this.AddStoppingPoint(1, 5, d);     // 10
      this.AddStoppingPoint(1, 6, u);     // 11
      this.AddStoppingPoint(1, 9, r);     // 12
      this.AddStoppingPoint(2, 0, d | l); // 13
      this.AddStoppingPoint(2, 2, r);     // 14
      this.AddStoppingPoint(2, 3, d | l); // 15
      this.AddStoppingPoint(2, 4, r | u); // 16
      this.AddStoppingPoint(2, 5, l | u); // 17
      this.AddStoppingPoint(2, 6, d | r); // 18
      this.AddStoppingPoint(2, 7, l);     // 19
      this.AddStoppingPoint(2, 9, r | d); // 20
      this.AddStoppingPoint(3, 3, l | u); // 21
      this.AddStoppingPoint(3, 4, d);     // 22
      this.AddStoppingPoint(3, 5, d);     // 23
      this.AddStoppingPoint(3, 6, r | u); // 24
      this.AddStoppingPoint(4, 2, l);     // 25
      this.AddStoppingPoint(4, 3, r);     // 26
      this.AddStoppingPoint(4, 6, l);     // 27
      this.AddStoppingPoint(4, 7, r);     // 28
      this.AddStoppingPoint(5, 3, l);     // 29
      this.AddStoppingPoint(5, 6, r);     // 30
      this.AddStoppingPoint(6, 0, l | u); // 31
      this.AddStoppingPoint(6, 3, d);     // 32
      this.AddStoppingPoint(6, 4, r | u); // 33
      this.AddStoppingPoint(6, 5, l | u); // 34
      this.AddStoppingPoint(6, 6, d);     // 35
      this.AddStoppingPoint(6, 9, r | u); // 36
      this.AddStoppingPoint(7, 0, d | l); // 37
      this.AddStoppingPoint(7, 1, r | u); // 38
      this.AddStoppingPoint(7, 2, l);     // 39
      this.AddStoppingPoint(7, 4, d);     // 40
      this.AddStoppingPoint(7, 5, d);     // 41
      this.AddStoppingPoint(7, 7, r);     // 42
      this.AddStoppingPoint(7, 8, l | u); // 43
      this.AddStoppingPoint(7, 9, r | d); // 44
      this.AddStoppingPoint(8, 0, l | u); // 45
      this.AddStoppingPoint(8, 1, d);     // 46
      this.AddStoppingPoint(8, 2, d | r); // 47
      this.AddStoppingPoint(8, 3, d | l); // 48
      this.AddStoppingPoint(8, 4, r | u); // 49
      this.AddStoppingPoint(8, 5, u | l); // 50
      this.AddStoppingPoint(8, 6, d | r); // 51
      this.AddStoppingPoint(8, 7, d | l); // 52
      this.AddStoppingPoint(8, 8, d);     // 53
      this.AddStoppingPoint(8, 9, r | u); // 54
      this.AddStoppingPoint(9, 0, l | d); // 55
      this.AddStoppingPoint(9, 9, r | d); // 56
      this.AddStoppingPoint(7, 3, u);     // 57
      this.AddStoppingPoint(7, 6, u);     // 58
      this.AddStoppingPoint(9, 4, d);     // 59
      this.AddStoppingPoint(9, 5, d);     // 60
      #endregion

      #region turning-points
      this.turningPoints = new LinkedList<DirectedPoint>();

      this.turningPointsByCol = new LinkedList<DirectedPoint>[this.cols.Length];
      for (int col = 0; col < this.cols.Length; col++)
        this.turningPointsByCol[col] = new LinkedList<DirectedPoint>();

      this.turningPointsByRow = new LinkedList<DirectedPoint>[this.rows.Length];
      for (int row = 0; row < this.rows.Length; row++)
        this.turningPointsByRow[row] = new LinkedList<DirectedPoint>();

      this.AddTurningPoint(0, 0, l | u);     // 1
      this.AddTurningPoint(0, 2, r | l | u); // 2
      this.AddTurningPoint(0, 4, r | u);     // 3
      this.AddTurningPoint(0, 5, l | u);     // 4
      this.AddTurningPoint(0, 7, l | r | u); // 5
      this.AddTurningPoint(0, 9, u | r);     // 6
      this.AddTurningPoint(1, 0, u | d | l); // 7
      this.AddTurningPoint(1, 2, all);       // 8
      this.AddTurningPoint(1, 3, l | r | u); // 9
      this.AddTurningPoint(1, 4, l | r | d); // 10
      this.AddTurningPoint(1, 5, l | r | d); // 11
      this.AddTurningPoint(1, 6, l | r | u); // 12
      this.AddTurningPoint(1, 7, all);       // 13
      this.AddTurningPoint(1, 9, r | u | d); // 14
      this.AddTurningPoint(2, 0, l | d);     // 15
      this.AddTurningPoint(2, 2, r | u | d); // 16
      this.AddTurningPoint(2, 3, l | d);     // 17
      this.AddTurningPoint(2, 4, r | u);     // 18
      this.AddTurningPoint(2, 5, l | u);     // 19
      this.AddTurningPoint(2, 6, r | d);     // 20
      this.AddTurningPoint(2, 7, l | u | d); // 21
      this.AddTurningPoint(2, 9, r | d);     // 22
      this.AddTurningPoint(3, 3, l | u);     // 23
      this.AddTurningPoint(3, 4, l | r | d); // 24
      this.AddTurningPoint(3, 5, l | r | d); // 25
      this.AddTurningPoint(3, 6, r | u);     // 26
      this.AddTurningPoint(4, 2, all);       // 27
      this.AddTurningPoint(4, 3, r | u | d); // 28
      this.AddTurningPoint(4, 6, l | u | d); // 29
      this.AddTurningPoint(4, 7, all);       // 30
      this.AddTurningPoint(5, 3, l | u | d); // 31
      this.AddTurningPoint(5, 6, r | u | d); // 32
      this.AddTurningPoint(6, 0, l | u);     // 33
      this.AddTurningPoint(6, 2, all);       // 34
      this.AddTurningPoint(6, 3, l | r | d); // 35
      this.AddTurningPoint(6, 4, r | u);     // 36
      this.AddTurningPoint(6, 5, l | u);     // 37
      this.AddTurningPoint(6, 6, l | r | d); // 38
      this.AddTurningPoint(6, 7, all);       // 39
      this.AddTurningPoint(6, 9, r | u);     // 40
      this.AddTurningPoint(7, 0, l | d);     // 41
      this.AddTurningPoint(7, 1, r | u);     // 42
      this.AddTurningPoint(7, 2, l | u | d); // 43
      this.AddTurningPoint(7, 3, l | r | u); // 44
      this.AddTurningPoint(7, 4, l | r | d); // 45
      this.AddTurningPoint(7, 5, l | r | d); // 46
      this.AddTurningPoint(7, 6, l | r | u); // 47
      this.AddTurningPoint(7, 7, r | u | d); // 48
      this.AddTurningPoint(7, 8, l | u);     // 49
      this.AddTurningPoint(7, 9, r | d);     // 50
      this.AddTurningPoint(8, 0, l | u);     // 51
      this.AddTurningPoint(8, 1, l | r | d); // 52
      this.AddTurningPoint(8, 2, r | d);     // 53
      this.AddTurningPoint(8, 3, l | d);     // 54
      this.AddTurningPoint(8, 4, r | u);     // 55
      this.AddTurningPoint(8, 5, l | u);     // 56
      this.AddTurningPoint(8, 6, r | d);     // 57
      this.AddTurningPoint(8, 7, l | d);     // 58
      this.AddTurningPoint(8, 8, l | r | d); // 59
      this.AddTurningPoint(8, 9, r | u);     // 60
      this.AddTurningPoint(9, 0, l | d);     // 61
      this.AddTurningPoint(9, 4, l | r | d); // 62
      this.AddTurningPoint(9, 5, l | r | d); // 63
      this.AddTurningPoint(9, 9, r | d);     // 64
      #endregion

      this.ResetCollectibles();
    }
  }
}
