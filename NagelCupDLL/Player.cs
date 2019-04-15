using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NagelCup
{
    [Serializable]
    public class Player : INotifyPropertyChanged
    {
        private string name;
        [System.ComponentModel.DisplayName("Name")]
        public string Name {
            get
            {
                return name;
            }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int id = -1;
        [System.ComponentModel.DisplayName("Nummer")]
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    //NotifyPropertyChanged();
                }
            }
        }

        private string chunk;
        [System.ComponentModel.DisplayName("Klotz")]
        public string Chunk
        {
            get
            {
                return chunk;
            }
            set
            {
                if (value != this.chunk)
                {
                    this.chunk = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool alive = true;
        [System.ComponentModel.DisplayName("Noch im Spiel")]
        public bool Alive
        {
            get
            {
                return alive;
            }
            set
            {
                if (value != this.alive)
                {
                    this.alive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string seedChunk;
        [System.ComponentModel.DisplayName("Fester Klotz")]
        public string SeedChunk
        {
            get
            {
                return seedChunk;
            }
            set
            {
                if (value != this.seedChunk)
                {
                    this.seedChunk = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
