using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шобака
{
	public partial class FormPostingJournal : Form
	{
		private string selectTableJourn = "";
		private SQLiteConnection sql_con;
		private SQLiteCommand sql_cmd;
		private DataSet DS = new DataSet();
		private DataTable DT = new DataTable();
		private string sPath = Path.Combine(Application.StartupPath, "C:\\Users\\79021\\source\\repos\\EconomicInformationSystem\\Шобака\\NewBD.db");
		public FormPostingJournal(int cur)
		{
			InitializeComponent();
			if (cur != -123)
			{
				selectTableJourn = "Select PostingJournal.ID AS '№ перемещения'," +
					" D.AccountNumber AS 'Дт', PostingJournal.SubkontoDebit1 AS 'СубконтоДт1', " +
					"PostingJournal.SubkontoDebit2 AS 'СубконтоДт2'," +
					" PostingJournal.SubkontoDebit3 AS 'СубконтоДт3', " +
					" B.AccountNumber AS 'Кт', PostingJournal.SubkontoKredit1 AS 'СубконтоКт1', " +
					" PostingJournal.Count AS 'Количество'," +
					"PostingJournal.Sum AS 'Сумма',PostingJournal.Date AS 'Дата'," +
					" PostingJournal.Documentid AS 'Журнал операций'" +
					" from PostingJournal, ChartOfAccounts D, ChartOfAccounts B " +
					"Where D.ID = PostingJournal.DebitAccount and B.ID = PostingJournal.KreditAccount and PostingJournal.Documentid = '" + cur + "'";
			}
			DateTime newDate;
			newDate = DateTime.Today;
			//maskedTextBox1.Text = newDate.ToString("yyyy.MM.dd");
		}

		private void FormPostingJournal_Load(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			selectTable(ConnectionString);
			// выбрать значения из справочников для отображения в comboBox  
			String selectMaterial = "SELECT ID, Price, MaterialName FROM MainMein";
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			// находим максимальное значение кода проводок для записи первичного ключа 
			String mValue = "select MAX(ID) from PostingJournal";
			object maxValue = selectValue(ConnectionString, mValue);
			if (Convert.ToString(maxValue) == "")
				maxValue = 0;
			// Обнулить значения переменных 
			string sum = "0";
			string count = "0";
			string coment = null;
			string Value1 = null;
			string Value2 = null;
			string Value3 = null;
			String selectDT = "select ID from ChartOfAccounts where AccountNumber='10." + Value1 + "'";
			object DT = selectValue(ConnectionString, selectDT);
			String selectKT = "select ID from ChartOfAccounts where AccountNumber='60'";
			object KT = selectValue(ConnectionString, selectKT);
			String selectCost = "select Price from MainMein where ID ='" + Value1 + "'";
			object cost = selectValue(ConnectionString, selectCost);
			double Summa = Convert.ToDouble(cost) * Convert.ToInt32(count);
			selectTable(ConnectionString);
		}
		private void ExecuteQuery(string txtQuery)
		{
			sql_con = new SQLiteConnection("Data Source=" + sPath + ";Version=3;New=False;Compress=True;");
			sql_con.Open();
			sql_cmd = sql_con.CreateCommand();
			sql_cmd.CommandText = txtQuery;
			sql_cmd.ExecuteNonQuery();
			sql_con.Close();
		}

		public void selectTable(string ConnectionString)
		{
			try
			{
				SQLiteConnection connect = new SQLiteConnection(ConnectionString);
				connect.Open();
				SQLiteDataAdapter dataAdapter;
				if (selectTableJourn == "")
				{
					dataAdapter = new SQLiteDataAdapter("Select PostingJournal.ID AS '№ перемещения'," +
					" D.AccountNumber AS 'Дт', PostingJournal.SubkontoDebit1 AS 'СубконтоДт1', " +
					"PostingJournal.SubkontoDebit2 AS 'СубконтоДт2'," +
					" PostingJournal.SubkontoDebit3 AS 'СубконтоДт3', " +
					" B.AccountNumber AS 'Кт', PostingJournal.SubkontoKredit1 AS 'СубконтоКт1', " +
					" PostingJournal.Count AS 'Количество'," + " PostingJournal.Date AS 'Дата'," +
					"PostingJournal.Sum AS 'Сумма',PostingJournal.Date AS 'Дата'," +
					"PostingJournal.Documentid AS 'Журнал операций'" +
					" from PostingJournal, ChartOfAccounts D, ChartOfAccounts B " +
					"Where D.ID = PostingJournal.DebitAccount and B.ID = PostingJournal.KreditAccount", connect);
				}
				else
				{
					dataAdapter = new SQLiteDataAdapter(selectTableJourn, connect);
					selectTableJourn = "";
				}
				DataSet ds = new DataSet();
				dataAdapter.Fill(ds);
				dataGridView1.DataSource = ds;
				dataGridView1.DataMember = ds.Tables[0].ToString();
				connect.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Oшибка");
			}
		}
		public void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBox, string displayMember, string valueMember)
		{
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
			connect.Open();
			SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
			DataSet ds = new DataSet();
			dataAdapter.Fill(ds);
			comboBox.DataSource = ds.Tables[0];
			comboBox.DisplayMember = displayMember;
			comboBox.ValueMember = valueMember;
			connect.Close();
		}
		public object selectValue(string ConnectionString, String selectCommand)
		{
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
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
		public void changeValue(string ConnectionString, String selectCommand)
		{
			SQLiteConnection connect = new SQLiteConnection(ConnectionString);
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

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			selectTable(ConnectionString);
		}
	}
}
