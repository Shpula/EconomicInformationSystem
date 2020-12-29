using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шобака
{
    public partial class MainMein : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "C:\\Users\\79021\\source\\repos\\EconomicInformationSystem\\Шобака\\NewBD.db");
        public MainMein()
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

        private void Materials_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
            ";New=False;Version=3";
            String selectCommand = "Select * from MainMein";
            SelectTable(ConnectionString, selectCommand);
        }

        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
            SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close();
            return value;
        }

        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath +
           ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void refreshForm(string ConnectionString, String selectCommand)
        {
            SelectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            PriceTextBox.Text = "";
            StoreNameTextBox.Text = "";
        }

        public void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            {
                Regex regex = new Regex(@"\d\,\d{2}");
                MatchCollection matches = regex.Matches(PriceTextBox.Text);
                if (matches.Count == 0)
                {
                    MessageBox.Show("Ошибка! ");
                    return;
                }
                string ConnectionString = @"Data Source=" + sPath +
                ";New=False;Version=3";
                String selectCommand = "select MAX(id) from MainMein";
                object maxValue = selectValue(ConnectionString, selectCommand);
                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                //вставка в таблицу MainMein
                string txtSQLQuery = "insert into MainMein (id, Price, MaterialName) values (" +
               (Convert.ToInt32(maxValue) + 1) + ", '" + PriceTextBox.Text + "','" + StoreNameTextBox.Text + "')";
                ExecuteQuery(txtSQLQuery);
                //обновление dataGridView1
                selectCommand = "select * from MainMein";
                refreshForm(ConnectionString, selectCommand);
                PriceTextBox.Text = "";
                StoreNameTextBox.Text = "";
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from MainMein where id=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from MainMein";
            refreshForm(ConnectionString, selectCommand);
            PriceTextBox.Text = "";
            StoreNameTextBox.Text = "";

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;

            //получить значение Name выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            string Price = PriceTextBox.Text;
            string StoreName = StoreNameTextBox.Text;

            String selectCommand = "update MainMein set Price='" + Price + "', MaterialName='" + StoreName + "' where id = " + valueId;
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from MainMein";
            refreshForm(ConnectionString, selectCommand);
            PriceTextBox.Text = "";
            StoreNameTextBox.Text = "";
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            string Price = dataGridView1[1, CurrentRow].Value.ToString();
            string StoreName = dataGridView1[2, CurrentRow].Value.ToString();
            PriceTextBox.Text = Price;
            StoreNameTextBox.Text = StoreName;
        }
    }
}