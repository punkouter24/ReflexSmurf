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

    private async void ViewScoresButton_Clicked(object sender, EventArgs e)
    {
        var scoreBoardPage = new ScoreBoardPage();
        await Navigation.PushAsync(scoreBoardPage);
    }
}



