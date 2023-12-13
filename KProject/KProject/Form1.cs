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
    public partial class Form1 : Form // окно Системы поиска книг в библиотеке
    {
        Book[] books = new Book[1000]; // массив книг
        public Form1() // конструктор окна Системы поиска книг в библиотеке
        {
            InitializeComponent(); // инициализация компонента - открытие окна, загрузка элементов
            string fileName = "Default.db"; // дефолтная база данных
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {                           // открытие файла дефолтной базы данных
                    using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                    {                   // чтение файла дефолтной базы данных
                        listBox1.Items.Clear(); // очистка списка книг
                        while (!sr.EndOfStream) // чтение файла до конца
                        {
                            string[] buffer = sr.ReadLine().Split('|'); // чтение строки файла 
                                    // и сохранение каждого набора символов, разделенных '|',
                                                                        // в массив
                            listBox1.Items.Add(" \"" + buffer[0] + "\"  " + buffer[1]);
                                                                // пополнение списка книг
                            Book book = new Book(buffer[0], buffer[1], buffer[2], buffer[3]);
                            // создание нового объекта класса Book, заполнение всех его параметров
                            books[book.BookId] = book;
                        }
                    }
                }
            }
            catch { } // файл БД не загрузится в случае ошибки; список книг окажется пустым
        }

        private void добавитьКнигуToolStripMenuItem_Click_1(object sender, EventArgs e)
        {           // метод, срабатывающий при нажатии 'редактировать / добавить книгу'
            AddBook addBook = new AddBook();            // создание нового объекта окна Добавление новой книги
            addBook.ShowDialog();                       // открытие окна Добавление новой книги
            if (addBook.ReturnFlag())                   // если кнопка "Добавить" была нажата
            { // создание нового объекта класса Book, заполнение всех его параметров
                Book book = new Book(addBook.ReturnTitle(), addBook.ReturnAutor(),
                    addBook.ReturnGenre(), addBook.ReturnDescription());
                books[book.BookId] = book;                    // запись созданного объекта в массив книг
                listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author); // пополнение списка книг
            }
            addBook.Close();        // закрытие окна Добавление новой книги
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'файл / открыть'
            var sourceFileOpenFileDialog = new OpenFileDialog // инициализация объекта класса OpenFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"Файл базы данных(*.db)|*.db", // фильтр расширения открываемого файла
                RestoreDirectory = true, // диалоговое окно запомнит директорию выбранного файла
                Multiselect = false, // пользователь не может выбрать более одного файла
                Title = @"Открыть файл базы данных" // название диалогового окна
            };

            if (sourceFileOpenFileDialog.ShowDialog() == DialogResult.OK) // если пользователь выбрал файл
            {                                                           // и нажал кнопку "Открыть",
                try
                { // производится открытие, чтение файла
                    string fileName = sourceFileOpenFileDialog.FileName;

                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var sr = new StreamReader(fs/*, Encoding.GetEncoding(1251)*/))
                        { // очистка открытого ранее списка книг
                            listBox1.Items.Clear();
                            while (!sr.EndOfStream) // список книг в окне Система поиска книг в библиотеке
                            { // заполняется книгами из нового файла, и новые объекты добавляются в массив books
                                string[] buffer = sr.ReadLine().Split('|');
                                listBox1.Items.Add(" \"" + buffer[0] + "\"  " + buffer[1]);
                                Book book = new Book(buffer[0], buffer[1], buffer[2], buffer[3]);
                                books[book.BookId] = book;
                            }
                        }
                    }
                }
                catch (Exception ex) // вывод сообщения при возникновении ошибки открытия файла
                {
                    MessageBox.Show(@"Ошибка: невозможно открыть файл. " + ex.Message);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'файл / сохранить'
            var sourceFileSFD = new SaveFileDialog // инициализация объекта класса SaveFileDialog
            {
                //InitialDirectory = @"C:\",
                Filter = @"Файл базы данных (*.db)|*.db", // фильтр расширения сохраняемого файла
                RestoreDirectory = true, // диалоговое окно запомнит директорию выбранного файла
                Title = @"Записать файл с данными о книгах" // название диалогового окна
            };
            if (sourceFileSFD.ShowDialog() == DialogResult.OK) // если пользователь выбрал файл
            {                                                  // и нажал кнопку "Сохранить",
                try
                { // производится создание файла,
                    using (var fs = new FileStream(sourceFileSFD.FileName, FileMode.Create, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                        {
                            foreach (var book in books)
                            { // запись всех полей элементов массива books в сохраняемый файл
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
                catch (Exception ex) // вывод сообщения при возникновении ошибки сохранения файла
                {
                    MessageBox.Show(@"Ошибка: не удается сохранить файл. " + ex.Message);
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        { // метод, срабатывающий при закрытии окна Система поиска книг в библиотеке
            using (var fs = new FileStream("Default.db", FileMode.Create, FileAccess.Write))
            {                                                   // запись в файл дефолтной БД
                using (var sw = new StreamWriter(fs/*, Encoding.GetEncoding("Windows - 1251")*/))
                {
                    foreach (var book in books)
                    { // запись всех полей элементов массива books в файл дефолтной БД 
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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'файл / выход'
            this.Close(); // закрытие окна Система поиска книг в библиотеке
        }

        private void редактироватьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'редактировать / редактировать книгу'
            AddBook addBook = new AddBook(); // создание нового объекта окна Добавление новой книги
            int index = listBox1.SelectedIndex; // переменная, которой присваивается значение 
            if (index != -1)                    // индекса выбранной для редактирования книги
            {                                                     // присваивание текстовым полям окна Добавление                addBook.SetTitle(books[index].Title);
                addBook.SetTitle(books[index].Title);             // новой книги заданных значений
                addBook.SetAuthor(books[index].Author);           //
                addBook.SetGenre(books[index].Genre);             //
                addBook.SetDescription(books[index].Description); //
                addBook.ShowDialog();                             // открытие окна Добавление новой книги
                if (addBook.ReturnFlag())                         // если кнопка "Добавить" была нажата
                {                                                 
                    books[index].Title = addBook.ReturnTitle();             // изменение значений полей выбранного 
                    books[index].Author = addBook.ReturnAutor();            // элемента массива
                    books[index].Genre = addBook.ReturnGenre();             // 
                    books[index].Description = addBook.ReturnDescription(); //
                    //listBox1.Items.Insert(index, " \"" + books[index].Title + "\"  " + books[index].Author);
                    //listBox1.Items.Remove(index + 1);
                    listBox1.Items.Clear();         // очистка списка книг
                    foreach (var book in books) // для каждого элемента (book) массива books
                    {
                        if (book != null)       // если элемент book не пустой
                        {                       // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        }
                    }
                }
            }
            addBook.Close(); // закрытие окна Добавление новой книги
        }

        private void button1_Click(object sender, EventArgs e)
        { // метод, срабатывающий при поиске по заголовку книги

        }
    }

}
