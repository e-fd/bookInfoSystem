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
    public partial class Form1 : Form
    {
        Book[] books = new Book[1000];
        public Form1()
        {
            InitializeComponent();
        }

        private void ‰Ó·‡‚ËÚ¸ ÌË„ÛToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.ShowDialog();
            if (addBook.ReturnFlag())
            {
                Book book = new Book(addBook.ReturnTitle(), addBook.ReturnAutor());
                books[book.BookId] = book;
                listBox1.Items.Add(book.Author+"  "+book.Title);
            }
            addBook.Close();
        }
    }

}
