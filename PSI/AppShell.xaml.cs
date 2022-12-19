using PSI.UserAuthentication;
using PSI.Verification;
using PSI.Views;
using PSI.Views.ManageLocation;

namespace PSI;

public partial class AppShell : Shell
{

	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddLocationView), typeof(AddLocationView));
        Routing.RegisterRoute(nameof(DeleteLocationView), typeof(DeleteLocationView));
        Routing.RegisterRoute(nameof(ReportView), typeof(ReportView));
        Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
        Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
        Routing.RegisterRoute(nameof(ReportDetailPage), typeof(ReportDetailPage));
        Routing.RegisterRoute(nameof(LocationsView), typeof(LocationsView));
        Routing.RegisterRoute(nameof(PlatformVerification), typeof(PlatformVerification));

    }
}
