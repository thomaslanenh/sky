using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SKY.Content.Songs
{
    public class Southern
    {
        private string SongTitle = "FkIt";
        private string Artist = "And Then";
        private float BPM = 120f;
        private string Genre = "Techno";


        List<ChartMap> Gems = new List<ChartMap>() { new ChartMap() {
            Beat = "0:02",
            Note = 2,
            loc = new Point(200,400),
            PowerUp = false
        }, new ChartMap() {
            Beat = "0:05",
            Note = 1,
            loc = new Point(100, 400),
            PowerUp = false
        }, new ChartMap() {
            Beat = "0:08",
            Note = 2,
            loc = new Point(200, 200),
            PowerUp = false
        },
        new ChartMap() {
            Beat = "0:12",
            Note = 1,
            loc = new Point(200, 400),
            PowerUp = false
        }};


        public (int Note, int X, int Y) GrabNote(string beat)
        {
            var found = Gems.FirstOrDefault(gem => gem.Beat == beat);
            if (found != null)
            {
                return (found.Note, found.loc.X, found.loc.Y);
            }
            else
            {
                return (0, 0, 0);
            }
           
        }
    }


}
