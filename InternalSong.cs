using SKY.Content.Songs;
using System;
using System.Diagnostics;

namespace SKY
{
    public class InternalSong
    {
        public string CurrentSong { get; set; }
        public string LastSong { get; set; }
        public void LoadSong(string songName)
        {
            switch (songName)
            {
                case "Southern":
                    Debug.WriteLine("Southern Loading.");
                    LoadedSong?.Invoke();
                    break;
                default:
                    LoadedSong?.Invoke();
                    Debug.WriteLine("Song Load Error");
                    break;
            }
        }

        public (int Note, int X, int Y) GrabNote(string beat)
        {
            Southern South = new();
            return South.GrabNote(beat);
        }

        public event Action LoadedSong;

    }

    
}