using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ASD.Telnet.ViewModels {

    using Infrastructure.Commands;
    using Infrastructure.Models;
    using Infrastructure.Services;

    public sealed class ShellViewModel : ViewModelBase {

        private SubnetInfo subnetInfo;

        private bool connected;

        private string outputText;
        private string inputText;

        private IEnumerable<string> addresses;

        public bool Connected => connected;

        public string OutputText {
            get => outputText;
            set => Set(ref outputText, value);
        }

        public string InputText {
            get => inputText;
            set => Set(ref inputText, value);
        }

        public IEnumerable<string> Addresses {
            get => addresses;
            set => Set(ref addresses, value);
        }

        public ICommand SendInput => new RelayCommand(SendInputCommandExecute);

        public void SendInputCommandExecute(object parameter) {
            var input = parameter as string;
            if (!string.IsNullOrWhiteSpace(input)) {
                OutputText = input == "cls"
                    ? null
                    : new StringBuilder(outputText).Append($"\n{input}").ToString();
                InputText = string.Empty;
            }
        }

        public ShellViewModel() {
            Initialize();
            InitializeAddressesAsync();
            //PrintHello();
        }

        private void Initialize() {
            subnetInfo = new SubnetInfo();
        }

        private async void InitializeAddressesAsync() {
            Addresses = (await SubnetScanner.ScanRangeAsync(subnetInfo.StartIP, subnetInfo.EndIP)).Select(a => a.ToString());
            PrintHello();
        }

        private void PrintHello() {
            SendInput.Execute("cls");
            SendInput.Execute("Welcome to the simple Telnet Client!\n");
            SendInput.Execute($"Current IP:\t{subnetInfo.Address.ToString()}");
            SendInput.Execute($"Subnet Mask:\t{subnetInfo.SubnetMask.ToString()}");
            SendInput.Execute($"Subnet Range:\t{subnetInfo.StartIP.ToString()} - {subnetInfo.EndIP.ToString()}");
        }
    }
}