using System;
using System.IO;
using NAudio.Wave;

class AudioPlayer
{
  private WaveOutEvent outputDevice;
  private AudioFileReader audioFile;
  private string[] playlist;
  private int currentTrackIndex;

  public AudioPlayer(string folderPath) 
  {
    playlist = Directory.GetFiles(folderPath, "*.mp3")
  }
}