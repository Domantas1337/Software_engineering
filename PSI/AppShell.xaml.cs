using PSI.UserAuthentication;
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
        Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(ReportDetailPage), typeof(ReportDetailPage));

    }
}
