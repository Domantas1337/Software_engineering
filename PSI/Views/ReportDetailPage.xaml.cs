using PSI.ViewModels;

namespace PSI.Views;

public partial class ReportDetailPage : ContentPage
{
	public ReportDetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}