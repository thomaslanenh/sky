using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKY.Content.Songs
{
    internal class ChartMap
    {

        public string Beat { get; set; }
        public int Note { get; set; }
        public Point loc { get; set; }
        public int pointValue { get; set; }

        public bool PowerUp { get; set; }
    }
}
