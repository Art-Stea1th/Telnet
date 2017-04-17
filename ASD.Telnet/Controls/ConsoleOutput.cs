using System.Windows.Controls;


namespace ASD.Telnet.Controls {

    public partial class ConsoleOutput : TextBox {

        protected override void OnTextChanged(TextChangedEventArgs e) {
            base.OnTextChanged(e);
            ScrollToEnd();
        }
    }
}