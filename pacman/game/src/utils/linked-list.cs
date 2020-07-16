using System;
using System.Text;

namespace MainNamespace
{
  class LinkedListItem<T>
  {
    private T value;
    public T Value { get => this.value; }
    public LinkedListItem<T> Next;
    public LinkedListItem<T> Prev;
    public LinkedListItem(T value, LinkedListItem<T> next = null, LinkedListItem<T> prev = null)
    {
      this.value = value;

      this.Next = next;
      this.Prev = prev;
    }
  }
  class LinkedList<T>
  {
    private LinkedListItem<T> first;
    public LinkedListItem<T> First { get => this.first; }
    private LinkedListItem<T> last;
    public LinkedListItem<T> Last { get => this.last; }
    private int length = 0;
    public int Length { get => this.length; }
    public bool Empty { get => this.length == 0; }
    public T RemoveItem(LinkedListItem<T> item)
    {
      if (this.Empty)
        throw new Exception("The linked list is empty");

      T value = item.Value;

      // First item
      if (item == this.first)
      {
        this.first = this.first.Next;

        if (this.first != null)
          this.first.Prev = null;
      }

      // Last item
      else if (item == this.last)
      {
        this.last = this.last.Prev;

        if (this.last != null)
          this.last.Next = null;
      }

      // Middle item
      else
      {
        item.Prev.Next = item.Next;
        item.Next.Prev = item.Prev;
      }

      // Remove references
      item.Prev = null;
      item.Next = null;

      // Adjust first and last items in special cases
      this.length--;
      if (this.length == 1)
        this.last = this.first;
      else if (this.length == 0)
        this.last = this.first = null;

      return value;
    }
    public T ExtractFirst() => this.RemoveItem(this.first);
    public T ExtractLast() => this.RemoveItem(this.last);
    public LinkedListItem<T> InsertLast(T value)
    {
      LinkedListItem<T> item = new LinkedListItem<T>(value, null, this.last);

      if (this.Empty)
        this.first = item;

      if (this.last != null)
        this.last.Next = item;

      this.last = item;

      this.length++;
      return item;
    }
    public LinkedListItem<T> InsertFirst(T value)
    {
      this.first = new LinkedListItem<T>(value, this.first, null);

      if (this.last == null)
        this.last = this.first;

      this.length++;
      return this.first;
    }
    public LinkedListIterator<T> Iterator() => new LinkedListIterator<T>(this.first);
    public void DetailedPrint()
    {
      var item = this.first;
      while (item != null)
      {
        Console.WriteLine(
          "{0} <-- {1} --> {2}",
          item.Prev == null ? "-" : item.Prev.Value.ToString(),
          item.Value.ToString(),
          item.Next == null ? "-" : item.Next.Value.ToString()
        );
        item = item.Next;
      }
    }
    public override string ToString()
    {
      var sb = new StringBuilder();
      var iterator = this.Iterator();

      sb.Append("{ ");
      while (!iterator.Done)
        sb.Append(iterator.Next().Value.ToString() + (iterator.Done ? "" : ", "));
      sb.Append(" }");

      return sb.ToString();
    }
    public bool ContainsValue(T value)
    {
      var iterator = this.Iterator();
      while (!iterator.Done)
        if (Object.Equals(iterator.Next().Value, value))
          return true;

      return false;
    }
    public void Wipe()
    {
      this.first = null;
      this.length = 0;
    }
    public LinkedList(params T[] args)
    {
      for (int i = 0; i < args.Length; i++)
        this.InsertLast(args[i]);
    }
  }
  class LinkedListIterator<T>
  {
    private LinkedListItem<T> cursor;
    private LinkedListItem<T> first;
    private int iterations;
    public int Iterations { get => this.iterations; }
    public bool Done { get => this.cursor == null; }
    public LinkedListItem<T> Next()
    {
      LinkedListItem<T> temp = this.cursor;
      this.cursor = this.cursor.Next;

      this.iterations++;
      return temp;
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
}
