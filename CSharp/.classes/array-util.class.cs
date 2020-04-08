class A
{
  public static int[] Range(int low, int high)
  {
    int[] res = new int[high - low];

    for (int i = low; i < high; i++)
      res[i] = i;

    return res;
  }
  public static int[] Range(int high)
  {
    return A.Range(0, high);
  }
  public static T[] Swap<T>(T[] arr, int i, int j)
  {
    T temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;

    return arr;
  }
  public static T[] Shuffle<T>(T[] arr, Random rnd)
  {
    for (int i = 0; i < arr.Length; i++)
      A.Swap(arr, i, rnd.Next(0, arr.Length));

    return arr;
  }
  public static T[] Rep<T>(T[] arr, int times)
  {
    T[] res = new T[arr.Length * times];

    for (int i = 0; i < times; i++)
      for (int j = 0; j < arr.Length; j++)
        res[j + i * arr.Length] = arr[j];

    return res;
  }
  public static T[] Print<T>(T[] arr, string delimiter = " ", string after = "\n")
  {
    for (int i = 0; i < arr.Length; i++)
      Console.Write(arr[i].ToString() + (i == arr.Length - 1 ? "" : delimiter));

    Console.Write(after);

    return arr;
  }
  public static T[,] Print2D(T[,] matrix, string colDelimiter = " ", string rowDelimiter = "\n", string after = "\n")
  {
    for (int row = 0; row < matrix.Length; row++)
      for (int col = 0; col < matrix[row].Length; col++)
        Console.Write(arr[i, j].ToString() + (col == arr[row].Length - 1 ? rowDelimiter : colDelimiter));

    Console.Write(after);

    return matrix;
  }
}