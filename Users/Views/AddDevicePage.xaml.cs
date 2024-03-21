using System.Diagnostics;
using Users.Models;
using Users.Services;

namespace Users.Views;

public partial class AddDevicePage : ContentPage
{
    private DevicesPage devicesPage;

    public AddDevicePage(){
		InitializeComponent();
	}

    public AddDevicePage(DevicesPage devicesPage)
    {
        InitializeComponent();

        this.devicesPage = devicesPage;
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e){

        var service = new DeviceService();

        var sdevice = new Models.Device {
			name = xentryname.Text,
			data = null,
        };
        try {

            var response = await service.AddDevice(sdevice);
            if (response != null) {
                await Shell.Current.DisplayAlert("Agregado", "Dispositivo agregado correctamente", "Ok");
                devicesPage.LastDeviceAdded = response;
                await Shell.Current.Navigation.PopAsync();
                return;
            }

            await Shell.Current.DisplayAlert("Error", "error inesperado al agregar un dispositivo", "Ok");

        } catch (Exception ex) {

            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Error", "error inesperado al agregar un usuario", "Ok");

        }
    }
}
