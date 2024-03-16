using DataAccess.Repositories;
using System.Windows;
using DataAccess.Entities;
using LibraryAPI;
namespace LibraryWpf;

/// <summary>
/// Interaction logic for AddAuthor.xaml
/// </summary>
public partial class AddAuthor : Window
{
    public AddAuthor()
    {
        InitializeComponent();

    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void idTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        idTextBox.Text = "";
    }
    private void nameTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        nameTextBox.Text = "";
    }
    private void surenameTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        surenameTextBox.Text = "";
    }

    private async void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        Author author = new Author();
        if (idTextBox.Text != "Id")
        {
            author.Id = long.Parse(idTextBox.Text);
        }
        author.Name = nameTextBox.Text;
        author.Surename = surenameTextBox.Text;

        var dbcf = new LibraryDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var authorRepository = new AuthorRepository(context);
        await authorRepository.CreateAsync(author);
        MessageBox.Show("Author added succesfully");
        this.Close();

    }
}
