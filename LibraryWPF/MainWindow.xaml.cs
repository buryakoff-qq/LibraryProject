using DataAccess.Repositories;
using System.Windows;
using DataAccess.Entities;
using LibraryAPI;

namespace LibraryWpf;

public partial class MainWindow : Window
{


    public MainWindow()
    {
        InitializeComponent();
    }

    private void AuthorId_OnClick(object sender, RoutedEventArgs e)
    {
        authorID.Text = "";

    }
    private void BookId_OnClick(object sender, RoutedEventArgs e)
    {
        bookID.Text = "";
    }
    private async void ListAllAuthors_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new LibraryDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new AuthorRepository(context);
        var repo2 = new BookRepository(context);
        //authorList.Items.Clear();

        var authors = await repo.ReadAllAsync();
        authorList.ItemsSource = authors;

    }
    private async void ListAllBooks_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new LibraryDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new BookRepository(context);
        var books = await repo.ReadAllAsync();
        bookList.ItemsSource = books;
    }

    private void CreateAuthor_OnClick(object sender, RoutedEventArgs e)
    {

        AddAuthor newWindow = new AddAuthor();
        newWindow.ShowDialog();
        ListAllAuthors_OnClick(sender, e);
    }
    private void CreateBook_OnClick(object sender, RoutedEventArgs e)
    {
        AddBook newWindow = new AddBook();
        newWindow.ShowDialog();
        ListAllBooks_OnClick(sender, e);
    }
    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (authorList.SelectedItem == null)
        {
            MessageBox.Show("Please select an author to update");
        }
        else
        {
            Author author = new Author();
            author = (Author)authorList.SelectedItem;
            UpdateAuthor newWindow = new UpdateAuthor(author);
            newWindow.ShowDialog();
            ListAllAuthors_OnClick(sender, e);
        }

    }
    private void UpdateBookButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (bookList.SelectedItem == null)
        {
            MessageBox.Show("Please select a book to update");
        }
        else
        {
            Book book = new Book();
            book = (Book)bookList.SelectedItem;
            UpdateBook newWindow = new UpdateBook(book);
            newWindow.ShowDialog();
            ListAllBooks_OnClick(sender, e);
        }
    }

    private async void ListById_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new AppDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new AuthorRepository(context);
        if (int.TryParse(authorID.Text, out int id))
        {
            var author = await repo.ReadAsync(id);
            if (author != null)
            {
                authorList.ItemsSource = new List<Author> { author };
            }
            else
            {
                MessageBox.Show("Author not found");
            }
        }
        else
        {
            MessageBox.Show("Please enter a valid id");
            return;
        }
    }

    private async void ListBookById_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new AppDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new BookRepository(context);
        if (int.TryParse(bookID.Text, out int id))
        {
            var book = await repo.ReadAsync(id);
            if (book != null)
            {
                bookList.ItemsSource = new List<Book> { book };
            }
            else
            {
                MessageBox.Show("Book not found");
            }
        }
        else
        {
            MessageBox.Show("Please enter a valid id");
            return;
        }
    }

    private void DeleteSelected_OnClick(object sender, RoutedEventArgs e)
    {
        if (authorList.SelectedItem == null)
        {
            MessageBox.Show("Please select an author to delete");
        }
        else
        {
            Author selection = new Author();
            selection = (Author)authorList.SelectedItem;
            //string[] selectedAuthorString = authorList.SelectedItem.ToString().Split(" ");
            var dbcf = new AppDbContextFactory();
            using var context = dbcf.CreateDbContext([]);
            var repo = new AuthorRepository(context);
            repo.DeleteAsync(selection.Id);
            MessageBox.Show("Author deleted succesfully");
            ListAllAuthors_OnClick(sender, e);
        }
    }
    private void DeleteSelectedBook_OnClick(object sender, RoutedEventArgs e)
    {
        if (bookList.SelectedItem == null)
        {
            MessageBox.Show("Please select a book to delete");
        }
        else
        {
            Book selection = new Book();
            selection = (Book)bookList.SelectedItem;
            var dbcf = new AppDbContextFactory();
            using var context = dbcf.CreateDbContext([]);
            var repo = new BookRepository(context);
            repo.DeleteAsync((long)selection.Id);
            MessageBox.Show("Book deleted succesfully");
            ListAllBooks_OnClick(sender, e);
        }
    }
    private void ShowAuthorUI()
    {
        createButton.Visibility = Visibility.Visible;
        updateButton.Visibility = Visibility.Visible;
        listAllButton.Visibility = Visibility.Visible;
        listByIdButton.Visibility = Visibility.Visible;
        deleteButton.Visibility = Visibility.Visible;
        authorID.Visibility = Visibility.Visible;
        authorList.Visibility = Visibility.Visible;
        bookButton.Visibility = Visibility.Visible;
        authorButton.Visibility = Visibility.Hidden;
        createBookButton.Visibility = Visibility.Hidden;
        updateBookButton.Visibility = Visibility.Hidden;
        listAllBooksButton.Visibility = Visibility.Hidden;
        listByIdBookButton.Visibility = Visibility.Hidden;
        deleteBookButton.Visibility = Visibility.Hidden;
        bookID.Visibility = Visibility.Hidden;
        bookList.Visibility = Visibility.Hidden;
        ListAllAuthors_OnClick(this, null);
    }
    private void ShowBookUI()
    {
        createButton.Visibility = Visibility.Hidden;
        updateButton.Visibility = Visibility.Hidden;
        listAllButton.Visibility = Visibility.Hidden;
        listByIdButton.Visibility = Visibility.Hidden;
        deleteButton.Visibility = Visibility.Hidden;
        authorID.Visibility = Visibility.Hidden;
        authorList.Visibility = Visibility.Hidden;
        bookButton.Visibility = Visibility.Hidden;
        authorButton.Visibility = Visibility.Visible;
        createBookButton.Visibility = Visibility.Visible;
        updateBookButton.Visibility = Visibility.Visible;
        listAllBooksButton.Visibility = Visibility.Visible;
        listByIdBookButton.Visibility = Visibility.Visible;
        deleteBookButton.Visibility = Visibility.Visible;
        bookID.Visibility = Visibility.Visible;
        bookList.Visibility = Visibility.Visible;
        ListAllBooks_OnClick(this, null);
    }
    private void bookButton_OnClick(object sender, RoutedEventArgs e)
    {
        ShowBookUI();
    }

    private void authorButton_OnClick(object sender, RoutedEventArgs e)
    {
        ShowAuthorUI();
    }


}