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

        Queue<ChartMap> charts = new Queue<ChartMap>();

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

        public Southern (){
            charts.Enqueue(new ChartMap() {
                Beat = "0:02",
                Note = 2,
                loc = new Point(200, 400),
                PowerUp = false
            });
            charts.Enqueue(new ChartMap()
            {
                Beat = "0:05",
                Note = 1,
                loc = new Point(100, 400),
                PowerUp = false
            });
            charts.Enqueue(new ChartMap()
            {
                Beat = "0:08",
                Note = 2,
                loc = new Point(200, 200),
                PowerUp = false
            });
            charts.Enqueue(new ChartMap()
            {
                Beat = "0:12",
                Note = 1,
                loc = new Point(200, 400),
                PowerUp = false
            });
        }

        public (int Note, int X, int Y) GrabCurrentNote (string beat)
        {
            if (charts.Peek().Beat == beat)
            {
                // need to find a way to peak at the element on top, render it until the time changes then dequeue it
                var block = charts.Dequeue();
                return (block.Note, block.loc.X, block.loc.Y);
            }
            else
            {
                return (0,0,0);
            }
        }

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
