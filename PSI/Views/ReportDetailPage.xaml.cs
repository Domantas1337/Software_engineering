using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportDetailPage : ContentPage
{

    private int timePassed = 0;
    public ReportDetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	public void OnReportScrolled(object sender, ScrolledEventArgs e)
	{

    }
}