namespace ReflexSmurf;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void StartGameButton_Clicked(object sender, EventArgs e)
    {
        // Store the name entered by the user in the UserData class
        UserData.UserName = UserNameEntry.Text;

        GamePage gamePage = new();
        await Navigation.PushAsync(gamePage);
    }

    private async void ViewScoresButton_Clicked(object sender, EventArgs e)
    {
        ScoreBoardPage scoreBoardPage = new();
        await Navigation.PushAsync(scoreBoardPage);
    }
}