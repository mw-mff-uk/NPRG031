namespace MainNamespace
{
  class Box
  {
    public double Width;
    public double Height;
    public double Left;
    public double Right { get => this.Left + this.Width; }
    public double Top;
    public double Bottom { get => this.Top + this.Height; }
    public double YCenter { get => this.Top + this.Height / 2; }
    public double XCenter { get => this.Left + this.Width / 2; }
    public Box(double width, double height, double left, double top)
    {
      this.Width = width;
      this.Height = height;
      this.Left = left;
      this.Top = top;
    }
  }
}
