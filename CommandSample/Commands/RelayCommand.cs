using System;
using System.Windows.Input;

namespace CommandSample.Commands {
	/// <summary>
	/// 일반적인 Command
	/// </summary>
	public class RelayCommand : ICommand {
		private readonly Action<object> _handler;
		private bool _isRunning;
		private Func<object, bool> _canExecuteCompare;

		/// <summary>
		/// 특정 행동을 하는 핸들러를 받아 생성
		/// </summary>
		/// <param name="handler"></param>
		public RelayCommand(Action<object> handler, Func<object, bool> canExecuteCompare = null)
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

		public void Execute(object parameter)
		{
			if (CanExecute(parameter)) {
				_isRunning = true;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				_handler?.Invoke(parameter);
				_isRunning = false;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
