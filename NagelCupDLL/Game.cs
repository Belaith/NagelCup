using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NagelCup
{
    [Serializable]
    public class Game
    {
        private static Random random = new Random();

        public SortableList<Player> Players { get; set; } = new SortableList<Player>();
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

        public List<Round> Rounds { get; set; } = new List<Round>();

        [XmlIgnore]
        public Round CurrentRound { get; set; }

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

        public void Init()
        {
            if (CurrentRound == null && Players.Any(x=>x.Chunk != null))
            {
                CurrentRound = new Round();

                CurrentRound.ID = Rounds.Count() + 1;

                CurrentRound.Locked = false;
                CurrentRound.Players = new SortableList<Player>();
                foreach (Player player in Players.Where(x=> !string.IsNullOrEmpty(x.Chunk)))
                {
                    CurrentRound.Players.Add(player);
                }
            }
        }

        public void NextRound(int chunks)
        {
            Locked = true;

            if (CurrentRound != null)
            {
                Rounds.Add(CurrentRound.Copy());
            }
            Rounds.ForEach(x => x.Locked = true);
            
            foreach (Player player in Players)
            {
                player.Chunk = string.Empty;
            }

            CurrentRound = new Round();
            CurrentRound.ID = Rounds.Count() + 1;

            foreach (Player player in Players.Where(x => x.Alive && x.ID > 0))
            {
                CurrentRound.Players.Add(player);
            }

            List<List<Player>> chunksList = new List<List<Player>>();

            for (int i = 0; i < chunks; i++)
            {
                chunksList.Add(new List<Player>());
            }
            
            foreach (var player in CurrentRound.Players.Where(x => !string.IsNullOrEmpty(x.SeedChunk)))
            {
                if (int.TryParse(player.SeedChunk, out int chunk) && chunksList.Count() >= chunk)
                {
                    player.Chunk = chunk.ToString();
                    chunksList[chunk - 1].Add(player);
                }
            }

            foreach (var player in RandomPermutation(CurrentRound.Players.Where(x => string.IsNullOrEmpty(x.Chunk))))
            {
                List<Player> chunk = chunksList.FirstOrDefault(x => x.Count() == chunksList.Min(y => y.Count()));
                chunk.Add(player);
                player.Chunk = (chunksList.IndexOf(chunk) +1).ToString();
            }
        }

        private static IEnumerable<T> RandomPermutation<T>(IEnumerable<T> sequence)
        {
            T[] retArray = sequence.ToArray();


            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = random.Next(i, retArray.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
