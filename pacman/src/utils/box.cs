namespace MainNamespace
{
  class Box
  {
    private int width;
    public int Width { get => this.width; }
    private int height;
    public int Height { get => this.height; }
    private int left;
    public int Left { get => this.left; }
    private int top;
    public int Top { get => this.top; }
    public int YCenter { get => this.top + this.height / 2; }
    public int XCenter { get => this.left + this.width / 2; }
    public Box(int width, int height, int left, int top)
    {
      this.width = width;
      this.height = height;
      this.left = left;
      this.top = top;
    }
  }
}
