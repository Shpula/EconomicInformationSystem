using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шобака
{
    public partial class MainMenu : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "NBD.db");
        public MainMenu()
        {
            InitializeComponent();
        }

        private void планСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartOfAccounts chart = new chartOfAccounts();
            chart.Show();
        }

        private void мОЛToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MOL chart = new MOL();
            chart.Show();
        }

        private void поставщикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Provider chart = new Provider();
            chart.Show();
        }

        private void складToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Storage chart = new Storage();
            chart.Show();
        }

        private void основнойСчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMein chart = new MainMein();
            chart.Show();
        }
    }
}
 