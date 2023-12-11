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
        bool Flag = false;
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
        public void SetAuthor(string Author)
        {
            textBox2.Text = Author;
        }
        public string? ReturnAutor()
        {
            return textBox2.Text;
        }
        public void SetTitle(string Title)
        {
            textBox1.Text = Title;
        }
        public string? ReturnTitle()
        {
            return textBox1.Text;
        }

        public bool ReturnFlag()
        {
            return Flag;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Flag = true;
            this.Hide();
        }
    }
}
