using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagelCup
{
    public class Chunk
    {
        public int ChunkID { get; private set; } = 0;
        public List<Player> Players { get; private set; } = new List<Player>();
    }
}
