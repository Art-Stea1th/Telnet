namespace ASD.Telnet.ViewModels {

    using Services;

    public sealed class ShellViewModel : ViewModelBase {

        private bool connected = false;

        private IPScanner scanner = null;

        public bool Connected => connected;

        public ShellViewModel() {
            scanner = new IPScanner();
        }
    }
}