class XY
{
  public int x;
  public int y;
  override public string ToString() => "(" + this.x + "," + this.y + ")";
  public static bool operator ==(XY a, XY b) => (a.x == b.x && a.y == b.y);
  public static bool operator !=(XY a, XY b) => (a.x != b.x || a.y != b.y);
  public XY(int x, int y)
  {
    this.x = x;
    this.y = y;
  }
}