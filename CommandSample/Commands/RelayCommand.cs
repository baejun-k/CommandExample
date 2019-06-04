using System;
using System.Windows.Input;

namespace CommandSample.Commands {
	/// <summary>
	/// 일반적인 Command
	/// </summary>
	public class RelayCommand : ICommand {
		private readonly Action<object> _handler;
		private bool _canExecute = true;

		/// <summary>
		/// 특정 행동을 하는 핸들러를 받아 생성
		/// </summary>
		/// <param name="handler"></param>
		public RelayCommand(Action<object> handler)
		{
			_handler = handler;
		}

		private void SetCanExecute(bool canExecute)
		{
			if (_canExecute != canExecute) {
				_canExecute = canExecute;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => _canExecute;

		public void Execute(object parameter)
		{
			if (CanExecute(null) == false) { return; }
			SetCanExecute(false);
			_handler?.Invoke(parameter);
			SetCanExecute(true);
		}
	}
}
