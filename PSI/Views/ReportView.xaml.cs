using PSI.Models;
using PSI.Generators;
using PSI.FileManagers;

namespace PSI.Views;

public partial class ReportView : ContentPage
{
    private string _title;
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

    async void OnAddReportButtonClicked(object sender, EventArgs e)
    {
        ReportItem reportItem = new ReportItem()
        {
            ID = IDGenerator.generateID(),
            Title = this.Title,
            Report = this.Report
        };
        WriteJSON<ReportItem>.write(Constants.reportsFilePath, reportItem);

        await Shell.Current.GoToAsync("..");
    }

    public string Report
    {
        get => _report;
        set
        {
            _report = value;

            if(Regex.IsMatch(_report, "(?i)(shit)|(fuc(k)?)"))
            {
                invalidInputMessage.Text = "Curse words are not allowed!";
            }
            else
            {
                invalidInputMessage.Text = "";
            }

        }
    }
    public string Title { get; set; }

}