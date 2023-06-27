namespace ReflexSmurf;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void StartGameButton_Clicked(object sender, EventArgs e)
    {
        GamePage gamePage = new();
        await Navigation.PushAsync(gamePage);
    }

    private async void ViewScoresButton_Clicked(object sender, EventArgs e)
    {
        ScoreBoardPage scoreBoardPage = new();
        await Navigation.PushAsync(scoreBoardPage);
    }
}