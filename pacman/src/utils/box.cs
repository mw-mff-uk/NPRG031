namespace MainNamespace
{
  class Box
  {
    public int Width;
    public int Height;
    public int Left;
    public int Top;
    public int YCenter { get => this.Top + this.Height / 2; }
    public int XCenter { get => this.Left + this.Width / 2; }
    public Box(int width, int height, int left, int top)
    {
      this.Width = width;
      this.Height = height;
      this.Left = left;
      this.Top = top;
    }
  }
}
