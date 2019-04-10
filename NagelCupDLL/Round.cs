using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagelCup
{
    public class Round
    {
        public int RoundID { get; set; } = 0;
        public SortableList<Player> Players { get; set; } = new SortableList<Player>();
        public List<Chunk> Chunks { get; set; } = new List<Chunk>();
    }
}
