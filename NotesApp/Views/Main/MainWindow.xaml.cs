using BLL.Models;
using NotesApp.ModelViews;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace NotesApp.Pages.Main
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<NoteModel> Notes {  get; set; }
		public MainWindow(MainWindowModelView modelView)
		{
			InitializeComponent();
			DataContext = modelView;
			Notes = new ObservableCollection<NoteModel>();
			NotesList.ItemsSource = Notes;
			foreach (var item in modelView.Notes)
			{
				Notes.Add(item);
			}

			Title.LostFocus += ChangeNote;
			Text.LostFocus += ChangeNote;
		}

		private async void ChangeNote(object sender, RoutedEventArgs e)
		{
			var selected = (NoteModel)NotesList.SelectedItem;
			var i = Notes.IndexOf(selected);
			Notes[i].Title = Title.Text;
			Notes[i].Text = Text.Text;
			NotesList.Items.Refresh();
			var mv = (MainWindowModelView)DataContext;
			await mv.Service.UpdateAsync(selected);
		}

		private async void AddNote(object sender, RoutedEventArgs e)
		{
			var newNote = new NoteModel(null, "New Note", "New Note", App.CurrentUser.Id ?? throw new InvalidOperationException());
			Notes.Add(newNote);
			NotesList.Items.Refresh();
			var mv = (MainWindowModelView)DataContext;
			await mv.Service.AddAsync(newNote);
		}

		private async void DeleteNote(object sender, RoutedEventArgs e)
		{
			var selected = (NoteModel)NotesList.SelectedItem;
			if (selected != null)
			{
				Notes.Remove(selected);
				NotesList.Items.Refresh();
				var mv = (MainWindowModelView)DataContext;
				await mv.Service.DeleteAsync(selected.Id ?? throw new InvalidOperationException());
			}
		}

		private void NotesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var selected = (NoteModel)NotesList.SelectedItem;
			Title.Text = selected.Title;
			Text.Text = selected.Text;
		}
	}
}
