using DataAccess.Repositories;
using System.Windows;
using DataAccess.Entities;

namespace LibraryWpf;

/// <summary>
/// Interaction logic for UpdateAuthor.xaml
/// </summary>
public partial class UpdateAuthor : Window
{
    public UpdateAuthor(Author author)
    {

        InitializeComponent();
        idTextBox.Text = author.Id.ToString();
        nameTextBox.Text = author.Name;
        surenameTextBox.Text = author.Surename;
    }
    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void nameTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        nameTextBox.Text = "";
    }
    private void surenameTextBox_OnFocus(object sender, RoutedEventArgs e)
    {
        surenameTextBox.Text = "";
    }
    private async void SubmitUpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dbcf = new AppDbContextFactory();
        using var context = dbcf.CreateDbContext([]);
        var repo = new AuthorRepository(context);
        Author author = new Author();
        author.Id = long.Parse(idTextBox.Text);
        author.Name = nameTextBox.Text;
        author.Surename = surenameTextBox.Text;
        await repo.UpdateAsync(author);
        MessageBox.Show("Author updated succesfully");
        this.Close();
    }
}
