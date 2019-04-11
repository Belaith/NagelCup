using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NagelCup
{
    public partial class MainWindow : Form
    {
        string path = "";
        Game game;

        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            game = new Game() { Locked = true };

            game.Players = new SortableList<Player>() { new Player("Till", 1, "", true) };
            game.Rounds = new List<Round>() { new Round() { Players = new SortableList<Player>() { new Player("Till", 1, "1", true)}, Locked = true } };
            game.Rounds.Add(new Round() { Players = new SortableList<Player>() { new Player("Till", 1, "1", true) }, Locked = true });
            game.CurrentRound = new Round() { Players = new SortableList<Player>() { new Player("Till", 1, "1", true) } };

            NagelCupTable table = new NagelCupTable(game);
            table.Dock = DockStyle.Fill;

            TabPage tabPage1 = new TabPage();
            tabPage1.Name = "tabPageTeilnehmer";
            tabPage1.Text = "Teilnehmer";
            tabPage1.Controls.Add(table);
            tabControl.TabPages.Add(tabPage1);

            int rounds = 0;
            foreach (var round in game.Rounds)
            {
                NagelCupTable tableRounds = new NagelCupTable(round);
                tableRounds.Dock = DockStyle.Fill;

                TabPage tabPage = new TabPage();
                tabPage.Name = "tabPageRound1";
                tabPage.Text = "Runde " + ++rounds;
                tabPage.Controls.Add(tableRounds);
                tabControl.TabPages.Add(tabPage);
            }
#endif
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    txtFilePath.Text = openFileDialog.FileName;

                    tabControl.TabPages.Clear();

                    //TODO file existiert, einlesen

                    //Teilnehmer und alte Chunks -> Tabs

                    game.Init();

                    NagelCupTable tableRound = new NagelCupTable(game.CurrentRound);
                    tableRound.Dock = DockStyle.Fill;

                    TabPage tabPage = new TabPage();
                    tabPage.Name = $"tabPageRound {game.Rounds.Count + 1}";
                    tabPage.Text = $"Runde {game.Rounds.Count + 1}";
                    tabPage.Controls.Add(tableRound);
                    tabControl.TabPages.Add(tabPage);

                    tabControl.SelectTab(tabControl.TabPages.Count - 1);
                }
                else
                {
                    if (MessageBox.Show("Soll eine neue Datei erzeugt werden?", "Datei nicht gefunden!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        txtFilePath.Text = openFileDialog.FileName;

                        tabControl.TabPages.Clear();

                        //TODO file existiert nochnicht, anlegen

                        game = new Game();

                        NagelCupTable table = new NagelCupTable(game);
                        table.Dock = DockStyle.Fill;

                        TabPage tabPage = new TabPage();
                        tabPage.Name = "tabPageTeilnehmer";
                        tabPage.Text = "Teilnehmer";
                        tabPage.Controls.Add(table);
                        tabControl.TabPages.Add(tabPage);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            game.Players.ListChanged += Players_ListChanged;

        }

        private void Players_ListChanged(object sender, ListChangedEventArgs e)
        {
            calcPersonsPerChunk();
        }

        private void txtChunks_Leave(object sender, EventArgs e)
        {
            calcPersonsPerChunk();
        }

        private void calcPersonsPerChunk()
        {
            if (int.TryParse(txtChunks.Text, out int chunks))
            {
                double personsPerChunk = (double)game.Players.Count(x => x.Alive && x.ID > 0) / chunks;
                if (txtPersonsPerChunk.Text != Math.Ceiling(personsPerChunk).ToString())
                {
                    txtPersonsPerChunk.Text = Math.Ceiling(personsPerChunk).ToString();
                }
            }
        }

        private void txtPersonsPerChunk_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(txtPersonsPerChunk.Text, out int personsPerChunk))
            {
                double chunks = (double)game.Players.Count(x => x.Alive && x.ID > 0) / personsPerChunk;
                if (txtChunks.Text != Math.Ceiling(chunks).ToString())
                {
                    txtChunks.Text = Math.Ceiling(chunks).ToString();
                }
            }
        }

        private void btnNewRound_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtChunks.Text, out int chunks))
            {
                game.NextRound(chunks);

                NagelCupTable tableRound = new NagelCupTable(game.CurrentRound);
                tableRound.Dock = DockStyle.Fill;

                TabPage tabPage = new TabPage();
                tabPage.Name = $"tabPageRound {game.Rounds.Count + 1}";
                tabPage.Text = $"Runde {game.Rounds.Count + 1}";
                tabPage.Controls.Add(tableRound);
                tabControl.TabPages.Add(tabPage);

                tabControl.SelectTab(tabControl.TabPages.Count - 1);
            }
        }
    }
}
