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
            comboBox1.Items.Clear(); // очистка списка жанров книг
            comboBox1.Items.Add("");                           // добавление жанров книг
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
            comboBox1.SelectedIndex = 0;    // индекс изначально выбранного элемента
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
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                            books[book.BookId] = book;
                        }
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
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
                book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
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
                                book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                                books[book.BookId] = book;
                            }
                        }
                    }
                }
                catch (Exception ex) // вывод сообщения при возникновении ошибки открытия файла
                {
                    MessageBox.Show(@"Ошибка: невозможно открыть файл: " + ex.Message);
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
                                    string outputString = book.Title + "|" + book.Author + "|"
                                        + book.Genre + "|" + book.Description + "|";
                                    sw.WriteLine(outputString);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex) // вывод сообщения при возникновении ошибки сохранения файла
                {
                    MessageBox.Show(@"Ошибка: не удается сохранить файл: " + ex.Message);
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
                                 + book.Genre + "|" + book.Description + "|";
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
            {                                                     
                foreach (var book in books)
                {
                    if ((index + 1) == book.GetListIndex())
                    {
                        index = book.BookId;
                        break;
                    }
                }                                                 // присваивание текстовым полям окна Добавление
                addBook.SetTitle(books[index].Title);             // значений выбранной книги
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
                    listBox1.Items.Clear();         // очистка списка книг
                    foreach (var book in books) // для каждого элемента (book) массива books
                    {
                        if (book != null)       // если элемент book не пустой
                        {                       // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                        }
                    }
                    toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                       // количества книг в списке
                }
            }
            addBook.Close(); // закрытие окна Добавление новой книги
        }

        private void удалитьКнигуToolStripMenuItem_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'редактировать / удалить книгу'
            int index = listBox1.SelectedIndex; // переменная, которой присваивается значение 
            if (index != -1)                    // индекса выбранной для удаления книги
            {
                bool bookRemove = false;
                int i = 0;
                int num = 0;
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        if ((index + 1) == book.GetListIndex())
                        {
                            index = book.BookId;
                            bookRemove = true;
                            num = book.GetBookCounter();
                            book.SetBookCounter(num - 1);
                        }
                        if (bookRemove == true)
                        {
                            books[i] = books[i + 1];
                            if (books[i] != null)
                            {
                                books[i].BookId--;
                                int n = books[i].GetListIndex() - 1;
                                books[i].SetListIndex(n);
                            }
                        }
                    }
                    i++;
                }
                books[num] = null;
                listBox1.Items.Clear();         // очистка списка книг
                foreach (var book in books) // для каждого элемента (book) массива books
                {
                    if (book != null)       // если элемент book не пустой
                    {                       // заполяем список книг, используя поля элементов массива books
                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
            }
        }
        private void button1_Click(object sender, EventArgs e)
        { // метод, срабатывающий при поиске по заголовку книги
            string substring = textBox1.Text;
            if (substring.Length > 0)
            {
                listBox1.Items.Clear();
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        if (book.Title.IndexOf(substring) >= 0)
                        { // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                        }
                        toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                           // количества книг в списке
                    }
                }
            }
            else
            {
                listBox1.Items.Clear();         // очистка списка книг
                foreach (var book in books) // для каждого элемента (book) массива books
                {
                    if (book != null)       // если элемент book не пустой
                    {                       // заполяем список книг, используя поля элементов массива books
                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { // метод, срабатывающий при поиске по автору книги
            string substring = textBox2.Text;
            if (substring.Length > 0)
            {
                listBox1.Items.Clear();
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        if (book.Author.IndexOf(substring) >= 0)
                        { // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                        }
                        toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                           // количества книг в списке
                    }
                }
            }
            else
            {
                listBox1.Items.Clear();         // очистка списка книг
                foreach (var book in books) // для каждого элемента (book) массива books
                {
                    if (book != null)       // если элемент book не пустой
                    {                       // заполяем список книг, используя поля элементов массива books
                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { // метод, срабатывающий при поиске по жанру книги
            string substring = comboBox1.Text;
            if (substring.Length > 0)
            {
                listBox1.Items.Clear();
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        if (book.Genre.IndexOf(substring) >= 0)
                        { // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                        }
                        toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                           // количества книг в списке
                    }
                }
            }
            else
            {
                listBox1.Items.Clear();         // очистка списка книг
                foreach (var book in books) // для каждого элемента (book) массива books
                {
                    if (book != null)       // если элемент book не пустой
                    {                       // заполяем список книг, используя поля элементов массива books
                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
            }

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void chosenParameterSearch_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'поиск по всем параметрам'
            string substring1 = textBox1.Text;
            string substring2 = textBox2.Text;
            string substring3 = comboBox1.Text;
            if (substring1.Length > 0 && substring2.Length > 0 && comboBox1.SelectedIndex != 0)
            { // все три поля заполнены пользователем
                listBox1.Items.Clear();
                foreach (var book in books)
                {
                    if (book != null)
                    {
                        if (book.Title.IndexOf(substring1) >= 0 && 
                            book.Author.IndexOf(substring2) >= 0 &&
                            book.Genre.IndexOf(substring3) >= 0)
                        { // заполяем список книг, используя поля элементов массива books
                            listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                            book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                        }
                        toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                           // количества книг в списке
                    }
                }
            }
            else
            {
                if (substring1.Length > 0 && substring2.Length > 0 && comboBox1.SelectedIndex == 0)
                { // заполнены заголовок и автор
                    listBox1.Items.Clear();
                    foreach (var book in books)
                    {
                        if (book != null)
                        {
                            if (book.Title.IndexOf(substring1) >= 0 &&
                                book.Author.IndexOf(substring2) >= 0)
                            { // заполяем список книг, используя поля элементов массива books
                                listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                                book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                            }
                            toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                               // количества книг в списке
                        }
                    }
                }
                else
                {
                    if (substring1.Length == 0 && substring2.Length > 0 && comboBox1.SelectedIndex != 0)
                    { // заполнены автор и жанр
                        listBox1.Items.Clear();
                        foreach (var book in books)
                        {
                            if (book != null)
                            {
                                if (book.Genre.IndexOf(substring3) >= 0 &&
                                    book.Author.IndexOf(substring2) >= 0)
                                { // заполяем список книг, используя поля элементов массива books
                                    listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                                    book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                                }
                                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                                   // количества книг в списке
                            }
                        }
                    }
                    else
                    {
                        if (substring1.Length > 0 && substring2.Length == 0 && comboBox1.SelectedIndex != 0)
                        { // заполнены заголовок и автор
                            listBox1.Items.Clear();
                            foreach (var book in books)
                            {
                                if (book != null)
                                {
                                    if (book.Title.IndexOf(substring1) >= 0 &&
                                        book.Genre.IndexOf(substring3) >= 0)
                                    { // заполяем список книг, используя поля элементов массива books
                                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                                    }
                                    toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                                       // количества книг в списке
                                }
                            }
                        }
                        else
                        {
                            if (substring1.Length > 0 && substring2.Length == 0 && comboBox1.SelectedIndex == 0)
                            { // заполнен заголовок
                                this.button1_Click(sender, e); // вызов метода, срабатывающего при поиске по заголовку книги
                            }
                            if (substring1.Length == 0 && substring2.Length > 0 && comboBox1.SelectedIndex == 0)
                            { // заполнен автор
                                this.button2_Click(sender, e); // вызов метода, срабатывающего при поиске по автору книги
                            }
                            if (substring1.Length == 0 && substring2.Length == 0 && comboBox1.SelectedIndex != 0)
                            { // заполнен жанр
                                this.button3_Click(sender, e); // вызов метода, срабатывающего при поиске по жанру книги
                            }
                        }
                    }
                }
                }

            if (substring1.Length == 0 && substring2.Length == 0 && comboBox1.SelectedIndex == 0)
            { // поля не заполнены
                listBox1.Items.Clear();         // очистка списка книг
                foreach (var book in books) // для каждого элемента (book) массива books
                {
                    if (book != null)       // если элемент book не пустой
                    {                       // заполяем список книг, используя поля элементов массива books
                        listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                        book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                    }
                }
                toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                                   // количества книг в списке
            }
        }

        private void clearTextBox_Click(object sender, EventArgs e)
        { // метод, срабатывающий при нажатии 'очистить параметры поиска'
            textBox1.Text = "";             // очистка полей поиска
            textBox2.Text = "";             // 
            comboBox1.SelectedIndex = 0;    //
            listBox1.Items.Clear();         // очистка списка книг
            foreach (var book in books) // для каждого элемента (book) массива books
            {
                if (book != null)       // если элемент book не пустой
                {                       // заполяем список книг, используя поля элементов массива books
                    listBox1.Items.Add(" \"" + book.Title + "\"  " + book.Author);
                    book.SetListIndex(listBox1.Items.Count); // установка значения listIndex
                }
            }
            toolStripStatusLabel3.Text = "Элементов: " + listBox1.Items.Count; // установка значения
                                                                               // количества книг в списке
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { // метод, срабытывающий при изменении значения индекса выбранной книги из списка listBox1
          // вывод информации о выбранной книге в таблице
            toolStripStatusLabel2.Text = listBox1.SelectedItem as string; // изменение текста в элементе
                                         // в statusStrip1; отображение названия и автора выбранной книги
            int index = listBox1.SelectedIndex; // переменная, которой присваивается значение 
            if (index != -1)                    // индекса выбранной для просмотра книги
            {
                foreach (var book in books)
                {
                    if ((index + 1) == book.GetListIndex())
                    {
                        index = book.BookId;
                        label8.Text = book.Title;              // заполнение полей таблицы 
                        label9.Text = book.Author;             // соответствующими значениями
                        label10.Text = book.Genre;             //
                        textBox3.Text = book.Description;      //
                        break;
                    }
                }
            }
        }
    }

}
