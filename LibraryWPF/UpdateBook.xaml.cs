using DataAccess.Entities;
using DataAccess.Repositories;
using System.Windows;

namespace LibraryWpf;

/// <summary>
/// Interaction logic for UpdateBook.xaml
/// </summary>
public partial class UpdateBook : Window
{
    public UpdateBook(Book book)
    {
        InitializeComponent();
        InitializeAuthors(book);
    }
    private async void InitializeAuthors(Book book)
    {
        idTextBox.Text = book.Id.ToString();
        titleTextBox.Text = book.Title;
        genreTextBox.Text = book.Genre;
        var dbcf = new AppDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new AuthorRepository(context);
        var authors = await repo.ReadAllAsync();
        Author author = await repo.ReadAsync((int)book.AuthorId);
        authorSelection.ItemsSource = (IEnumerable<Author>)authors;
        authorSelection.DisplayMemberPath = "Authordata";
        authorSelection.SelectedItem = author;
        //authorSelection.SelectedItem = new List<Author> { book.Author };
    }
    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void titleTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        titleTextBox.Text = "";
    }
    private void genreTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        genreTextBox.Text = "";
    }
    private async void SubmitUpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new AppDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new BookRepository(context);
        Book booktoedit = new Book();
        booktoedit.Id = long.Parse(idTextBox.Text);
        booktoedit.Title = titleTextBox.Text;
        booktoedit.Genre = genreTextBox.Text;
        booktoedit.AuthorId = ((Author)authorSelection.SelectedItem).Id;
        await repo.UpdateAsync(booktoedit);
        MessageBox.Show("Author updated succesfully");
        this.Close();
    }
}
