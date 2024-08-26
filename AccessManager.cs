using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
using MaterialSkin;
using MaterialSkin.Controls;
namespace ArendaAvto
{
    public class AccessManager
    {
        public string error;
        public string keymax="1";
        // forms
        public void MaterialDesignSettings(MaterialForm f)
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(f);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepPurple700, Primary.DeepPurple800, Primary.DeepPurple700, Accent.DeepPurple700, TextShade.WHITE);
        }

        public void DgvSet(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }
        public void LogOut(Form f)
        {
            
            f.Hide();
            Form f1 = new Form1();
            f1.Closed += (s, args) => f.Close();
            f1.Show();
        }
        public int IndexSelected(DataGridView d1)
        {
            error = "";



            if (d1.SelectedRows.Count != 1)
            {
                error = "Выберите одну строку! ";
                return 0;
            }

            //Запомним выбранную строку
            return d1.SelectedRows[0].Index;
        }




        //db_edit
        //opening connection
        public void BasicQueryExecutor(string query)
        {
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение
            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //Выполняем запрос
            if (dbCommand.ExecuteNonQuery() != 1)
                error = "Ошибка запроса";
            else
                error = "Успешно";

            //Закрываем соеденение с БД
            dbConnection.Close();
        }

        public void AccessLoader(string[] readerValues, DataGridView d1, string query)
        {
            object[] items = new object[readerValues.Length];
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb";//строка соеденения
            OleDbConnection dbConnection = new OleDbConnection(connectionString);//создаем соеденение
            //Выполянем запрос к БД
            dbConnection.Open();//открываем соеденение
            
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//считываем данные
            error = "";
            //Проверяем данные
            if (dbReader.HasRows == false)
            {
                error = "Нет данных";
            }
            else
            {
                //Запишем данные в таблицу формы
                while (dbReader.Read())
                {
                    for (int i=0; i<readerValues.Length; i++)
                    {
                        items[i] = dbReader[readerValues[i]];
                        
                    }
                    keymax = items[0].ToString();
                    d1.Rows.Add(items);
                }
            }
            //Закрываем соеденение с БД
            dbReader.Close();
            dbConnection.Close();
        }
        //int stlength, DataGridView d1 , 
        public void AddDB(int stlength, DataGridView d1, string query)
        {
            
            bool cellcheck=false;
            

            //Запомним выбранную строку
            int index = IndexSelected(d1 as DataGridView);

            
            for(int i=0; i<stlength; i++)
            {
                if(d1.Rows[index].Cells[i].Value==null)
                {
                    cellcheck = true;
                }
            }
            if (cellcheck==true)
            {
                error = "Не все данные введены";
                return;
            }
            query += " VALUES(' " + d1.Rows[index].Cells[0].Value.ToString();
            for (int i = 1; i < stlength-1; i++)
            {
                query += "', '" + d1.Rows[index].Cells[i].Value.ToString();
            }
            if(d1.Rows[index].Cells[stlength - 1].Value.ToString() == "Null")
            {
                query += "', " + d1.Rows[index].Cells[stlength - 1].Value.ToString();
                query += ")";
            }
            else
            {

                query += "', '" + d1.Rows[index].Cells[stlength - 1].Value.ToString();
                query += "')";
            }
            BasicQueryExecutor(query);
        }
        public void DeleteDB(string query, DataGridView d1)
        {

            //Запомним выбранную строку
            int index = IndexSelected(d1 as DataGridView);

            //Проверим данные в таблицы
            if (d1.Rows[index].Cells[0].Value == null)
            {
                error = "Не все данные введены!";
                return;
            }

            //Считаем данные
            string id = d1.Rows[index].Cells[0].Value.ToString();
            query += id;
            BasicQueryExecutor(query);
        }
        public void UpdateDB(string[] readerValues, DataGridView d1, string query)
        {
            int stlength = readerValues.Length;

            

            bool cellcheck = false;


            //Запомним выбранную строку
            int index = IndexSelected(d1 as DataGridView);


            for (int i = 0; i < stlength; i++)
            {
                if (d1.Rows[index].Cells[i].Value == null)
                {
                    cellcheck = true;
                }
            }
            if (cellcheck == true)
            {
                error = "Не все данные введены";
                return;
            }
            query += readerValues[1] + " = '" + d1.Rows[index].Cells[1].Value.ToString();
            for (int i = 2; i < stlength; i++)
            {
                query += "', " + readerValues[i] + "= '" + d1.Rows[index].Cells[i].Value.ToString();
            }
            query += "' WHERE " + readerValues[0] + " = " + d1.Rows[index].Cells[0].Value.ToString() + "";
            //query += "Марка = '" + d1.Rows[index].Cells[1].Value.ToString() + "', Стоимость = '" + d1.Rows[index].Cells[2].Value.ToString() + "', Стоимость_проката = '" + d1.Rows[index].Cells[3].Value.ToString() + "', Тип = '" + d1.Rows[index].Cells[4].Value.ToString() + "', Год_выпуска = '" + d1.Rows[index].Cells[5].Value.ToString() + "' WHERE Код_автомобиля = " + d1.Rows[index].Cells[0].Value.ToString()+ "";

            BasicQueryExecutor(query);
        }
        
    }
}
