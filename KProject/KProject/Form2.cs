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
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Неизвестно");
            comboBox1.Items.Add("Проза");
            comboBox1.Items.Add("Поэзия");
            comboBox1.Items.Add("Приключения");
            comboBox1.Items.Add("Драма");
            comboBox1.Items.Add("Фэнтези");
            comboBox1.Items.Add("Научная фантастика");
            comboBox1.Items.Add("Биография и мемуары");
            comboBox1.Items.Add("Мифы и легенды");
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book book = new Book(textBox1.Text, textBox2.Text, richTextBox1.Text, comboBox1.SelectedText);
            Form1.books[book.BookId] = book;
        }
    }
}
