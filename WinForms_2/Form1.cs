using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinForms_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [OnSerializing]
        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                int day = int.Parse(textBox_Day.Text);
                int month = int.Parse(textBox_Month.Text);
                int year = int.Parse(textBox_Year.Text);
            
                Info info = new Info
                {
                    Surname = textBox_Surname.Text,
                    Name = textBox_Name.Text,
                    Patronymic = textBox_Patronymic.Text,
                    Sex = textBox_Sex.Text,
                    DateOfBirth = new DateTime(year, month, day),
                    FamilyStatus = textBox_Status.Text,
                    AdditionalInfo = textBox_Info.Text
                };
                FileStream fileStream = new FileStream("Info.xml", FileMode.Create, FileAccess.Write);
                XmlSerializer serializer = new XmlSerializer(typeof(Info));
                serializer.Serialize(fileStream, info);
                fileStream.Close();
                MessageBox.Show("Данные загружены!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            // очищаем поля
            textBox_Surname.Text = string.Empty;
            textBox_Name.Text = string.Empty;
            textBox_Patronymic.Text = string.Empty;
            textBox_Sex.Text = string.Empty;
            textBox_Day.Text = string.Empty;
            textBox_Month.Text = string.Empty;
            textBox_Year.Text = string.Empty;
            textBox_Status.Text = string.Empty;
            textBox_Info.Text = string.Empty;
        }
        private void button_Load_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            FileStream fileStream = new FileStream("Info.xml", FileMode.Open, FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(Info));
            info = (Info)serializer.Deserialize(fileStream);
            fileStream.Close();
            textBox_Name.Text = info.Name;
            textBox_Surname.Text = info.Surname;
            textBox_Patronymic.Text = info.Patronymic;
            textBox_Sex.Text = info.Sex;
            textBox_Status.Text = info.FamilyStatus;
            textBox_Info.Text = info.AdditionalInfo;
            textBox_Day.Text = info.DateOfBirth.Day.ToString();
            textBox_Month.Text = info.DateOfBirth.Month.ToString();
            textBox_Year.Text = info.DateOfBirth.Year.ToString();
        }
        [Serializable]
        public class Info
        {
            public string Surname { get; set; }       // Фамилия
            public string Name { get; set; }          // Имя
            public string Patronymic { get; set; }    // Отчество
            public string Sex { get; set; }           // Пол
            public DateTime DateOfBirth { get; set; } // Год и дата рождения
            public string FamilyStatus { get; set; }  // Семейный статус
            public string AdditionalInfo { get; set; }// Дополнительная информация
            public Info() { } // для сериализации должен быть конструктор не принимающий параметров
        }
    }
}
