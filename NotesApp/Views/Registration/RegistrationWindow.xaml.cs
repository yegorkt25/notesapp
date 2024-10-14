using BLL.Models;
using BLL.Validation;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ModelViews;
using NotesApp.Pages.Login;
using NotesApp.Pages.Main;
using NotesApp.Views.ExceptionHandler;
using System.Windows;

namespace NotesApp.Pages.Registration
{
	/// <summary>
	/// Interaction logic for RegistrationWindow.xaml
	/// </summary>
	public partial class RegistrationWindow : Window
	{
		public RegistrationWindow(RegistrationModelView modelView)
		{
			InitializeComponent();
			DataContext = modelView;
		}

		private void BackAction(object sender, RoutedEventArgs e)
		{
			var window = App.ServiceProvider.GetRequiredService<LoginWindow>();

			window.Show();

			Close();
		}

		private async void SignUpAction(object sender, RoutedEventArgs e)
		{
			try
			{
				if (Password.Password == RptPassword.Password)
				{
					var user = new UserModel(null, Username.Text, Password.Password);
					var mv = (RegistrationModelView)DataContext;
					await mv.Service.AddAsync(user);
					App.CurrentUser = await mv.Service.GetByUsername(user.Username);
					var window = App.ServiceProvider.GetRequiredService<MainWindow>();

					window.Show();

					Close();
				}
			}
			catch (BusinessLogicException ex)
			{
				var model = new ExceptionViewModel() { Message = ex.Message };
				var window = new ExceptionWindow(model);

				window.Show();
			}
			catch (Exception)
			{
				var model = new ExceptionViewModel() { Message = "Unhandled exception" };
				var window = new ExceptionWindow(model);

				window.Show();
			}
		}
	}
}
