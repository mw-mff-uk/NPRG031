using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class SpeedTracker : Label
  {
    class SpeedTrackerRecord
    {
      public double Elapsed;
      public double Distance;
      public SpeedTrackerRecord(double elapsed, double distance)
      {
        this.Elapsed = elapsed;
        this.Distance = distance;
      }
    }
    private const double SPEED_HISTORY = 1000.0;
    private LinkedList<SpeedTrackerRecord> history;
    public void Update(double elapsed, double distance)
    {
      double duration = 0.0;
      double distanceTotal = 0.0;

      this.history.InsertFirst(new SpeedTrackerRecord(elapsed, distance));

      var iterator = this.history.Iterator();
      while (!iterator.Done)
      {
        LinkedListItem<SpeedTrackerRecord> item = iterator.Next();

        distanceTotal += item.Value.Distance;

        if (elapsed - item.Value.Elapsed > SPEED_HISTORY)
        {
          item.Next = null;
          break;
        }

        duration = Math.Max(duration, elapsed - item.Value.Elapsed);
      }

      this.Text = (int)Math.Round((distanceTotal / duration) * 1000) + " px/s";
    }
    public void Spawn(Control parent)
    {
      this.Parent = parent;
    }
    public SpeedTracker(int left, int top)
    {
      this.Left = left;
      this.Top = top;

      this.Width = 150;
      this.Height = 20;

      this.Text = "?? px/s";

      this.Font = new Font("monospace", 16, FontStyle.Bold);
      this.ForeColor = Color.White;

      this.history = new LinkedList<SpeedTrackerRecord>();
    }
  }
}
