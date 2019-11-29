using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using CommandSample.Commands;
using CommandSample.ViewModels;

namespace CommandSample {
	public class MainWindowViewModel : ViewModelBase {
		private ObservableCollection<string> _logList;

		public MainWindowViewModel()
		{
			_logList = new ObservableCollection<string>();
			WireCommands();
		}

		/// <summary>
		/// Command를 엮어준다
		/// </summary>
		private void WireCommands()
		{
			ShowMessageBoxCommand = 
				new RelayCommand(ShowMessageBoxCommandHandler);
			WriteLogCommand =
				new AsyncRelayCommand(WriteLogCommandHandler);
		}
		
		public RelayCommand ShowMessageBoxCommand { get; private set; }

		public AsyncRelayCommand WriteLogCommand { get; private set; }

		/// <summary>
		/// MainWindow의 LogList에 바인딩 될 property
		/// </summary>
		public ObservableCollection<string> LogList {
			get { return _logList; }
			set {
				if(_logList != value) {
					_logList = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// 메시지박스 출력
		/// </summary>
		/// <param name="arg"></param>
		private void ShowMessageBoxCommandHandler(object arg)
		{
			MessageBox.Show("메시지 상자 출력", "Command 예제", 
				MessageBoxButton.OK, MessageBoxImage.Information);
		}

		/// <summary>
		/// 비동기로 2초에 걸쳐 3개의 로그를 작성
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		private Task WriteLogCommandHandler(object arg)
		{
			int numBgn = _logList.Count + 1;
			Action<string> addMethod = _logList.Add;

			return Task.Run(() => {
				Debug.WriteLine($"Test Log {numBgn}");
				Application.Current.Dispatcher.Invoke(addMethod, $"Test Log {numBgn}");
				Thread.Sleep(1000);
				Debug.WriteLine($"Test Log {numBgn + 1}");
				Application.Current.Dispatcher.Invoke(addMethod, $"Test Log {numBgn + 1}");
				Thread.Sleep(1000);
				Debug.WriteLine($"Test Log {numBgn + 2}");
				Application.Current.Dispatcher.Invoke(addMethod, $"Test Log {numBgn + 2}");
			});
		}
	}
}
