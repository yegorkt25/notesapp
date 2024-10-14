using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ModelViews;
using NotesApp.Pages.Main;
using NotesApp.Pages.Registration;
using NotesApp.Views.ExceptionHandler;
using System.Windows;

namespace NotesApp.Pages.Login
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow(LoginModelView modelView)
		{
			InitializeComponent();
			DataContext = modelView;
		}

		private async void SignInAction(object sender, RoutedEventArgs e)
		{
			try
			{
				var user = new UserModel(null, Username.Text, Password.Password);

				var mv = (LoginModelView)DataContext;

				if (await mv.Service.AuthenticateUser(user))
				{
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

		private void SignUpAction(object sender, RoutedEventArgs e)
		{
			var window = App.ServiceProvider.GetRequiredService<RegistrationWindow>();

			window.Show();

			Close();
		}
	}
}
