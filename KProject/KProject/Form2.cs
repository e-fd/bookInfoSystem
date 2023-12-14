using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KProject
{
    public partial class AddBook : Form // окно Добавление новой книги
    {
        bool Flag = false; // false - добавления книги в БД  не происходит,
                           // true - книга добавляется в БД
        public AddBook() // конструктор окна Добавление новой книги
        {
            InitializeComponent(); // инициализация компонента - открытие окна, загрузка элементов
            comboBox1.Items.Clear(); // очистка списка жанров книг
            comboBox1.Items.Add("Неизвестно");                 // добавление жанров книг
            comboBox1.Items.Add("Роман-эпопея");               //
            comboBox1.Items.Add("Роман");                      //
            comboBox1.Items.Add("Повесть");                    //
            comboBox1.Items.Add("Рассказ");                    //
            comboBox1.Items.Add("Притча");                     //
            comboBox1.Items.Add("Лирическое стихотворение");   //
            comboBox1.Items.Add("Элегия");                     //
            comboBox1.Items.Add("Послание");                   //
            comboBox1.Items.Add("Эпиграмма");                  //
            comboBox1.Items.Add("Ода");                        //
            comboBox1.Items.Add("Сонет");                      //
            comboBox1.Items.Add("Комедия");                    //
            comboBox1.Items.Add("Трагедия");                   //
            comboBox1.Items.Add("Драма");                      //
            comboBox1.Items.Add("Поэма");                      //
            comboBox1.Items.Add("Баллада");                    //
            comboBox1.SelectedIndex = 0;                // индекс изначально выбранного элемента
        }
        public void SetTitle(string Title) // (1) метод для присвоения значения
        {                                  // записываемого текста в поле 
            textBox1.Text = Title;
        }
        public string? ReturnTitle() // (2) метод для возврата записанного
        {                            // в поле текста
            return textBox1.Text;
        }
        public void SetAuthor(string Author) // (1)
        {
            textBox2.Text = Author;
        }
        public string? ReturnAutor() // (2)
        {
            return textBox2.Text;
        }
        public void SetGenre(string Genre) // (1)
        {
            comboBox1.Text = Genre;
        }
        public string? ReturnGenre() // (2)
        {
            return comboBox1.Text;
        }
        public void SetDescription(string Description) // (1)
        {
            richTextBox1.Text = Description;
        }
        public string? ReturnDescription() // (2)
        {
            return richTextBox1.Text;
        }
        public bool ReturnFlag() // метод для возвращения значения переменной Flag
        {
            return Flag;
        }
        private void button2_Click(object sender, EventArgs e)
        {                // закрытие окна при нажатии кнопки "Отмена"
            this.Hide(); // (введенные данные не сохраняются
        }                // и книга не добавляется в БД (Flag == false))
        private void button1_Click(object sender, EventArgs e)
        {                // при нажатии кнопки "Добавить" происходит изменение значения
            Flag = true; // переменной Flag (и, соответственно, добавление книги в список)
            this.Hide(); // и закрытие окна
        }
    }
}
