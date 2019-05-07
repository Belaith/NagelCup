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
using System.Xml.Serialization;

namespace NagelCup
{
    public partial class MainWindow : Form
    {
        string path = "";
        Game game;

        public MainWindow()
        {
            InitializeComponent();
            
            game = new Game();

            NagelCupTable table = new NagelCupTable(game);
            table.Dock = DockStyle.Fill;

            TabPage tabPage = new TabPage();
            tabPage.Name = "tabPageTeilnehmer";
            tabPage.Text = "Teilnehmer";
            tabPage.Controls.Add(table);
            tabControl.TabPages.Add(tabPage);            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    tabControl.TabPages.Clear();
                    if (Path.GetExtension(openFileDialog.FileName).ToUpper() == ".XML")
                    {
                        path = openFileDialog.FileName;
                        
                        XmlSerializer serializer = new XmlSerializer(typeof(Game));
                        using (TextReader writer = new StreamReader(path))
                        {
                            game = (Game)serializer.Deserialize(writer);
                        }

                        game.Init();

                        NagelCupTable table = new NagelCupTable(game);
                        table.Dock = DockStyle.Fill;

                        TabPage tabPage = new TabPage();
                        tabPage.Name = "tabPageTeilnehmer";
                        tabPage.Text = "Teilnehmer";
                        tabPage.Controls.Add(table);
                        tabControl.TabPages.Add(tabPage);

                        foreach (Round round in game.Rounds.OrderBy(x => x.ID).ToList())
                        {
                            NagelCupTable tableRound = new NagelCupTable(round);
                            tableRound.Dock = DockStyle.Fill;

                            TabPage tabPageRound = new TabPage();
                            tabPageRound.Name = $"tabPageRound {round.ID}";
                            tabPageRound.Text = $"Runde {round.ID}";
                            tabPageRound.Controls.Add(tableRound);
                            tabControl.TabPages.Add(tabPageRound);
                        }

                        if (game.CurrentRound != null)
                        {
                            NagelCupTable tableRound = new NagelCupTable(game.CurrentRound);
                            tableRound.Dock = DockStyle.Fill;

                            TabPage tabPageRound = new TabPage();
                            tabPageRound.Name = $"tabPageRound {game.CurrentRound.ID}";
                            tabPageRound.Text = $"Runde {game.CurrentRound.ID}";
                            tabPageRound.Controls.Add(tableRound);
                            tabControl.TabPages.Add(tabPageRound);

                            tabControl.SelectTab(tabControl.TabPages.Count - 1);
                        }

                        tabControl.SelectTab(tabControl.TabPages.Count - 1);

                        game.Players.ListChanged += Players_ListChanged;
                    }
                    else if(Path.GetExtension(openFileDialog.FileName).ToUpper() == ".CSV")
                    {
                        path = Path.GetFileNameWithoutExtension(openFileDialog.FileName) + ".xml";

                        game = new Game();

                        NagelCupTable table = new NagelCupTable(game);
                        table.Dock = DockStyle.Fill;

                        TabPage tabPage = new TabPage();
                        tabPage.Name = "tabPageTeilnehmer";
                        tabPage.Text = "Teilnehmer";
                        tabPage.Controls.Add(table);
                        tabControl.TabPages.Add(tabPage);

                        using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] spalten = line.Split(new string[] { ";", "," }, StringSplitOptions.None);

                                int id = 1;
                                if (spalten.Length < 2 || !int.TryParse(spalten[1], out id))
                                {
                                    if (game.Players.Count() > 0)
                                    {
                                        id = game.Players.Max(x => x.ID ?? 0) + 1;
                                    }
                                }
                                game.Players.Add(new Player(spalten[0], id, null, true));
                            }
                        }

