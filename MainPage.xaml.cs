namespace MemoryGameFinish2024
{
  public partial class MainPage : ContentPage
  {
    // This is a comment!
    // Keep track of how many cards are selected
    // Check if the two cards are a match or not
    // Keep track of whose turn it is
    // Find and add our images

    // Keep track of which cards are selected
    ImageButton firstSelection = new ImageButton();
    ImageButton secondSelection = new ImageButton();

    // Keep track of how many cards are selected
    int selectedCount = 0;

    // Keep track of the turn number
    int numberOfTurns = 0;

    bool playerOneTurn = false;

    // Keeps track of player matches
    int playerOneMatches = 0;
    int playerTwoMatches = 0;

    List<string> cardList = new List<string>()
        {
            "eevee.png",
            "gardevoir.png",
            "gengar.png",
            "pikachu.png",
            "totodile.png",
            "toxapex.png",
            "typhlosion.png",
            "umbreon.png",
            "eevee.png",
            "gardevoir.png",
            "gengar.png",
            "pikachu.png",
            "totodile.png",
            "toxapex.png",
            "typhlosion.png",
            "umbreon.png"
        };

    // List of our mixed up cards
    List<string> shuffledCardList;

    // List of all our image buttons
    List<ImageButton> cardControls = new List<ImageButton>();

    public MainPage()
    {
      InitializeComponent();

      cardControls.Add(Card1);
      cardControls.Add(Card2);
      cardControls.Add(Card3);
      cardControls.Add(Card4);
      cardControls.Add(Card5);
      cardControls.Add(Card6);
      cardControls.Add(Card7);
      cardControls.Add(Card8);
      cardControls.Add(Card9);
      cardControls.Add(Card10);
      cardControls.Add(Card11);
      cardControls.Add(Card12);
      cardControls.Add(Card13);
      cardControls.Add(Card14);
      cardControls.Add(Card15);
      cardControls.Add(Card16);

      // Shuffle the cards
      Random randomizer = new Random();

      // Take the unshuffled card list, use the randomizer to shuffle, and then
      // assign it to the shuffled card list!
      shuffledCardList = cardList.OrderBy(card => randomizer.Next()).ToList();
    }

    private async void CardButton_Clicked(object sender, EventArgs e)
    {
      // Grab the card that was pressed, rename it currentCard
      ImageButton currentCard = (ImageButton)sender;

      // If the current card has no image, give it one from the shuffled card list
      if (currentCard.Source == null)
      {
        currentCard.Source = shuffledCardList[int.Parse(currentCard.ClassId) - 1];
      }
      else
      {
        currentCard.Source = null;
      }

      // Check if we're on the first or second card that's selected
      if (selectedCount == 0)
      {
        firstSelection = currentCard;
      }
      else if (selectedCount == 1)
      {
        secondSelection = currentCard;
      }

      selectedCount++;

      // Check if we have two cards selected
      if (selectedCount == 2)
      {
        ButtonShuffle.IsEnabled = false;
        CardsGrid.IsEnabled = false;
        await Task.Delay(1000);
        CardsGrid.IsEnabled = true;
        ButtonShuffle.IsEnabled = true;

        // We have a match!
        if (firstSelection.Source?.ToString() == secondSelection.Source?.ToString())
        {
          firstSelection.BackgroundColor = Colors.Green;
          secondSelection.BackgroundColor = Colors.Green;

          if (playerOneTurn)
          {
            playerOneMatches++;
            playerOneTurn = false;
          }
          else
          {
            playerTwoMatches++;
            playerOneTurn = true;
          }

          firstSelection.IsEnabled = false;
          secondSelection.IsEnabled = false;
        }
        else
        {
          firstSelection.Source = null;
          secondSelection.Source = null;

          if (playerOneTurn)
          {
            playerOneTurn = false;
          }
          else
          {
            playerOneTurn = true;
          }
        }

        selectedCount = 0;
        numberOfTurns++;
        LabelNumberOfTurns.Text = numberOfTurns.ToString();
      }

      if (playerOneMatches + playerTwoMatches == 8)
      {
        await DisplayAlert("Game done!", $"Player 1 got {playerOneMatches} matches! Player 2 got {playerTwoMatches} matches!", "Close");
      }
    }

    private void ButtonShuffle_Clicked(object sender, EventArgs e)
    {
      // Reset all main variables
      firstSelection.Source = null;
      secondSelection.Source = null;

      selectedCount = 0;
      numberOfTurns = 0;
      playerOneMatches = 0;
      playerTwoMatches = 0;
      LabelNumberOfTurns.Text = "0";

      playerOneTurn = true;

      // Shuffle the board again
      Random random = new Random();
      shuffledCardList = cardList.OrderBy(card => random.Next()).ToList();

      // Reset the cards in the UI
      foreach (var card in cardControls)
      {
        card.Source = null;
        card.BackgroundColor = Colors.White;
        card.IsEnabled = true;
      }
    }
  }

}
