using Users.Models;

namespace Users.Views;

public partial class UserDetails : ContentPage
{
    private User? user;

    public UserDetails()
	{
		InitializeComponent();
	}

    public UserDetails(User? user) {
        InitializeComponent();

        this.user = user;

        xname.Text = user.name;
        xemail.Text = user.email;
        xid.Text = user.id.ToString();
        xgender.Text = user.gender;
        xstatus.Text = user.status;

    }
}