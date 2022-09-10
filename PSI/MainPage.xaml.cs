namespace PSI;

public partial class MainPage : ContentPage
{
	private int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void onCounterClicked(object sender, EventArgs e)
	{
		count++;
		if (count == 1)
			counterBtn.Text = $"Clicked {count} time";
		else
			counterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(counterBtn.Text);
	}

	private void onTextChanged(object sender, EventArgs e)
	{
		hiLabel.Text = myEditor.Text;
	}
}
