using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.ContactsContract.CommonDataKinds;


namespace Largo_TareaMVVM.SL_Models
{
    internal class SL_AllNotes
    {
        public ObservableCollection<SL_Note> Notes { get; set; } = new ObservableCollection<SL_Note>();

        public SL_AllNotes() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<SL_Note> notes = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new SL_Note()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetLastWriteTime(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Date);

            // Add each note into the ObservableCollection
            foreach (SL_Note note in notes)
                Notes.Add(note);
        }
    }
}
