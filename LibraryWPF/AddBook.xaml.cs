using DataAccess.Entities;
using DataAccess.Repositories;
using LibraryAPI;
using System.Text.RegularExpressions;
using System.Windows;

namespace LibraryWpf
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook()
        {
            InitializeComponent();
            InitializeAuthors();
        }
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void idTextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            idTextBox.Text = "";
        }
        private void titleTextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            titleTextBox.Text = "";
        }
        private void genreTextBox_OnFocus(object sender, RoutedEventArgs e)
        {
            genreTextBox.Text = "";
        }

        private async void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Book book = new Book();
            Regex text = new Regex(@"^[A-Za-z]{4,}$");
            Regex digit = new Regex(@"^[0-9]$");
            if (!text.IsMatch(titleTextBox.Text) || !text.IsMatch(genreTextBox.Text))
            {
                MessageBox.Show("Please enter a valid Title and Genre");
                return;
            }
            //if (!digit.IsMatch(idTextBox.Text))
            //{
            //    MessageBox.Show("Please enter a valid ID");
            //    return;
            //}

            if (authorSelection.SelectedItem == null)
            {
                MessageBox.Show("Please select an author");
                return;
            }

            book.Title = titleTextBox.Text;
            book.Genre = genreTextBox.Text;
            book.AuthorId = ((Author)authorSelection.SelectedItem).Id;
            var dbcf = new LibraryDbContextFactory();
            using var context = dbcf.CreateDbContext([]);
            var repo = new BookRepository(context);
            await repo.CreateAsync(book);
            MessageBox.Show("Book added successfully");
            this.Close();

        }
        public async Task InitializeAuthors()
        {
            InitializeComponent();
            var dbcf = new LibraryDbContextFactory();
            using var context = dbcf.CreateDbContext([]);
            var repo = new AuthorRepository(context);
            var author = await repo.ReadAllAsync();
            IEnumerable<Author> authorList = author;

            authorSelection.ItemsSource = authorList;
            authorSelection.DisplayMemberPath = "Authordata";
        }
    }
}
