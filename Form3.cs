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
    public partial class Form3 : MaterialForm
    {
        string keyuser;
        string keyfines;

        public string[] readerValues1 = new string[6]; //авто
        public string[] readerValues2 = new string[3]; //штрафы
        public string[] readerValues3 = new string[5];// выданные авто
        public string[] readerValues4 = new string[2]; // цены
        public string[] readerValues5 = new string[2]; // скидки
        AccessManager am = new AccessManager();
        public Form3(string keyloginvalue)
        {
            keyuser = keyloginvalue;
            InitializeComponent();
            am.MaterialDesignSettings(this as MaterialForm);
            am.DgvSet(dataGridView1);
            am.DgvSet(dataGridView2);
            pictureBox1.Image = Image.FromFile("./img/logout.png");
            materialLabel1.FontType = MaterialSkinManager.fontType.H6;
            materialLabel2.FontType = MaterialSkinManager.fontType.Subtitle2;
            materialLabel3.FontType = MaterialSkinManager.fontType.Subtitle2;

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

        public void LoadOrders()
        {
            //выданные авто
            readerValues3[0] = "Код_заказа";
            readerValues3[1] = "Марка_автомобиля";
            readerValues3[2] = "Фамилия_клиента";
            readerValues3[3] = "Дата_выдачи";
            readerValues3[4] = "Дата_возврата";
            LoadDB(readerValues3, dataGridView5, "SELECT ВыданныеАвто.[Код_заказа], ВыданныеАвто.[Марка_автомобиля], ВыданныеАвто.[Фамилия_клиента], ВыданныеАвто.[Дата_выдачи], ВыданныеАвто.[Дата_возврата] FROM ВыданныеАвто");
        }

        public void LoadDiscounts()
        {
            //выданные авто
            readerValues5[0] = "Код";
            readerValues5[1] = "Sum-Размер_скидки";
            LoadDB(readerValues5, dataGridView4, "SELECT Клиенты.Код, Sum(Скидки.Размер_скидки) AS [Sum-Размер_скидки] FROM Скидки INNER JOIN(Клиенты INNER JOIN[Выданные_скидки] ON Клиенты.[Код] = [Выданные_скидки].[Фамилия_клиента]) ON Скидки.[Код_скидки] = [Выданные_скидки].[Наименование_скидки] GROUP BY Клиенты.Код HAVING Клиенты.Код = " + keyuser);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
            LoadAvto();
            LoadOrders();
            dataGridView5.Rows.Clear();
            dataGridView5.Rows[0].Cells[0].Value = (Int32.Parse(am.keymax)+1).ToString();
            dataGridView5.Rows[0].Cells[2].Value = keyuser;
            LoadDiscounts();
        }
        public void LoadFines()
        {
            int index = am.IndexSelected(dataGridView1);
            keyfines = dataGridView1.Rows[index].Cells[0].Value.ToString();
            //авто
            readerValues2[0] = "Марка_автомобиля";
            readerValues2[1] = "Наименование_штрафа";
            readerValues2[2] = "Размер_штрафа";
            LoadDB(readerValues2, dataGridView2, "SELECT ВыданныеШтрафы.[Марка_автомобиля], Наименование_штрафа.Наименование_штрафа, ВыданныеШтрафы.[Размер_штрафа] FROM Наименование_штрафа INNER JOIN ВыданныеШтрафы ON Наименование_штрафа.Код = ВыданныеШтрафы.Наименование_штрафа WHERE ВыданныеШтрафы.[Марка_автомобиля] = "+ keyfines);

        }
        private void SelectAvto_Click(object sender, EventArgs e)
        {
            LoadFines();
            dataGridView5.Rows[0].Cells[1].Value = keyfines;
            DateTime now = DateTime.Now;
            dataGridView5.Rows[0].Cells[3].Value = now;
            dataGridView5.Rows[0].Cells[4].Value = "Null";
            errorLabel.Text = am.error;
            AddOrder.Visible = true;

            //price discount
            int index = am.IndexSelected(dataGridView1);
            Price0Box.Text = Convert.ToString(dataGridView1.Rows[index].Cells[3].Value);
            Price1Box.Text = Convert.ToString((1-Convert.ToSingle(dataGridView4.Rows[0].Cells[1].Value)/100)* Convert.ToSingle(Price0Box.Text));
        }

        private void AddOrder_Click(object sender, EventArgs e)
        {
            am.AddDB(readerValues3.Length, dataGridView5, "INSERT INTO ВыданныеАвто");
            ErrorLabel2.Text = am.error;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            am.LogOut(this as Form);
        }
    }
}
