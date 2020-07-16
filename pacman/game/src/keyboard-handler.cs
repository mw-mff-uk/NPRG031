using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace MainNamespace
{
  class KeyboardHandler : TextBox
  {
    const int KEY_PRESSED_DURATION = 400;
    private Keys activeKey;
    public Keys ActiveKey { get => this.activeKey; }
    private DateTime pressedTimestamp;
    private int msSincePressed { get => (int)(DateTime.Now - this.pressedTimestamp).TotalMilliseconds; }
    public bool IsPressed { get => this.msSincePressed < KeyboardHandler.KEY_PRESSED_DURATION; }
    public void Spawn(Form parent)
    {
      this.Parent = parent;
    }
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
      this.activeKey = e.KeyCode;
      this.pressedTimestamp = DateTime.Now;
    }
    public KeyboardHandler()
    {
      this.BackColor = Color.FromArgb(10, 5, 2);
      this.Top = 9999;
      this.Left = 9999;
      this.Width = 0;
      this.Height = 0;
      this.KeyDown += this.OnKeyDown;
    }
  }
}
