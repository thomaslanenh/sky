using SKY.Content.Songs;

namespace SKY
{
    public class InternalSong
    {
        public Southern South = new();

        public string LoadSong(string songName)
        {
            return "Hi";
        }

        public (int Note, int X, int Y) GrabNote(string beat)
        {

            return South.GrabNote(beat);
        }
    }

    
}