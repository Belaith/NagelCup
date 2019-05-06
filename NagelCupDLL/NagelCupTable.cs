using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NagelCup
{
    public partial class NagelCupTable : UserControl
    {
        Game game;
        Round round;

        public NagelCupTable()
        {
            InitializeComponent();
        }

        public NagelCupTable(Game game) : this()
        {
            this.game = game;

            dataGridView.DataSource = new BindingSource(game.Players, null);

            if (!game.Locked)
            {
                dataGridView.AllowUserToAddRows = true;
                dataGridView.AllowUserToDeleteRows = true;
            }

            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                if (!game.Locked && (dc.Index.Equals(0) || dc.Index.Equals(1) || dc.Index.Equals(4)))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }

            dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
            countPlayers();
            game.PropertyChanged += locked_PropertyChanged;
        }

        public NagelCupTable(Round round) : this()
        {
            this.round = round;

            dataGridView.DataSource = new BindingSource(round.Players, null);

            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                if (!round.Locked && dc.Index.Equals(3))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }

                if (dc.Index.Equals(4))
                {
                    dc.Visible = false;
                }
            }

            dataGridView.Sort(dataGridView.Columns[2], ListSortDirection.Ascending);
            countPlayers();
            round.PropertyChanged += locked_PropertyChanged;
        }

        private void locked_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool? locked = game?.Locked ?? round?.Locked;

            if ((locked ?? false) == true)
            {
                foreach (DataGridViewColumn dc in dataGridView.Columns)
                {
                    dc.ReadOnly = true;
                }
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;

                if (game != null)
                {
                    game.PropertyChanged -= locked_PropertyChanged;
                }
                if (round != null)
                {
                    round.PropertyChanged -= locked_PropertyChanged;
                }
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            countPlayers();
        }

        private void countPlayers()
        {
            SortableList<Player> players = game?.Players ?? round?.Players;

            int allPlayers = players.Count;
            int activePlayers = players.Count(x => x.Alive && x.ID > 0);

            lblCount.Text = $"Anzahl Spieler insgeamt: {allPlayers}";
            lblCountAll.Text = $"Anzahl Spieler noch aktiv: {activePlayers}";
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.CancelEdit();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && dataGridView.Columns[e.ColumnIndex].ReadOnly == false)
            {
                Player player = (Player)dataGridView.Rows[e.RowIndex].DataBoundItem;
                if (player != null)
                {
                    player.Alive = !player.Alive;
                }
            }
            dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            countPlayers();
        }

        private void dataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = game.Players.Max(x => x.ID ?? 0) + 1;
        }
    }
}
