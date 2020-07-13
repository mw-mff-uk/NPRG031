class Settings
{
  private int monsters = 4;
  public int Monsters { get => this.monsters; }
  private double playerSpeed = 0.175;
  public double PlayerSpeed { get => this.playerSpeed; }
  private double monsterSpeed = 0.17;
  public double MonsterSpeed { get => this.monsterSpeed; }
  private int tickPeriod = 10;
  public int TickPeriod { get => this.tickPeriod; }
  private bool friendlyMode = false;
  public bool FriendlyMode { get => this.friendlyMode; }
}
