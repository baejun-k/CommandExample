using System;
using System.Windows.Input;

namespace CommandSample.Commands {
	public class RelayCommand : ICommand {
		private readonly Action<object> _handler;
		private bool _canExecute = true;

		public RelayCommand(Action<object> task)
		{
			_handler = task;
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
			try {
				if (CanExecute(null) == false) { return; }
				SetCanExecute(false);
				_handler?.Invoke(parameter);
			}
			catch (Exception) {
				// TODO: 예외처리 해줄게 있으면 해준다.
			}
			finally {
				SetCanExecute(true);
			}
		}
	}
}
