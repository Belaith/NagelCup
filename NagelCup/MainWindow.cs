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
            Game game = new Game();

            game.Players = new SortableList<Player>() { new Player("Till", 1, "", true) };
            game.Rounds = new List<Round>() { new Round() { Players = new SortableList<Player>() { new Player("Till", 1, "1", true) } } };
            game.Rounds.Add(new Round() { Players = new SortableList<Player>() { new Player("Till", 1, "1", true) } });

            TabPage tabPage1 = new TabPage();
            tabPage1.Name = "tabPageTeilnehmer";
            tabPage1.Text = "Teilnehmer";
            tabPage1.Controls.Add(new NagelCupTable(game, true));
            tabControl.TabPages.Add(tabPage1);

            int rounds = 0;
            foreach (var round in game.Rounds)
            {
                TabPage tabPage = new TabPage();
                tabPage.Name = "tabPageRound1";
                tabPage.Text = "Runde " + ++rounds;
                tabPage.Controls.Add(new NagelCupTable(round, false));
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
                    //file existiert, einlesen
                }
                else
                {
                    if (MessageBox.Show("Soll eine neue Datei erzeugt werden?", "Datei nicht gefunden!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {

                    //file existiert nochnicht, anlegen
                    }
                }
            }
        }
    }
}
