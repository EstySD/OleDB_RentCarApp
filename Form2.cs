using System;
using System.Collections;
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
    public partial class Form2 : MaterialForm
    {
        public string[] readerValues1 = new string[6]; //авто
        public string[] readerValues2 = new string[6]; //клиенты
        public string[] readerValues3 = new string[5];// выданные
        public string[] readerValues4 = new string[3];
        public string[] readerValues5 = new string[2]; //прибыль за неделю
        public string[] readerValues6 = new string[2]; //сроки
        public string[] readerValues7 = new string[3]; //работники


        AccessManager am = new AccessManager();

        
        public Form2()
        {
            InitializeComponent();
            am.MaterialDesignSettings(this as MaterialForm);

            pictureBox1.Image = Image.FromFile("./img/logout.png");

            // настройка datagridview
            am.DgvSet(dataGridView1);
            am.DgvSet(dataGridView2);
            am.DgvSet(dataGridView3);
            am.DgvSet(dataGridView4);
            am.DgvSet(dataGridView5);
            am.DgvSet(dataGridView6);
            am.DgvSet(dataGridView7);

        }
        public void LoadDB(string[] readerValues, DataGridView d1, string query)
        {
            d1.Rows.Clear();
            am.AccessLoader(readerValues, d1, query);
            
        }
        public void LoadAvto()
        {
            //авто
            readerValues1[0] = "Код_автомобиля";
            readerValues1[1] = "Марка";
            readerValues1[2] = "Стоимость";
            readerValues1[3] = "Стоимость_проката";
            readerValues1[4] = "Тип";
            readerValues1[5] = "Год_выпуска";
            LoadDB(readerValues1, dataGridView1, "SELECT Автомобили.[Код_автомобиля], Автомобили.[Марка], Автомобили.[Стоимость], Автомобили.[Стоимость_проката], Автомобили.[Тип], Автомобили.[Год_выпуска] FROM Автомобили");

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
        public void LoadOrders()
        {
            //выданные авто
            readerValues3[0] = "Код_заказа";
            readerValues3[1] = "Марка_автомобиля";
            readerValues3[2] = "Фамилия_клиента";
            readerValues3[3] = "Дата_выдачи";
            readerValues3[4] = "Дата_возврата";
            LoadDB(readerValues3, dataGridView3, "SELECT ВыданныеАвто.[Код_заказа], ВыданныеАвто.[Марка_автомобиля], ВыданныеАвто.[Фамилия_клиента], ВыданныеАвто.[Дата_выдачи], ВыданныеАвто.[Дата_возврата] FROM ВыданныеАвто");
        }
        public void LoadBestAvto()
        {
            //самые прибыльные авто
            readerValues4[0] = "Марка";
            readerValues4[1] = "First-Стоимость2";
            readerValues4[2] = "Общая прибыль";
            LoadDB(readerValues4, dataGridView4, "SELECT Автомобили.Марка, First([Стоимость2].[Стоимость2]) AS [First-Стоимость2], Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата],2)*[Стоимость2]) AS [Общая прибыль] FROM(Автомобили INNER JOIN ВыданныеАвто ON Автомобили.[Код_автомобиля] = [ВыданныеАвто].[Марка_автомобиля]) INNER JOIN Стоимость2 ON([ВыданныеАвто].[Марка_автомобиля] = [Стоимость2].[Код_автомобиля]) AND(Автомобили.[Код_автомобиля] = [Стоимость2].[Код_автомобиля]) GROUP BY Автомобили.Марка ORDER BY Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата],2)*[Стоимость2]) DESC");
        }
        public void LoadProfit()
        {
            dataGridView5.Rows.Clear();
            //прибыль за месяц
            readerValues5[0] = "Код_заказа";
            readerValues5[1] = "Общая_прибыль";
            LoadDB(readerValues5, dataGridView5, "SELECT ВыданныеАвто.Код_заказа, Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата],2)*[Стоимость2]) AS [Общая_прибыль], ВыданныеАвто.Дата_возврата FROM(Автомобили INNER JOIN ВыданныеАвто ON Автомобили.[Код_автомобиля] = ВыданныеАвто.[Марка_автомобиля]) INNER JOIN Стоимость2 ON Автомобили.[Код_автомобиля] = Стоимость2.[Код_автомобиля] GROUP BY ВыданныеАвто.Код_заказа, ВыданныеАвто.Дата_возврата HAVING(((ВыданныеАвто.Дата_возврата) > Date() - 31)) ORDER BY Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата], 2) *[Стоимость2]) DESC");
        }
        public void RentalPeriod()
        {
            //сроки
            readerValues6[0] = "Марка";
            readerValues6[1] = "Срок_проката";
            LoadDB(readerValues6, dataGridView6, "SELECT Автомобили.Марка, Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата],2)) AS [Срок_проката] FROM Автомобили INNER JOIN ВыданныеАвто ON Автомобили.[Код_автомобиля] = ВыданныеАвто.[Марка_автомобиля] GROUP BY Автомобили.Марка, ВыданныеАвто.[Марка_автомобиля] ORDER BY Sum(DateDiff('d',[Дата_выдачи],[Дата_возврата], 2)) DESC");
        }
        public void Workers()
        {
            //самые прибыльные авто
            readerValues7[0] = "Код";
            readerValues7[1] = "Логин";
            readerValues7[2] = "Пароль";
            LoadDB(readerValues7, dataGridView7, "SELECT СотрудникиВход.Код, СотрудникиВход.Логин, СотрудникиВход.Пароль FROM СотрудникиВход");
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadAvto();
            LoadClients();
            LoadOrders();
            LoadBestAvto();
            LoadProfit();
            RentalPeriod();
            Workers();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        { 
            
            am.AddDB(readerValues1.Length, dataGridView1, "INSERT INTO Автомобили");
            errorLabel.Text = am.error;
            LoadAvto();
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            
            
            am.DeleteDB("DELETE FROM Автомобили WHERE [Код_автомобиля] = ", dataGridView1);
            errorLabel.Text = am.error;
            LoadAvto();
        }

        private void Refresh_Button_Click(object sender, EventArgs e)
        {
            
            am.UpdateDB(readerValues1, dataGridView1, "UPDATE Автомобили SET ");
            errorLabel.Text = am.error;
            LoadAvto();
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

        private void Add_Button3_Click(object sender, EventArgs e)
        {
            
            am.AddDB(readerValues3.Length, dataGridView3, "INSERT INTO ВыданныеАвто");
            ErrorLabel3.Text = am.error;
            LoadOrders();
        }

        private void Delete_Button3_Click(object sender, EventArgs e)
        {
            
            am.DeleteDB("DELETE FROM ВыданныеАвто WHERE [Код_заказа] = ", dataGridView3);
            ErrorLabel3.Text = am.error;
            LoadOrders();
        }
        
        private void Add_ButtonWorker_Click(object sender, EventArgs e)
        {
            
            am.AddDB(readerValues7.Length, dataGridView7, "INSERT INTO СотрудникиВход");
            ErrorLabel4.Text = am.error;
            Workers();
        }

        private void Delete_ButtonWorker_Click(object sender, EventArgs e)
        {
            
            am.DeleteDB("DELETE FROM СотрудникиВход WHERE [Код] = ", dataGridView7);
            ErrorLabel4.Text = am.error;
            Workers();
        }

        private void Refresh_ButtonWorker_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues7, dataGridView7, "UPDATE СотрудникиВход SET ");
            ErrorLabel4.Text = am.error;
            Workers();
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            ErrorLabel2.Text = "";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            ErrorLabel3.Text = "";
        }

        private void dataGridView7_Click(object sender, EventArgs e)
        {
            ErrorLabel4.Text = "";
        }

        private void Refresh_Button2_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues2, dataGridView2, "UPDATE Клиенты SET ");
            ErrorLabel2.Text = am.error;
            LoadClients();
        }

        private void Refresh_Button3_Click(object sender, EventArgs e)
        {
            am.UpdateDB(readerValues3, dataGridView3, "UPDATE ВыданныеАвто SET ");
            ErrorLabel3.Text = am.error;
            LoadOrders();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            am.LogOut(this as Form);
        }
    }
}
