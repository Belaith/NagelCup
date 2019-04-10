using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagelCup
{
    public class Game
    {
        public SortableList<Player> Players { get; set; } = new SortableList<Player>();
        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}