                        game.Players.ListChanged += Players_ListChanged;
                    }
                }
            }
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

                if (game.Rounds.Count - 1 > 0)
                {
                    tabControl.TabPages.RemoveAt(tabControl.TabPages.Count - 1);

                    NagelCupTable tableRound = new NagelCupTable(game.Rounds[game.Rounds.Count - 1]);
                    tableRound.Dock = DockStyle.Fill;

                    TabPage tabPageRound = new TabPage();
                    tabPageRound.Name = $"tabPageRound {game.Rounds[game.Rounds.Count - 1].ID}";
                    tabPageRound.Text = $"Runde {game.Rounds[game.Rounds.Count - 1].ID}";
                    tabPageRound.Controls.Add(tableRound);
                    tabControl.TabPages.Add(tabPageRound);
                }

                NagelCupTable tableRoundCurrent = new NagelCupTable(game.CurrentRound);
                tableRoundCurrent.Dock = DockStyle.Fill;

                TabPage tabPageRoundCurrent = new TabPage();
                tabPageRoundCurrent.Name = $"tabPageRound_{game.Rounds.Count + 1}";
                tabPageRoundCurrent.Text = $"Runde {game.Rounds.Count + 1}";
                tabPageRoundCurrent.Controls.Add(tableRoundCurrent);
                tabControl.TabPages.Add(tabPageRoundCurrent);

                tabControl.SelectTab(tabControl.TabPages.Count - 1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                saveFileDialog.FileName = path;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Game));
                using (TextWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    serializer.Serialize(writer, game);
                }

                if (!string.IsNullOrWhiteSpace(path))
                {
                    path = saveFileDialog.FileName;
                }
            }
        }

        private void btnResetToRound_Click(object sender, EventArgs e)
        {
            int resetToRound = tabControl.SelectedIndex;

            string roundMessage = resetToRound == 0 ? "Teilnehmeranlage" : "Runde " + resetToRound.ToString();
            
            if (MessageBox.Show($"Wirklich auf {roundMessage} zurücksetzen?", "Achtung!", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            if (tabControl.TabCount > 1 && 0 < resetToRound && resetToRound < tabControl.TabCount - 1)
            {
                Round oldRound = game.Rounds.FirstOrDefault(x => x.ID == resetToRound);

                tabControl.TabPages.Clear();

                game.Rounds.RemoveAll(x => x.ID >= resetToRound);
                game.CurrentRound = null;

                game.SimpleListPlayers.ForEach(x => { x.Alive = false; x.Chunk = string.Empty; });

                foreach (Player player in game.Players)
                {
                    Player roundPlayer = oldRound.Players.FirstOrDefault(x=> x.ID == player.ID);

                    if (roundPlayer.Chunk != null)
                    {
                        player.Alive = roundPlayer.Alive;
                        player.Chunk = roundPlayer.Chunk;
                    }
                }

                game.Init();

                NagelCupTable table = new NagelCupTable(game);
                table.Dock = DockStyle.Fill;

                TabPage tabPage = new TabPage();
                tabPage.Name = "tabPageTeilnehmer";
                tabPage.Text = "Teilnehmer";
                tabPage.Controls.Add(table);
                tabControl.TabPages.Add(tabPage);

                foreach (Round round in game.Rounds.OrderBy(x => x.ID).ToList())
                {
                    NagelCupTable tableRound = new NagelCupTable(round);
                    tableRound.Dock = DockStyle.Fill;

                    TabPage tabPageRound = new TabPage();
                    tabPageRound.Name = $"tabPageRound {round.ID}";
                    tabPageRound.Text = $"Runde {round.ID}";
                    tabPageRound.Controls.Add(tableRound);
                    tabControl.TabPages.Add(tabPageRound);
                }

                if (game.CurrentRound != null)
                {
                    NagelCupTable tableRound = new NagelCupTable(game.CurrentRound);
                    tableRound.Dock = DockStyle.Fill;

                    TabPage tabPageRound = new TabPage();
                    tabPageRound.Name = $"tabPageRound {game.CurrentRound.ID}";
                    tabPageRound.Text = $"Runde {game.CurrentRound.ID}";
                    tabPageRound.Controls.Add(tableRound);
                    tabControl.TabPages.Add(tabPageRound);

                    tabControl.SelectTab(tabControl.TabPages.Count - 1);
                }

                tabControl.SelectTab(tabControl.TabPages.Count - 1);

            }
            else if (resetToRound == 0)
            {
                tabControl.TabPages.Clear();

                game.Locked = false;

                game.Rounds.Clear();

                game.SimpleListPlayers.ForEach(x => { x.Alive = true; x.Chunk = string.Empty; });

                NagelCupTable table = new NagelCupTable(game);
                table.Dock = DockStyle.Fill;

                TabPage tabPage = new TabPage();
                tabPage.Name = "tabPageTeilnehmer";
                tabPage.Text = "Teilnehmer";
                tabPage.Controls.Add(table);
                tabControl.TabPages.Add(tabPage);
            }
        }
    }
}
