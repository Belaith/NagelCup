using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NagelCup
{
    [Serializable]
    public class Round
    {
        private SortableList<Player> players = new SortableList<Player>();
        public SortableList<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }
        [XmlIgnore]
        public List<Player> SimpleListPlayers
        {
            get
            {
                return Players.ToList();
            }
            set
            {
                Players = new SortableList<Player>(value);
            }
        }

        public int ID { get; set; } = -1;

        private bool locked = false;
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                if (value != this.locked)
                {
                    this.locked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Round Copy()
        {
            Round newRound = new Round();

            newRound.ID = Round.DeepClone<int>(ID);
            newRound.Locked = Round.DeepClone<bool>(Locked);

            foreach (Player player in Players)
            {
                newRound.Players.Add(DeepClone<Player>(player));
            }

            return newRound;
        }

        private static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}