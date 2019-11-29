using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandSample.Commands {
	/// <summary>
	/// 비동기 작업을 위한 Command
	/// </summary>
	public class AsyncRelayCommand : ICommand {
		private readonly Func<object, Task> _handler;
		private bool _isRunning;
		private Func<object, bool> _canExecuteCompare;

		/// <summary>
		/// 특정 행동을 하는 핸들러를 받아 생성
		/// </summary>
		/// <param name="handler"></param>
		public AsyncRelayCommand(Func<object, Task> handler, Func<object, bool> canExecuteCompare = null)
		{
			_handler = handler;
			_canExecuteCompare = canExecuteCompare;
			_isRunning = false;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return !_isRunning && (_canExecuteCompare?.Invoke(parameter) ?? true);
		}

		public async void Execute(object parameter)
		{
			if (CanExecute(parameter) && _handler != null) {
				_isRunning = true;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				await _handler(parameter);
				_isRunning = false;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
