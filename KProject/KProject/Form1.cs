namespace KProject
{
    public partial class Form1 : Form
    {
        public Book[] books;
        public Form1()
        {
            InitializeComponent();
        }
        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            addBook.Show();
        }
    }
}
