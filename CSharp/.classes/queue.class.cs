class Queue<T>
{
  class QueueItem<T>
  {
    public T value;
    public QueueItem<T> next;
    public QueueItem(T value, QueueItem<T> next = null)
    {
      this.value = value;
      this.next = next;
    }
  }
  private QueueItem<T> _first;
  private QueueItem<T> _last;
  private int length = 0;
  public int Length { get => this.length; }
  public bool CanDequeue { get => this.length > 0; }
  public Queue<T> Enqueue(T value)
  {
    QueueItem<T> item = new QueueItem<T>(value);

    if (this._first == null)
      this._first = item;

    if (this._last != null)
      this._last.next = item;

    this._last = item;

    this.length++;
    return this;
  }
  public T Dequeue()
  {
    if (!this.CanDequeue)
      throw new Exception("The queue is empty");

    T value = this._first.value;

    if (this.Length == 1)
      this._first = this._last = null;
    else
      this._first = this._first.next;

    this.length--;
    return value;
  }
  public Queue(T[] items = null)
  {
    if (items != null)
      for (int i = 0; i < items.Length; i++)
        this.Enqueue(items[i]);
  }
}