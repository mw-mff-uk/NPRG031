using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class FpsTracker : Label
  {
    private const double FPS_HISTORY = 1000.0;
    private LinkedList<double> ticks;
    double fps = 0.0;
    public void Update(double elapsed)
    {
      int count = 0;
      double duration = 0.0;

      this.ticks.InsertFirst(elapsed);

      var iterator = this.ticks.Iterator();
      while (!iterator.Done)
      {
        count++;
        LinkedListItem<double> item = iterator.Next();

        if (elapsed - item.Value > FPS_HISTORY)
        {
          item.Next = null;
          break;
        }

        duration = Math.Max(duration, elapsed - item.Value);
      }

      this.Text = (int)Math.Round(((double)count / duration) * 1000) + " FPS";
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public FpsTracker(int left, int top)
    {
      this.Left = left;
      this.Top = top;

      this.Width = 200;
      this.Height = 20;

      this.Text = "?? FPS";

      this.Font = new Font("monospace", 16, FontStyle.Bold);
      this.ForeColor = Color.White;

      this.ticks = new LinkedList<double>();
    }
  }
}
