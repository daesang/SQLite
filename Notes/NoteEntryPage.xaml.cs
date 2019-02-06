using System;
using Xamarin.Forms;
using Notes.Models;
using System.Threading.Tasks;

namespace Notes
{
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();

            //var note = (Note)BindingContext;

            //var note2 = App.Database.GetNoteAsync<Note>(note.ID);
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;
            //note.PW = "test";

            await App.Database.SaveNoteAsync(note);
            //if (note.ID != 0)
            //{
            //    await App.Database.UpdateNoteAsync<Note>(note);
            //}
            //else
            //{
            //    await App.Database.InsertNoteAsync<Note>(note);
            //}

            //await App.Database.InsertOrReplaceAsync<Note>(note);

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync<Note>(note.ID);
            await Navigation.PopAsync();
        }
    }
}
