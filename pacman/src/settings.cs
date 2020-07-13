class Settings
{
  private int monsters = 4;
  public int Monsters { get => this.monsters; }
  private int playerSpeed = 4;
  public int PlayerSpeed { get => this.playerSpeed; }
  private int monsterSpeed = 4;
  public int MonsterSpeed { get => this.monsterSpeed; }
  private int tickPeriod = 13;
  public int TickPeriod { get => this.tickPeriod; }
  private bool friendlyMode = false;
  public bool FriendlyMode { get => this.friendlyMode; }
}
