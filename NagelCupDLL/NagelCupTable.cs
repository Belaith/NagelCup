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
        bool locked = false;

        public NagelCupTable()
        {
            InitializeComponent();
        }

        public NagelCupTable(Game game, bool locked) : this()
        {
            this.game = game;
            this.locked = locked;

            dataGridView.DataSource = new BindingSource(game.Players, null);

            if (!locked)
            {
                dataGridView.AllowUserToAddRows = true;
                dataGridView.AllowUserToDeleteRows = true;
            }

            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                if (!locked && (dc.Index.Equals(0) || dc.Index.Equals(1)))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }

            dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
        }

        public NagelCupTable(Round round, bool locked) : this()
        {
            this.round = round;
            this.locked = locked;

            dataGridView.DataSource = new BindingSource(round.Players, null);

            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                if (!locked && (dc.Index.Equals(3)))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }

            dataGridView.Sort(dataGridView.Columns[2], ListSortDirection.Ascending);
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                Player player = (Player)row.DataBoundItem;

                if (player != null && player.ID == -1)
                {
                    player.ID = findNextID();
                }
            }
        }

        private int findNextID()
        {
            int highest = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                Player player = (Player)row.DataBoundItem;

                if (player != null && player.ID > highest)
                {
                    highest = player.ID;
                }
            }

            return highest + 1;
        }
    }
}
