using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NagelCup
{
    public class Player
    {
        [System.ComponentModel.DisplayName("Name")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Nummer")]
        public int ID { get; set; } = -1;
        [System.ComponentModel.DisplayName("Klotz")]
        public string Chunk { get; set; }
        [System.ComponentModel.DisplayName("Noch im Spiel")]
        public bool Alive { get; set; } = true;

        public Player()
        {
        }

        public Player(string name, int id, string chunk, bool alive)
        {
            this.Name = name;
            this.ID = id;
            this.Chunk = chunk;
            this.Alive = alive;
        }
    }
}
