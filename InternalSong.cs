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

        public object[] GrabNote(string beat)
        {

            return South.GrabNote(beat);
        }
    }

    
}