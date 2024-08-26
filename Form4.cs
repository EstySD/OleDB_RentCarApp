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
    public partial class Form4 : MaterialForm
    {
        public string[] readerValues2 = new string[6];
        public string[] readerValues1 = new string[6];
        public string[] readerValues3 = new string[5];
        public string[] readerValues4 = new string[2]; // скидки
        public string[] readerValues5 = new string[3]; // размер скидок
        public string[] readerValues6 = new string[6]; //авто
        public string[] readerValues7 = new string[3]; //штрафы
        public string[] readerValues8 = new string[2]; // имя штрафов

        AccessManager am = new AccessManager();


        public Form4()
        {
            InitializeComponent();

            am.MaterialDesignSettings(this as MaterialForm);
            am.DgvSet(dataGridView2);
            am.DgvSet(dataGridView1);
            am.DgvSet(dataGridView3);
            am.DgvSet(dataGridView4);
            am.DgvSet(dataGridView5);
            am.DgvSet(dataGridView6);
            am.DgvSet(dataGridView7);
            am.DgvSet(dataGridView8);
            pictureBox1.Image = Image.FromFile("./img/logout.png");
        }

        public void ClearRegBox()
        {
            SecondNameBox.Clear();
            FirstNameBox.Clear();
            ThirdNameBox.Clear();
            AddressBox.Clear();
            PhoneBox.Clear();
        }
        
        public void LoadDB(string[] readerValues, DataGridView d1, string query)
        {
            d1.Rows.Clear();
            am.AccessLoader(readerValues, d1, query);

        }
        public void RegForm()
        {
            //рег форма
            readerValues1[0] = "Код";
            readerValues1[1] = "Фамилия";
            readerValues1[2] = "Имя";
            readerValues1[3] = "Отчество";
            readerValues1[4] = "Адрес";
            readerValues1[5] = "Телефон";
            LoadDB(readerValues1, dataGridView1, "SELECT Клиенты.[Код], Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество], Клиенты.[Адрес], Клиенты.[Телефон] FROM Клиенты");
        }
        public void LoadClients()
        {
            //клиенты
            readerValues2[0] = "Код";
            readerValues2[1] = "Фамилия";
            readerValues2[2] = "Имя";
            readerValues2[3] = "Отчество";
            readerValues2[4] = "Адрес";
            readerValues2[5] = "Телефон";
            LoadDB(readerValues2, dataGridView2, "SELECT Клиенты.[Код], Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество], Клиенты.[Адрес], Клиенты.[Телефон] FROM Клиенты");
        }
        public void Unfinished()
        {
            //вернуть авто
            readerValues3[0] = "Код_заказа";
            readerValues3[1] = "Марка_автомобиля";
            readerValues3[2] = "Фамилия_клиента";
            readerValues3[3] = "Дата_выдачи";
            readerValues3[4] = "Дата_возврата";
            LoadDB(readerValues3, dataGridView3, "SELECT ВыданныеАвто.[Код_заказа], ВыданныеАвто.[Марка_автомобиля], ВыданныеАвто.[Фамилия_клиента], ВыданныеАвто.[Дата_выдачи], ВыданныеАвто.[Дата_возврата] FROM ВыданныеАвто WHERE(((ВыданныеАвто.[Дата_возврата]) Is Null))");
        }
        // скидки
        public void Discounts()
        {
            //вернуть авто
            int userkey = Convert.ToInt32(dataGridView2.Rows[am.IndexSelected(dataGridView2)].Cells[0].Value);
            readerValues4[0] = "Фамилия_клиента";
            readerValues4[1] = "Наименование_скидки";
            LoadDB(readerValues4, dataGridView4, "SELECT Выданные_скидки.[Фамилия_клиента], Выданные_скидки.[Наименование_скидки] FROM Выданные_скидки WHERE Выданные_скидки.[Фамилия_клиента] = " + userkey);
        }
        private void Add_Button3_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues4.Length, dataGridView4, "INSERT INTO Выданные_скидки");
            ErrorLabel2.Text = am.error;
            Discounts();
        }

        private void Delete_Button3_Click(object sender, EventArgs e)
        {
            int index = am.IndexSelected(dataGridView4 as DataGridView);
            
            //Проверим данные в таблицы
            if (dataGridView4.Rows[index].Cells[0].Value == null)
            {
                am.error = "Не все данные введены!";
                return;
            }

            //Считаем данные
            string id0 = dataGridView4.Rows[index].Cells[0].Value.ToString();
            string id1 = dataGridView4.Rows[index].Cells[1].Value.ToString();
            string query = "DELETE FROM Выданные_скидки WHERE Выданные_скидки.Наименование_скидки = " + id1 + " AND Выданные_скидки.Фамилия_клиента =" + id0;
            am.BasicQueryExecutor(query);
            Discounts();
        }

        //размер скидок 
        public void DiscountsValue()
        {
            
            readerValues5[0] = "Код_скидки";
            readerValues5[1] = "Наименование_скидки";
            readerValues5[2] = "Размер_скидки";
            LoadDB(readerValues5, dataGridView5, "SELECT Скидки.[Код_скидки], Скидки.[Наименование_скидки], Скидки.[Размер_скидки] FROM Скидки");
        }
        private void Add_Button4_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues5.Length, dataGridView5, "INSERT INTO Скидки");
            ErrorLabel2.Text = am.error;
            DiscountsValue();
        }

        private void Delete_Button4_Click(object sender, EventArgs e)
        {
            am.DeleteDB("DELETE FROM Скидки WHERE [Код_скидки] = ", dataGridView5);
            ErrorLabel2.Text = am.error;
            DiscountsValue();
        }

        private void Refresh_Button4_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues5, dataGridView5, "UPDATE Скидки SET ");
            ErrorLabel2.Text = am.error;
            DiscountsValue();
        }
        // авто

        public void LoadAvto()
        {
            //авто
            readerValues6[0] = "Код_автомобиля";
            readerValues6[1] = "Марка";
            readerValues6[2] = "Стоимость";
            readerValues6[3] = "Стоимость_проката";
            readerValues6[4] = "Тип";
            readerValues6[5] = "Год_выпуска";
            LoadDB(readerValues6, dataGridView6, "SELECT Автомобили.[Код_автомобиля], Автомобили.[Марка], Автомобили.[Стоимость], Автомобили.[Стоимость_проката], Автомобили.[Тип], Автомобили.[Год_выпуска] FROM Автомобили");

        }

        private void Add_Button5_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues6.Length, dataGridView6, "INSERT INTO Автомобили");
            ErrorLabel6.Text = am.error;
            LoadAvto();
        }

        private void Delete_Button5_Click(object sender, EventArgs e)
        {
            am.DeleteDB("DELETE FROM Автомобили WHERE [Код_автомобиля] = ", dataGridView6);
            ErrorLabel6.Text = am.error;
            LoadAvto();
        }

        private void Refresh_Button5_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues6, dataGridView6, "UPDATE Автомобили SET ");
            ErrorLabel6.Text = am.error;
            LoadAvto();
        }
        //штрафы
        public void LoadFines()
        {
            int index = am.IndexSelected(dataGridView6);
            string keyfines = dataGridView6.Rows[index].Cells[0].Value.ToString();
            //авто
            readerValues7[0] = "Марка_автомобиля";
            readerValues7[1] = "Наименование_штрафа";
            readerValues7[2] = "Размер_штрафа";
            LoadDB(readerValues7, dataGridView7, "SELECT ВыданныеШтрафы.[Марка_автомобиля], ВыданныеШтрафы.[Наименование_штрафа], ВыданныеШтрафы.[Размер_штрафа] FROM ВыданныеШтрафы WHERE ВыданныеШтрафы.[Марка_автомобиля] = " + keyfines);

        }

        private void Add_Button6_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues7.Length, dataGridView7, "INSERT INTO ВыданныеШтрафы");
            ErrorLabel6.Text = am.error;
            LoadFines();
        }

        private void Delete_Button6_Click(object sender, EventArgs e)
        {
            int index = am.IndexSelected(dataGridView7 as DataGridView);

            //Проверим данные в таблицы
            if (dataGridView7.Rows[index].Cells[0].Value == null)
            {
                am.error = "Не все данные введены!";
                return;
            }

            //Считаем данные
            string id0 = dataGridView7.Rows[index].Cells[0].Value.ToString();
            string id1 = dataGridView7.Rows[index].Cells[1].Value.ToString();
            string query = "DELETE FROM ВыданныеШтрафы WHERE ВыданныеШтрафы.Марка_автомобиля = " + id0 + " AND ВыданныеШтрафы.Наименование_штрафа =" + id1;
            am.BasicQueryExecutor(query);
            LoadFines();
        }
        // имя штрафов

        public void FinesValue()
        {

            readerValues8[0] = "Код";
            readerValues8[1] = "Наименование_штрафа";
            LoadDB(readerValues8, dataGridView8, "SELECT Наименование_штрафа.[Код], Наименование_штрафа.[Наименование_штрафа] FROM Наименование_штрафа");
        }

        private void Add_Button7_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues8.Length, dataGridView8, "INSERT INTO Наименование_штрафа");
            ErrorLabel6.Text = am.error;
            FinesValue();
        }

        private void Delete_Button7_Click(object sender, EventArgs e)
        {
            am.DeleteDB("DELETE FROM Наименование_штрафа WHERE [Код] = ", dataGridView8);
            ErrorLabel6.Text = am.error;
            FinesValue();
        }

        private void Refresh_Button7_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues8, dataGridView8, "UPDATE Наименование_штрафа SET ");
            ErrorLabel6.Text = am.error;
            FinesValue();
        }
        //
        private void Form4_Load(object sender, EventArgs e)
        {
            LoadClients();
            Unfinished();
            Discounts();
            DiscountsValue();
            LoadAvto();
            LoadFines();
            FinesValue();
            dataGridView4.Columns[0].Width=1;
        }

        private void Add_Button2_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues2.Length, dataGridView2, "INSERT INTO Клиенты");
            ErrorLabel2.Text = am.error;
            LoadClients();
        }

        private void Delete_Button2_Click(object sender, EventArgs e)
        {
            am.DeleteDB("DELETE FROM Клиенты WHERE [Код] = ", dataGridView2);
            ErrorLabel2.Text = am.error;
            LoadClients();
        }

        private void Refresh_Button2_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues2, dataGridView2, "UPDATE Клиенты SET ");
            ErrorLabel2.Text = am.error;
            LoadClients();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            LoadClients();
            dataGridView1.Rows[0].Cells[0].Value = (Int32.Parse(am.keymax) + 1).ToString();
            dataGridView1.Rows[0].Selected=true;
            am.AddDB(readerValues2.Length, dataGridView1, "INSERT INTO Клиенты");
            materialLabel1.Text = am.error;
            dataGridView1.Rows.Clear();
            ClearRegBox();
            LoadClients();
        }

        private void SecondNameBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[1].Value = SecondNameBox.Text;
        }

        private void FirstNameBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[2].Value = FirstNameBox.Text;
        }

        private void ThirdNameBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[3].Value = ThirdNameBox.Text;
        }

        private void AddressBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[4].Value = AddressBox.Text;
        }

        private void PhoneBox_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows[0].Cells[5].Value = PhoneBox.Text;
        }

        private void Refresh_Button1_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues3, dataGridView3, "UPDATE ВыданныеАвто SET ");
            ErrorLabel1.Text = am.error;
            Unfinished();
        }

        private void ClearButton1_Click(object sender, EventArgs e)
        {
            ClearRegBox();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            am.LogOut(this as Form);
        }


        private void dataGridView2_Click(object sender, EventArgs e)
        {
            Discounts();

        }

        private void dataGridView6_Click(object sender, EventArgs e)
        {
            LoadFines();
        }

    }
}
