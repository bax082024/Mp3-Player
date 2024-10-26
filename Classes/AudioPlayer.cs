using System;
using System.IO;
using System.Runtime.CompilerServices;
using NAudio.Wave;

class AudioPlayer
{
  private WaveOutEvent? outputDevice;
  private AudioFileReader? audioFile;
  private string[] playlist;
  private int currentTrackIndex;

  public AudioPlayer(string folderPath) 
  {
    playlist = Directory.GetFiles(folderPath, "*.mp3");
  }

  public void Play()
  {
    if (playlist.Length == 0)
    {
      Console.WriteLine("No Mp3 Files Found.");
      return;
    }

    PlayTrack(currentTrackIndex);
  }

  private void PlayTrack(int trackIndex)
  {
    if (outputDevice != null)
    {
      outputDevice.Dispose();
      audioFile.Dispose();
    }

    audioFile = new AudioFileReader(playlist[trackIndex]);
    outputDevice = new WaveOutEvent();
    outputDevice.Init(audioFile);
    outputDevice.Play();

    Console.WriteLine($"Playing: {Path.GetFileName(playlist[trackIndex])}");
    outputDevice.PlaybackStopped += OnPlaybackStopped;
  }

  private void OnPlaybackStopped(object sender, StoppedEventArgs e)
  {
    currentTrackIndex = (currentTrackIndex + 1) % playlist.Length;
    PlayTrack(currentTrackIndex);
  }

  public void Stop()
  {
    outputDevice?.Stop();
  }


  
}