using System;
using System.IO;

namespace MainNamespace
{
  class Highscore
  {
    private string path = "/home/wiki/School/NPRG031/pacman/data/highscore.txt";
    private int[] scores;
    public int First { get => this.scores[0]; set { this.scores[0] = value; } }
    public int Second { get => this.scores[1]; set { this.scores[1] = value; } }
    public int Third { get => this.scores[2]; set { this.scores[2] = value; } }
    public string Text { get => String.Format("HIGH SCORE\n\n1. {0}\n2. {1}\n3. {2}", this.First, this.Second, this.Third); }
    public void Submit(int score)
    {
      if (score > this.Third)
      {
        if (score > this.First)
        {
          this.Third = this.Second;
          this.Second = this.First;
          this.First = score;
        }
        else if (score > this.Second)
        {
          this.Third = this.Second;
          this.Second = score;
        }
        else
        {
          this.Third = score;
        }

        File.WriteAllText(this.path, String.Join("\n", this.scores));
      }
    }
    public Highscore()
    {
      this.scores = new int[3];

      int cursor = 0;
      foreach (string line in File.ReadAllLines(this.path))
        this.scores[cursor++] = Convert.ToInt32(line);
    }
  }
}
