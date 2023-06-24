namespace ReflexSmurf;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void StartGameButton_Clicked(object sender, EventArgs e)
    {
        var gamePage = new GamePage();
        await Navigation.PushAsync(gamePage);
    }
}

