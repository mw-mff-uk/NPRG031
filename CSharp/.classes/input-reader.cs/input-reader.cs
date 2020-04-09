class InputReader
{
  class Char
  {
    private int _value;
    public int Value { get => this._value; }
    public bool NewLine { get => this._value == '\n'; }
    public bool IsEmpty { get => (this._value == ' ' || this._value == '\n' || this._value == '\t' || this._value == '\r' || this._value == '\v'); }
    public bool IsNum { get => (this._value >= '0' && this._value <= '9'); }
    public bool IsAlph { get => ((this._value >= 'a' && this._value <= 'z') || (this._value >= 'A' && this._value <= 'Z')); }
    public bool EOF { get => this._value == -1; }
    public static bool operator ==(Char ch1, Char ch2) => ch1.Value == ch2.Value;
    public static bool operator ==(Char ch1, int ch2) => ch1.Value == ch2;
    public static bool operator !=(Char ch1, Char ch2) => ch1.Value != ch2.Value;
    public static bool operator !=(Char ch1, int ch2) => ch1.Value != ch2;
    public override bool Equals(object obj) => (((Char)obj).Value == this.Value);
    public override int GetHashCode() => this.Value;
    public Char(int value)
    {
      this._value = value;
    }
  }
  private Char[] _buffer;
  private int _cursor;
  private bool _EOF;
  public bool EOF { get { return this._EOF; } }
  private Char GetChar()
  {
    if (this._cursor > 0)
    {
      Char ch = this._buffer[--this._cursor];
      return ch;
    }

    return new Char(Console.Read());
  }
  private void ReturnChar(Char ch)
  {
    this._buffer[this._cursor++] = ch;
  }
  public string ReadLine()
  {
    Char ch;
    StringBuilder sb = new StringBuilder();

    // Skip empty symbols
    while ((ch = this.GetChar()).IsEmpty) ;

    // Check for EOF
    if (ch.EOF)
    {
      this._EOF = true;
      return "";
    }

    while (!(ch.NewLine || ch.EOF))
    {
      sb.Append((char)ch.Value);
      ch = this.GetChar();
    }

    if (ch.EOF)
      this._EOF = true;
    else
      this.ReturnChar(ch);

    return sb.ToString();
  }
  public string ReadWord()
  {
    Char ch;
    StringBuilder sb = new StringBuilder();

    // Skip empty symbols
    while ((ch = this.GetChar()).IsEmpty) ;

    // Check for EOF
    if (ch.EOF)
    {
      this._EOF = true;
      return "";
    }

    // Build the string
    while (ch.IsAlph || ch.IsNum)
    {
      sb.Append((char)ch.Value);
      ch = this.GetChar();
    }

    // Check for EOF
    if (ch.EOF)
      this._EOF = true;
    else
      this.ReturnChar(ch);

    return sb.ToString();
  }
  public string ReadRest()
  {
    Char ch;
    StringBuilder sb = new StringBuilder();

    while (true)
    {
      ch = this.GetChar();

      if (ch.EOF)
        break;

      if (!ch.IsEmpty)
        sb.Append((char)ch.Value);
    }

    return sb.ToString();
  }
  public double ReadFloat()
  {
    Char ch;
    double x = 0.0;
    int div = 1;
    int sign = 1;

    // Skip empty symbols
    while ((ch = this.GetChar()).IsEmpty) ;

    // Check for EOF
    if (ch.EOF)
    {
      this._EOF = true;
      return 0.0;
    }

    // Check for sign
    if (ch == '+' || ch == '-')
    {
      if (ch == '-')
        sign = -1;

      ch = this.GetChar();
    }

    // Keep reading numbers
    while (ch.IsNum)
    {
      x = x * 10 + ch.Value - '0';
      ch = this.GetChar();
    }

    // Check for decimal symbol (dot, comma)
    if (ch == '.' || ch == ',')
    {
      while ((ch = this.GetChar()).IsNum)
      {
        x = x * 10 + ch.Value - '0';
        div *= 10;
      }
    }

    // Check for EOF
    if (ch.EOF)
      this._EOF = true;
    else
      this.ReturnChar(ch);

    return sign * x / div;
  }
  public int ReadInt()
  {
    return (int)this.ReadFloat();
  }
  public InputReader()
  {
    this._buffer = new Char[100];
    this._cursor = 0;
  }
}