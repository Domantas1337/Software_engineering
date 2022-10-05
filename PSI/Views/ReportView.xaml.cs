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

            if(Regex.IsMatch(_report, "(?i)(shit)|(fuc(k)?)|(nig(ga)?)"))
            {
                invalidInputMessage.Text = "Curse words are not allowed!";
            }
            else
            {
                invalidInputMessage.Text = "";
            }

        }
    }
}