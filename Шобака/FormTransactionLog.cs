
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
	public partial class FormTransactionLog : Form
	{
		private string selectTableJourn = "";
		private SQLiteConnection sql_con;
		private SQLiteCommand sql_cmd;
		private DataSet DS = new DataSet();
		private DataTable DT = new DataTable();
		private string sPath = Path.Combine(Application.StartupPath, "C:\\Users\\Butin\\source\\repos\\Шобака\\Шобака\\NewBD.db");
		public FormTransactionLog()
		{
			InitializeComponent();
			DateTime newDate;
			newDate = DateTime.Today;
			maskedTextBox1.Text = newDate.ToString("yyyy.MM.dd");
		}

		private void FormTransactionLog_Load(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			selectTable(ConnectionString);
			// выбрать значения из справочников для отображения в comboBox  
			String selectMaterial = "SELECT id, MaterialName, Price FROM MainMein";
			selectCombo(ConnectionString, selectMaterial, comboBoxMaterial, "MaterialName", "ID");
			String selectMOL = "SELECT ID, FIO FROM MOL";
			selectCombo(ConnectionString, selectMOL, comboBoxMOL, "FIO", "ID");
			String selectStorage = "SELECT ID, StorageName FROM Storage";
			selectCombo(ConnectionString, selectStorage, comboBoxStorage, "StorageName", "ID");
			String selectProvider = "SELECT ID, FIO FROM Provider";
			selectCombo(ConnectionString, selectProvider, comboBoxProvider, "FIO", "ID");
			String mValue = "select MAX(ID) from Document";
			object maxValue = selectValue(ConnectionString, mValue);
			if (Convert.ToString(maxValue) == "")
				maxValue = 0;
			textBoxNum.Text = (Convert.ToInt32(maxValue) + 1).ToString();
			textBoxCount.Text = "0";
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			// находим максимальное значение кода проводок для записи первичного ключа 
			String mValue = "select MAX(ID) from Document";
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
			string Value4 = null;
			string Value5 = null;
			if (comboBoxMaterial.Text != "")
			{
				//ОС 
				Value1 = comboBoxMaterial.SelectedValue.ToString();
			}
			if (comboBoxMOL.Text != "")
			{
				//Подразделение 
				Value2 = comboBoxMOL.SelectedValue.ToString();
			}
			if (comboBoxStorage.Text != "")
			{
				//Подразделение 
				Value4 = comboBoxStorage.SelectedValue.ToString();
			}
			if (comboBoxStorage.Text != "")
			{
				//Подразделение 
				Value5 = comboBoxProvider.SelectedValue.ToString();
			}
			//Поле количество 
			if (textBoxCount.Text != "")
			{
				count = textBoxCount.Text;
			}
			if (textBoxCount.Text == "0")
			{
				MessageBox.Show("Количество материалов не может быть 0!");
				return;
			}//Поиск по базе данных значений 
			String selectCost = "select Price from MainMein where ID ='" + Value1 + "'";
			object cost = selectValue(ConnectionString, selectCost);
			double Summa = Convert.ToDouble(cost) * Convert.ToInt32(count);
			String selectDT = "select ID from ChartOfAccounts where AccountNumber='10." + Value1 + "'";
			object DT = selectValue(ConnectionString, selectDT);
			String selectKT = "select ID from ChartOfAccounts where AccountNumber='60'";
			object KT = selectValue(ConnectionString, selectKT);

			string add = "INSERT INTO Document (ID, Date, Sum, Count, MOLid, Storageid, Materialid, Providerid) " +
				"VALUES (" + (Convert.ToInt32(maxValue) + 1) + ",'" + maskedTextBox1.Text + "','" + Summa.ToString() + "','" + textBoxCount.Text + "','" + Convert.ToInt32(Value5) + "','" +
				Convert.ToInt32(Value4) + "','" + Convert.ToInt32(Value2) + "','" + Convert.ToInt32(Value1) + "')";
			string addjourn = "INSERT INTO PostingJournal (ID, DebitAccount, SubkontoDebit1,  SubkontoDebit2," +
				"SubkontoDebit3, KreditAccount, SubkontoKredit1, Count, Sum, Date, Documentid) VALUES " +
				"(" + (Convert.ToInt32(maxValue) + 1) + ",'" + DT.ToString() + "','"  + comboBoxMaterial.Text + "','" + comboBoxStorage.Text + "','"
				 + comboBoxMOL.Text + "','" + KT.ToString()
				+ "','" + comboBoxProvider.Text + "','" + textBoxCount.Text +  "','" + Summa.ToString() + "','" 
				+ maskedTextBox1.Text + "','" + (Convert.ToInt32(maxValue) + 1) + "')";
			ExecuteQuery(add);
			ExecuteQuery(addjourn);
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
				SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("Select Document.ID AS '№ перемещения', Document.Date AS 'Дата', Document.Sum AS 'Сумма', MainMein.MaterialName AS 'Материал', Document.Count AS 'Количество' from Document JOIN MainMein ON Document.ID = MainMein.ID", connect);
				DataSet ds = new DataSet();
				dataAdapter.Fill(ds);
				dataGridView1.DataSource = ds;
				dataGridView1.DataMember = ds.Tables[0].ToString();
				connect.Close();
				dataGridView1.Columns["№ перемещения"].DisplayIndex = 0;
				dataGridView1.Columns["Дата"].DisplayIndex = 1;
				dataGridView1.Columns["Сумма"].DisplayIndex = 2;
				dataGridView1.Columns["Материал"].DisplayIndex = 3;
				dataGridView1.Columns["Количество"].DisplayIndex = 4;
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

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(comboBoxMOL.Text) || string.IsNullOrEmpty(comboBoxProvider.Text) || string.IsNullOrEmpty(comboBoxStorage.Text)) {
				MessageBox.Show("Выберите значение");
				return;
			}
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
			string valueId = dataGridView1[0, CurrentRow].Value.ToString();
			string id = comboBoxMaterial.SelectedValue.ToString();
			string select = "select Price from MainMein where ID = '" + id + "'";
			object str = selectValue(ConnectionString, select);
			double sum = Convert.ToDouble(str) * Convert.ToInt32(textBoxCount.Text);

			string count = "0";
			string Value1 = null;
			string Value2 = null;
			string Value3 = null;
			string Value4 = null;

			if (comboBoxMaterial.Text != "")
			{
				//ОС 
				Value1 = comboBoxMaterial.SelectedValue.ToString();
			}
			if (comboBoxMOL.Text != "")
			{
				//Подразделение 
				Value2 = comboBoxMOL.SelectedValue.ToString();
			}
			if (comboBoxStorage.Text != "")
			{
				//Подразделение 
				Value3 = comboBoxProvider.SelectedValue.ToString();
			}
			if (comboBoxStorage.Text != "")
			{
				//Подразделение 
				Value4 = comboBoxStorage.SelectedValue.ToString();
			}
			//Поле количество 
			if (textBoxCount.Text != "")
			{
				count = textBoxCount.Text;
			}
			if (textBoxCount.Text == "0")
			{
				MessageBox.Show("Количество материалов не может быть 0!");
				return;
			}

			String selectCost = "select Price from MainMein where ID ='" + Value1 + "'";
			object cost = selectValue(ConnectionString, selectCost);
			double Summa = Convert.ToDouble(cost) * Convert.ToInt32(count);
			String selectDT = "select ID from ChartOfAccounts where AccountNumber='10." + Value1 + "'";
			object DT = selectValue(ConnectionString, selectDT);
			String selectKT = "select ID from ChartOfAccounts where AccountNumber='60'";
			object KT = selectValue(ConnectionString, selectKT);
		
			String selectCommand = "update Document set Date='" + maskedTextBox1.Text + "', Sum='"
				+ sum.ToString() + "', Count = '" + textBoxCount.Text
				+ "', MOLid ='" + comboBoxMOL.SelectedValue.ToString() + "', Storageid = '"
				+ comboBoxStorage.SelectedValue.ToString() + "', MaterialID = '" + comboBoxMaterial.SelectedValue.ToString() + "' where ID = " + valueId +
				";update PostingJournal set DebitAccount = '" + DT.ToString() + "', SubkontoDebit1 = '" +
				comboBoxMaterial.Text + "',  SubkontoDebit2 = '" + comboBoxStorage.Text + "', SubkontoDebit3 = '" +
				comboBoxMOL.Text + "', KreditAccount = '" + DT.ToString() + "', SubkontoKredit1 = '" +
				comboBoxMaterial.Text + "', Count = '" + textBoxCount.Text + "', Sum = '" + Summa.ToString() +
				"', Date = '" + maskedTextBox1.Text + "' where Documentid = " + valueId;
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
			//обновление 
			selectTable(ConnectionString);
			dataGridView1.Update();
			dataGridView1.Refresh();
		}

		private void buttonDel_Click(object sender, EventArgs e)
		{
			int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
			//получить значение IDвыбранной строки
			string valueId = dataGridView1[0, CurrentRow].Value.ToString();
			String selectCommand = "delete from Document where ID=" + valueId + "; " +
				"delete from PostingJournal where Documentid=" + valueId;
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
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
			//обновление 
			selectTable(ConnectionString);
			dataGridView1.Update();
			dataGridView1.Refresh();
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

		private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			string id = dataGridView1[0, e.RowIndex].Value.ToString();
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			String select = "select MOLid from Document where ID = '" + id + "'";
			object str = selectValue(ConnectionString, select);
			comboBoxStorage.SelectedValue = str.ToString();
			select = "select Storageid from Document where ID = '" + id + "'";
			str = selectValue(ConnectionString, select);
			comboBoxProvider.SelectedValue = str.ToString();
			select = "select Providerid from Document where ID = '" + id + "'";
			str = selectValue(ConnectionString, select);
			DateTime newDate;
			newDate = DateTime.Today;
			textBoxNum.Text = id;
			maskedTextBox1.Text = dataGridView1[1, e.RowIndex].Value.ToString();
			textBoxCount.Text = dataGridView1[4, e.RowIndex].Value.ToString();
			comboBoxMaterial.Text = dataGridView1[3, e.RowIndex].Value.ToString();
		}

        private void button1_Click(object sender, EventArgs e)
        {
			int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
			string valueId = dataGridView1[0, CurrentRow].Value.ToString();
			FormPostingJournal form = new FormPostingJournal(Convert.ToInt32(valueId));
			form.Show();
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
			selectTable(ConnectionString);
		}
	}
}
