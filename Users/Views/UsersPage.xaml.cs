using System.Collections.ObjectModel;
using System.Diagnostics;
using Users.Models;
using Users.Services;

namespace Users.Views;

public partial class UsersPage : ContentPage{

    ObservableCollection<User> _users;
	public UsersPage(){
		InitializeComponent();
	}

    protected async override void OnAppearing() {
        base.OnAppearing();
        Debug.WriteLine("OnAppearing UsersPage");
        await GetUsers();
    }

    async Task GetUsers() {
        var service = new UsersService();
        xrefresh.IsRefreshing = true;
        try {
            var response = await service.GetUsers();

            _users = new ObservableCollection<User>(response);
            xuserscview.ItemsSource = _users;

            if(_users.Count == 0) {
                await Shell.Current.DisplayAlert(title: "Error", "error inesperado al cargar usuarios", "Ok");
            }
            xrefresh.IsRefreshing = false;

        } catch (Exception ex) {

            await Shell.Current.DisplayAlert(title: "Error", "error inesperado al cargar usuarios", "Ok");
            xrefresh.IsRefreshing = false;

            Debug.WriteLine(ex.Message);
        }
    }

    private async void xuserscview_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        var user = e.CurrentSelection.FirstOrDefault() as User;

        await Shell.Current.Navigation.PushModalAsync(new UserDetails(user));
        Debug.WriteLine("Sender");
    }

    private async void Button_Clicked(object sender, EventArgs e) {

        await Shell.Current.Navigation.PushAsync(new AddUserPage());

    }

    private async void RefreshView_Refreshing(object sender, EventArgs e) {
        await GetUsers();
    }
}