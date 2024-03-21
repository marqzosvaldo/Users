using System.Collections.ObjectModel;
using System.Diagnostics;
using Users.Models;
using Users.Services;

namespace Users.Views;

public partial class DevicesPage : ContentPage{
    ObservableCollection<Models.Device> _devices;
    public DevicesPage(){
		InitializeComponent();
	}
    public Models.Device LastDeviceAdded { get; set; }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetDevices();

        
    }

    async Task GetDevices() {
        xrefresh.IsRefreshing = true;

        var service = new DeviceService();

        try {
            var response = await service.GetDevices();

            _devices = new ObservableCollection<Models.Device>(response);

            //if (_currentAddDevicePage != null) {
            //    _devices.Add(_currentAddDevicePage.LastDeviceAdded);
            //    _currentAddDevicePage.LastDeviceAdded = null;
            //}
            if(LastDeviceAdded != null) {
            
                _devices.Add(new Models.Device() {
                    id = LastDeviceAdded.id,
                    name = LastDeviceAdded.name
                });
         
                //LastDeviceAdded = null;
            }

            xdevicecollection.ItemsSource = _devices;
            if (_devices.Count == 0) {
                await Shell.Current.DisplayAlert(title: "Error", "error inesperado al cargar dispositivos", "Ok");
                xrefresh.IsRefreshing = false;

            }

            xrefresh.IsRefreshing = false;
        } catch (Exception ex) {

            await Shell.Current.DisplayAlert(title: "Error", "error inesperado al cargar dispositivos", "Ok");
            xrefresh.IsRefreshing = false;


            Debug.WriteLine(ex.Message);
        }
    }

    async void xdevicecollection_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e){
        var user = e.CurrentSelection.FirstOrDefault() as Models.Device;

        

    }

    async void xrefresh_Refreshing(System.Object sender, System.EventArgs e){
        await GetDevices();
    }
    
    async void Button_Clicked(System.Object sender, System.EventArgs e){
        //_currentAddDevicePage = new AddDevicePage(this);

        await Shell.Current.Navigation.PushModalAsync(new AddDevicePage(this));
    }
}
