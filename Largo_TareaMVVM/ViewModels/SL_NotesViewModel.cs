using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Largo_TareaMVVM.SL_Models;
using System.Reflection;
using System.Windows.Input;

namespace Largo_TareaMVVM.ViewModels
{
    internal class SL_NotesViewModel : IQueryAttributable
    {

        public ObservableCollection<ViewModels.SL_NoteViewModel> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public SL_NotesViewModel()
        {
            AllNotes = new ObservableCollection<ViewModels.SL_NoteViewModel>(SL_Models.SL_Note.LoadAll().Select(n => new SL_NoteViewModel(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModels.SL_NoteViewModel>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.SL_NotePage));
        }

        private async Task SelectNoteAsync(ViewModels.SL_NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.SL_NotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                SL_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                SL_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new SL_NoteViewModel(SL_Models.SL_Note.Load(noteId)));
            }
        }

    }
}
