namespace Largo_TareaMVVM.Views;

public partial class SL_AllNotesPage : ContentPage
{
    public SL_AllNotesPage()
    {
        InitializeComponent();

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}