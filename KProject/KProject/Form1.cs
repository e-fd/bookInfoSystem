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
    public partial class Form1 : Form // ���� ������� ������ ���� � ����������
    {
        Book[] books = new Book[1000]; // ������ ����
        public Form1() // ����������� ���� ������� ������ ���� � ����������
        {
            InitializeComponent(); // ������������� ���������� - �������� ����, �������� ���������
            string fileName = "Default.db"; // ��������� ���� ������
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {                           // �������� ����� ��������� ���� ������
                    using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                    {                   // ������ ����� ��������� ���� ������
                        listBox1.Items.Clear(); // ������� ������ ����
                        while (!sr.EndOfStream) // ������ ����� �� �����
                        {
                            string[] buffer = sr.ReadLine().Split('|'); // ������ ������ ����� 
                                    // � ���������� ������� ������ ��������, ����������� '|',
                                                                        // � ������
                            listBox1.Items.Add(" \"" + buffer[0] + "\"  " + buffer[1]);
                                                                // ���������� ������ ����
                            Book book = new Book(buffer[0], buffer[1], buffer[2], buffer[3]);
                            // �������� ������ ������� ������ Book, ���������� ���� ��� ����������
                            books[book.BookId] = book;
                        }
                    }
                }
            }
            catch { } // ���� �� �� ���������� � ������ ������; ������ ���� �������� ������
        }

        private void �������������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {           // �����, ������������� ��� ������� '������������� / �������� �����'
            AddBook addBook = new AddBook();            // �������� ������ ������� ���� ���������� ����� �����
            addBook.ShowDialog();                       // �������� ���� ���������� ����� �����
            if (addBook.ReturnFlag())                   // ���� ������ "��������" ���� ������
            { // �������� ������ ������� ������ Book, ���������� ���� ��� ����������
                Book book = new Book(addBook.ReturnTitle(), addBook.ReturnAutor(),
                    addBook.ReturnGenre(), addBook.ReturnDescription());
                books[book.BookId] = book;                    // ������ ���������� ������� � ������ ����
                listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author); // ���������� ������ ����
            }
            addBook.Close();        // �������� ���� ���������� ����� �����
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        { // �����, ������������� ��� ������� '���� / �������'
            var sourceFileOpenFileDialog = new OpenFileDialog // ������������� ������� ������ OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"���� ���� ������(*.db)|*.db", // ������ ���������� ������������ �����
                RestoreDirectory = true, // ���������� ���� �������� ���������� ���������� �����
                Multiselect = false, // ������������ �� ����� ������� ����� ������ �����
                Title = @"������� ���� ���� ������" // �������� ����������� ����
            };

            if (sourceFileOpenFileDialog.ShowDialog() == DialogResult.OK) // ���� ������������ ������ ����
            {                                                           // � ����� ������ "�������",
                try
                { // ������������ ��������, ������ �����
                    string fileName = sourceFileOpenFileDialog.FileName;

                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                        { // ������� ��������� ����� ������ ����
                            listBox1.Items.Clear();
                            while (!sr.EndOfStream) // ������ ���� � ���� ������� ������ ���� � ����������
                            { // ����������� ������� �� ������ �����, � ����� ������� ����������� � ������ books
                                string[] buffer = sr.ReadLine().Split('|');
                                listBox1.Items.Add(" \"" + buffer[0] + "\"  " + buffer[1]);
                                Book book = new Book(buffer[0], buffer[1], buffer[2], buffer[3]);
                                books[book.BookId] = book;
                            }
                        }
                    }
                }
                catch (Exception ex) // ����� ��������� ��� ������������� ������ �������� �����
                {
                    MessageBox.Show(@"������: ���������� ������� ����. " + ex.Message);
                }
            }
        }

        private void ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        { // �����, ������������� ��� ������� '���� / ���������'
            var sourceFileSFD = new SaveFileDialog // ������������� ������� ������ SaveFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"���� ���� ������ (*.db)|*.db", // ������ ���������� ������������ �����
                RestoreDirectory = true, // ���������� ���� �������� ���������� ���������� �����
                Title = @"�������� ���� � ������� � ������" // �������� ����������� ����
            };
            if (sourceFileSFD.ShowDialog() == DialogResult.OK) // ���� ������������ ������ ����
            {                                                  // � ����� ������ "���������",
                try
                { // ������������ �������� �����,
                    using (var fs = new FileStream(sourceFileSFD.FileName, FileMode.Create, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                        {
                            foreach (var book in books)
                            { // ������ ���� ����� ��������� ������� books � ����������� ����
                                if (book != null)
                                {
                                    string outputString = book.Author + "|" + book.Title + "|"
                                        + book.Genre + "|" + book.Description + "|";
                                    sw.WriteLine(outputString);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex) // ����� ��������� ��� ������������� ������ ���������� �����
                {
                    MessageBox.Show(@"������: �� ������� ��������� ����. " + ex.Message);
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { // �����, ������������� ��� �������� ���� ������� ������ ���� � ����������
            using (var fs = new FileStream("Default.db", FileMode.Create, FileAccess.Write))
            {                                                   // ������ � ���� ��������� ��
                using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                {
                    foreach (var book in books)
                    { // ������ ���� ����� ��������� ������� books � ���� ��������� �� 
                        if (book != null)
                        {
                            string outputString = book.Title + "|" + book.Author + "|"
                                 + book.Genre+ "|" + book.Description+ "|";
                            sw.WriteLine(outputString);
                        }
                    }
                }
            }
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        { // �����, ������������� ��� ������� '���� / �����'
            this.Close(); // �������� ���� ������� ������ ���� � ����������
        }

        private void ������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        { // �����, ������������� ��� ������� '������������� / ������������� �����'
            AddBook addBook = new AddBook(); // �������� ������ ������� ���� ���������� ����� �����
            int index = listBox1.SelectedIndex; // ����������, ������� ������������� �������� 
            if (index != -1)                    // ������� ��������� ��� �������������� �����
            {                                                     // ������������ ��������� ����� ���� ����������                addBook.SetTitle(books[index].Title);
                addBook.SetTitle(books[index].Title);             // ����� ����� �������� ��������
                addBook.SetAuthor(books[index].Author);           //
                addBook.SetGenre(books[index].Genre);             //
                addBook.SetDescription(books[index].Description); //
                addBook.ShowDialog();                             // �������� ���� ���������� ����� �����
                if (addBook.ReturnFlag())                         // ���� ������ "��������" ���� ������
                {                                                 
                    books[index].Title = addBook.ReturnTitle();             // ��������� �������� ����� ���������� 
                    books[index].Author = addBook.ReturnAutor();            // �������� �������
                    books[index].Genre = addBook.ReturnGenre();             // 
                    books[index].Description = addBook.ReturnDescription(); //
                    //listBox1.Items.Insert(index, " \"" + books[index].Title + "\"  " + books[index].Author);
                    //listBox1.Items.Remove(index + 1);
                    listBox1.Items.Clear();         // ������� ������ ����
                    foreach (var book in books) // ��� ������� �������� (book) ������� books
                    {
                        if (book != null)       // ���� ������� book �� ������
                        {                       // �������� ������ ����, ��������� ���� ��������� ������� books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        }
                    }
                }
            }
            addBook.Close(); // �������� ���� ���������� ����� �����
        }

        private void button1_Click(object sender, EventArgs e)
        { // �����, ������������� ��� ������ �� ��������� �����

        }
    }

}
