using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace KProject
{
    public partial class Form1 : Form
    {
        Book[] books = new Book[1000];
        public Form1()
        {
            InitializeComponent();
            string fileName = "Default.db";
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                    {
                        listBox1.Items.Clear();

                        while (!sr.EndOfStream)
                        {
                            string[] buffer = sr.ReadLine().Split('|');

                            listBox1.Items.Add(buffer[0] + "  " + buffer[1]);
                            Book book = new Book(buffer[0], buffer[1]);
                            books[book.BookId] = book;
                        }
                    }
                }
            }
            catch { }
        }

        private void добавить нигуToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.ShowDialog();
            if (addBook.ReturnFlag())
            {
                Book book = new Book(addBook.ReturnTitle(), addBook.ReturnAutor());
                books[book.BookId] = book;
                listBox1.Items.Add(book.Author + "  " + book.Title);
            }
            addBook.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceFileOpenFileDialog = new OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"‘айл базы данных(*.db)|*.db",
                RestoreDirectory = true,
                Multiselect = false,
                Title = @"ќткрыть файл базы данных"
            };

            if (sourceFileOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = sourceFileOpenFileDialog.FileName;

                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                        {
                            listBox1.Items.Clear();

                            while (!sr.EndOfStream)
                            {
                                string[] buffer = sr.ReadLine().Split('|');

                                listBox1.Items.Add(buffer[0] + "  " + buffer[1]);
                                Book book = new Book(buffer[0], buffer[1]);
                                books[book.BookId] = book;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"ќшибка: невозможно открыть файл. " + ex.Message);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourseFileSFD = new SaveFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"‘айл базы данных (*.db)|*.db",
                RestoreDirectory = true,
                Title = @"«аписать файл с данными о книгах"
            };
            if (sourseFileSFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var fs = new FileStream(sourseFileSFD.FileName, FileMode.Create, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                        {
                            foreach (var book in books)
                            {
                                if (book != null)
                                {
                                    string outputString = book.Author + "|" + book.Title + "|";
                                    sw.WriteLine(outputString);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"ќшибка: не удаетс€ открыть файл. " + ex.Message);
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var fs = new FileStream("Default.db", FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                {
                    foreach (var book in books)
                    {
                        if (book != null)
                        {
                            string outputString = book.Author + "|" + book.Title + "|";
                            sw.WriteLine(outputString);
                        }
                    }
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void редактировать нигуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                addBook.SetTitle(books[index].Title);
                addBook.SetAuthor(books[index].Author);
                addBook.ShowDialog();
                if (addBook.ReturnFlag())
                {
                    books[index].Author = addBook.ReturnAutor();
                    books[index].Title = addBook.ReturnTitle();
                    listBox1.Items.Insert(index, books[index].Author + "  " + books[index].Title);
                    listBox1.Items.Remove(index);
                }
            }
            addBook.Close();
        }
    }

}
