namespace PSI.Views;

public partial class ReportView : ContentPage
{
	private string _report;
	public ReportView()
	{
		InitializeComponent();
		BindingContext = this;
	}

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("..");
    }

    public string Report
    {
        get => _report;
        set
        {
            _report = value;

            if(Regex.IsMatch(_report, "(?i)fuck") || Regex.IsMatch(_report, "(?i)shit"))
            {
                invalidInputMessage.Text = "No curse words";
            }
            else
            {
                invalidInputMessage.Text = "";
            }

        }
    }
}