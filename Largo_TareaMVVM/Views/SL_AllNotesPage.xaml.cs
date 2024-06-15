namespace Largo_TareaMVVM.Views;

public partial class SL_AllNotesPage : ContentPage
{
    public SL_AllNotesPage()
    {
        InitializeComponent();

        BindingContext = new SL_Models.SL_AllNotes();
    }

    protected override void OnAppearing()
    {
        ((SL_Models.SL_AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SL_NotePage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (SL_Models.SL_Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(SL_NotePage)}?{nameof(SL_NotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}