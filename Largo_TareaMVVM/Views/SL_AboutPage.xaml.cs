namespace Largo_TareaMVVM.Views;

public partial class SL_AboutPage : ContentPage
{
	public SL_AboutPage()
	{
		InitializeComponent();
	}

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is SL_Models.SL_About about)
        {
            await Launcher.Default.OpenAsync("https://aka.ms/maui");
        }
    }
}