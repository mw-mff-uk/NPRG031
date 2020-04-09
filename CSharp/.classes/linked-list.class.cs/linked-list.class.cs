class LinkedListItem<T>
{
  private T value;
  public T Value { get => this.value; }
  public LinkedListItem<T> next;
  public LinkedListItem(T value, LinkedListItem<T> next = null)
  {
    this.value = value;
    this.next = next;
  }
}
class LinkedList<T>
{
  private LinkedListItem<T> first;
  private LinkedListItem<T> last;
  private int length = 0;
  public int Length { get => this.length; }
  public bool Empty { get => this.length == 0; }
  public LinkedList<T> Append(T value)
  {
    LinkedListItem<T> item = new LinkedListItem<T>(value);

    if (this.first == null)
      this.first = item;

    if (this.last != null)
      this.last.next = item;

    this.last = item;

    this.length++;
    return this;
  }
  public LinkedList<T> Preppend(T value)
  {
    this.first = new LinkedListItem<T>(value, this.first);

    if (this.last == null)
      this.last = this.first;

    this.length++;
    return this;
  }
  public T ExtractFirst()
  {
    if (this.Empty)
      throw new Exception("The linked list is empty");

    T value = this.first.Value;
    this.first = this.first.next;

    this.length--;
    return value;
  }
  public T ExtractLast()
  {
    if (this.Empty)
      throw new Exception("The linked list is empty");

    T value = this.first.Value;

    if (this.Length == 1)
      this.first = this.last = null;
    else
      this.first = this.first.next;

    this.length--;
    return value;
  }
  public LinkedListIterator<T> Iterator() => new LinkedListIterator<T>(this.first);
  public LinkedList(params T[] args)
  {
    for (int i = 0; i < args.Length; i++)
      this.Append(args[i]);
  }
}
class LinkedListIterator<T>
{
  private LinkedListItem<T> cursor;
  private LinkedListItem<T> first;
  private int iterations;
  public int Iterations { get => this.iterations; }
  public bool Done { get => this.cursor == null; }
  public T Next()
  {
    LinkedListItem<T> temp = this.cursor;
    this.cursor = this.cursor.next;

    this.iterations++;
    return temp.Value;
  }
  public LinkedListIterator<T> Reset()
  {
    this.cursor = this.first;
    this.iterations = 0;

    return this;
  }
  public LinkedListIterator(LinkedListItem<T> first)
  {
    this.first = first;
    this.Reset();
  }
}