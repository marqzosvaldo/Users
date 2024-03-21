using System.Diagnostics;
using Users.Models;
using Users.Services;

namespace Users.Views;

public partial class AddUserPage : ContentPage
{
	public AddUserPage()
	{
		InitializeComponent();
	}

	async Task AddUser() {
        var service = new UsersService();
        try {
            var user = new User {
                name = xentryname.Text,
                email = xentryemail.Text,
                gender = "male",
                status = "active"
            };
            var response = await service.AddUser(user);
            if(response != null) {
                await Shell.Current.DisplayAlert("Agregado", "Usuario agregado correctamente", "Ok");

                await Shell.Current.Navigation.PopAsync();

                return;
            }

            await Shell.Current.DisplayAlert("Error", "error inesperado al agregar un usuario", "Ok");
        } catch (Exception ex) {

            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Error", "error inesperado al agregar un usuario", "Ok");

        }
    }
    private async void Button_Clicked(object sender, EventArgs e) {
        var button = (Button)sender;
        button.IsEnabled = false;
        xentryname.IsEnabled = false ;
        await AddUser();
        button.IsEnabled = true;
        xentryname.IsEnabled = true;


    }
}