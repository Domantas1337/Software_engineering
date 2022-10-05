using PSI.Views;

namespace PSI;

public partial class AppShell : Shell
{

	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddLocationView), typeof(AddLocationView));
        Routing.RegisterRoute(nameof(ReportView), typeof(ReportView));
        Routing.RegisterRoute(nameof(SelectionView), typeof(SelectionView));
    }
}
