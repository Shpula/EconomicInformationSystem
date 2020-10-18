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
    public partial class chartOfAccounts : Form
    {
        private string sPath = Path.Combine(Application.StartupPath, "C:\\Users\\Butin\\source\\repos\\Шобака\\Шобака\\NBD.db");
        public chartOfAccounts()
        {
            InitializeComponent();
        }

        public void SelectTable(string ConnectionString, String selectCommand)

        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        private void chartOfAccounts_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
        ";New=False;Version=3";
            String selectCommand = "Select * from chartOfAccounts";
            SelectTable(ConnectionString, selectCommand);
        }
    }
}
