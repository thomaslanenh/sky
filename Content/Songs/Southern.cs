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


        public object[] GrabNote(string beat)
        {
            
            if (Gems.FirstOrDefault(gem => gem.Beat == beat) != null)
            {
                ChartMap FoundBeat = new ChartMap();
                FoundBeat = Gems.FirstOrDefault(gem => gem.Beat == beat);

                
                return new object[] { FoundBeat.Note, FoundBeat.loc.X, FoundBeat.loc.Y };
            }
            else
            {
                return new object[] {0, new Point(0,0)};
            }
        }
    }


}
