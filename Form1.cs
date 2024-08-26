using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.OleDb;
namespace ArendaAvto
{
    public partial class Form1 : MaterialForm
    {
        string login;
        string password;
        string keyloginvalue;
        public Form1()
        {
            InitializeComponent();
            AccessManager am = new AccessManager();
            am.MaterialDesignSettings(this as MaterialForm);
            pictureBox1.Image = Image.FromFile("./img/user.png");
            pictureBox2.Image = Image.FromFile("./img/enter.png");
        }
        private bool LoginAccess(string query, string lvalue, string pvalue)
        {
            login = LoginText.Text;
            password = PasswordText.Text;
            bool valid = false;
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение
            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            //строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//считываем данные

            //Проверяем данные
            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены!", "Ошибка!");
            }
            else
            {
                //Запишем данные в таблицу формы
                while (dbReader.Read())
                {
                    if (login == (string)dbReader[lvalue] && password == (string)dbReader[pvalue])
                    {
                        valid = true;
                        keyloginvalue = dbReader["Код"].ToString();
                    }
                }
            }
            //Закрываем соеденение с БД
            dbReader.Close();
            dbConnection.Close();
            return valid;
        }
        private void EnterButton_Click(object sender, EventArgs e)
        {
            bool valid = false;
            valid = LoginAccess("SELECT ВходДиректор.[Код], ВходДиректор.[Логин], ВходДиректор.[Пароль] FROM ВходДиректор", "Логин", "Пароль");
            if (valid==true)
            {
                this.Hide();
                Form f2 = new Form2();
                f2.Closed += (s, args) => this.Close();
                f2.Show();
            }
            valid = LoginAccess("SELECT Клиенты.* FROM Клиенты", "Фамилия", "Имя");
            if (valid == true)
            {
                this.Hide();
                Form f3 = new Form3(keyloginvalue);
                f3.Closed += (s, args) => this.Close();
                f3.Show();
            }
            valid = LoginAccess("SELECT СотрудникиВход.[Код], СотрудникиВход.[Логин], СотрудникиВход.[Пароль] FROM СотрудникиВход", "Логин", "Пароль");
            if (valid == true)
            {
                this.Hide();
                Form f4 = new Form4();
                f4.Closed += (s, args) => this.Close();
                f4.Show();
            }
            if (valid==false)
            {
                LoginErrorLabel.Text = "Неверный логин или пароль";
            }
            
        }
    }
}
