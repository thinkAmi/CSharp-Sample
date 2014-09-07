using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Collections.ObjectModel;   // ObservableCollection
using System.IO.Ports;                  // SerialPort
using System.Runtime.CompilerServices;  // CallerMemberName属性
using System.Windows.Media;             // Brush
using System.Management;                // ManagementClass

namespace SerialPortReceiver
{
    public partial class SerialPortViewModel : ViewModelBase
    {
        private SerialPort serialPort;


        private void ExecuteStartCommand(object x)
        {
            if (serialPort == null)
            {
                serialPort = new SerialPort(SelectedComPort.DeviceID, 9600, Parity.None, 8, StopBits.One);

                // 改行コードまでUIに反映されないよう、システムの改行コードを設定しておく
                serialPort.NewLine = Environment.NewLine;
            }

            serialPort.Open();
            serialPort.DataReceived += (s, e) =>
            {
                var readData = serialPort.ReadLine();

                // ViewModelBaseのDispatcherプロパティを使って、UIスレッドに値を渡す
                Dispatcher.Invoke(new Action(() =>
                {
                    if (DataGridSource == null)
                    {
                        DataGridSource = new ObservableCollection<SerialPortModel>();
                    }
                    DataGridSource.Add(new SerialPortModel() { ReadData = readData });
                }));
            };

            UpdateStatus();
        }

        private bool CanExecuteStartCommand(object x)
        {
            // SerialPortが開いていない時だけ有効化
            return serialPort == null || !serialPort.IsOpen;
        }

        private void ExecuteStopCommand(object x)
        {
            serialPort.Close();
            UpdateStatus();
        }

        private bool CanExecuteStopCommand(object x)
        {
            // SerialPortが開いている時だけ有効化
            return serialPort.IsOpen;
        }


        private string _status;
        public string Status
        {
            get
            {
                if (_status == null)
                {
                    UpdateStatus();
                }
                return _status;
            }
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        private Brush _statusBackground;
        public Brush StatusBackground
        {
            get { return _statusBackground; }
            set
            {
                _statusBackground = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ComPort> _comPorts;
        public ObservableCollection<ComPort> ComPorts
        {
            get
            {
                if (_comPorts == null)
                {
                    _comPorts = GetComPorts();
                    RaisePropertyChanged();
                }
                return _comPorts;
            }
        }

        private ComPort _selectedComPort;
        public ComPort SelectedComPort
        {
            get { return _selectedComPort; }
            set
            {
                _selectedComPort = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<SerialPortModel> _dataGridSource;
        public ObservableCollection<SerialPortModel> DataGridSource
        {
            get
            {
                return _dataGridSource;
            }
            set
            {
                _dataGridSource = value;
                //  ObservableCollection型でも値が変更されたらRaisePropertyChanged()を呼ぶ
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<ComPort> GetComPorts()
        {
            var results = new ObservableCollection<ComPort>();

            var mc = new ManagementClass("Win32_SerialPort");
            foreach (var m in mc.GetInstances()) using (m)
                {
                    results.Add(new ComPort()
                    {
                        DeviceID = (string)m.GetPropertyValue("DeviceID"),
                        Description = (string)m.GetPropertyValue("Caption")
                    });
                }
            return results;
        }

        private void UpdateStatus()
        {
            if (serialPort == null || !serialPort.IsOpen)
            {
                Status = "Closed";
                StatusBackground = Brushes.LightGray;
            }
            else
            {
                Status = "Receiving...";
                StatusBackground = Brushes.LimeGreen;
            }
        }


        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }
    }
}
