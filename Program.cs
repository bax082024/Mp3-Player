class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Enter music folder path:");
    string? folderPath = Console.ReadLine();

    if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
    {
      Console.WriteLine("Invalid folder path.");
    }

    AudioPlayer player = new AudioPlayer(folderPath);

    Console.WriteLine("Press 'P' to play/resume, 'S' to stop, 'A' to pause, 'B' for previous track, 'Q' to quit");

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
      else if (key == ConsoleKey.A)
      {
        player.Pause();
      }
      else if (key == ConsoleKey.N)
      {
        player.NextTrack();
      }
      else if (key == ConsoleKey.B)
      {
        player.PreviousTrack();
      }
      else if (key == ConsoleKey.Q)
      {
        player.Stop();
        break;
      }
    }
  }
}