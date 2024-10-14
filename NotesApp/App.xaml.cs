using BLL;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.ModelViews;
using NotesApp.Pages.Login;
using NotesApp.Pages.Main;
using NotesApp.Pages.Registration;
using System.Windows;

namespace NotesApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static ServiceProvider ServiceProvider {  get; private set; }
		public static UserModel CurrentUser { get; set; }
		protected override void OnStartup(StartupEventArgs e)
		{
			ServiceCollection services = new ServiceCollection();
			services.AddDbContext<NotesAppContext>(options => { options.UseNpgsql("Host=localhost;Database=notesapp;Username=postgres;Password=12345678"); });

			services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile(new AutomapperProfile());
			});
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<INoteRepository, NoteRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<INoteService, NoteService>();

			services.AddScoped<LoginModelView>();
			services.AddScoped<RegistrationModelView>();
			services.AddTransient<MainWindowModelView>();

			services.AddTransient<LoginWindow>();
			services.AddTransient<RegistrationWindow>();
			services.AddTransient<MainWindow>();

			ServiceProvider = services.BuildServiceProvider();
			var loginWindow = ServiceProvider.GetService<LoginWindow>();

			loginWindow?.Show();
			base.OnStartup(e);
		}
	}

}
