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
  private bool isPaused;
  private bool isSwitchingTracks = false;

  public AudioPlayer(string? folderPath) 
  {
    if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
    {
      throw new ArgumentException("Invalid folder path");
    }
    playlist = Directory.GetFiles(folderPath, "*.mp3"); 
    currentTrackIndex = 0;
    isPaused = false;
  }

  public void Play()
  {
    if (playlist.Length == 0)
    {
      Console.WriteLine("No Mp3 Files Found.");
      return;
    }
    if (isPaused)
    {
      Resume();
    }
    else
    {
      PlayTrack(currentTrackIndex);
    }

    
  }

  private void PlayTrack(int trackIndex)
  {
    if (outputDevice != null)
    {
      outputDevice.Dispose(); 
    }
    if (audioFile != null)
    {
      audioFile.Dispose();
    }

    audioFile = new AudioFileReader(playlist[trackIndex]);
    outputDevice = new WaveOutEvent();
    outputDevice.Init(audioFile);
    outputDevice.Play();

    Console.WriteLine($"Playing: {Path.GetFileName(playlist[trackIndex])}");
    outputDevice.PlaybackStopped += OnPlaybackStopped;
  }

  public void Pause()
  {
    if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
    {
      outputDevice.Pause();
      isPaused = true;
      Console.WriteLine("Paused");
    }
  }

  public void Resume()
  {
    if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
    {
      outputDevice.Play();
      isPaused = false;
      Console.WriteLine("Resume");
    }
  }

  public void Stop()
  {
    outputDevice?.Stop();
    isPaused = false;
  }

  public void NextTrack()
  {
    isSwitchingTracks = true;
    currentTrackIndex = (currentTrackIndex + 1) % playlist.Length;
    PlayTrack(currentTrackIndex);
    isSwitchingTracks = false;
  }

  public void PreviousTrack()
  {
    isSwitchingTracks = true;
    currentTrackIndex = (currentTrackIndex - 1 + playlist.Length) % playlist.Length;
    PlayTrack(currentTrackIndex);
    isSwitchingTracks = false;
  }

  private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
  {
    if (!isSwitchingTracks)
    { 
      currentTrackIndex = (currentTrackIndex + 1) % playlist.Length;
      PlayTrack(currentTrackIndex);
    }
  }

  


  
}