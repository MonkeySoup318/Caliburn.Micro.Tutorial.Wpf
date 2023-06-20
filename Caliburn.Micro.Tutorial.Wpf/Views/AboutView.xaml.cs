using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Caliburn.Micro.Tutorial.Wpf.Views
{
    public partial class AboutView : Window
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // You need a workaround here for .Net Core:
            //  https://github.com/dotnet/runtime/issues/28005
            var psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
