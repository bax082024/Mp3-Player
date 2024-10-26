class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Enter music folder path:");
    string folderPath = Console.ReadLine();

    AudioPlayer player = new AudioPlayer(folderPath);

    Console.WriteLine("Press 'P' to play, 'S' to stop, or 'Q' to quit");

    while (true)
    {
      var key = Console.ReadKey(true).Key;

      if(key == ConsoleKey.P)
      {
        player.Play();
      }
      else if (key == ConsoleKey.S)
      {
        player.Stop();
      }
      else if (key == ConsoleKey.Q)
      {
        player.Stop();
        break;
      }
    }
  }
}