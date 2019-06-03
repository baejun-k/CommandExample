using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandSample.Commands {
	public class AsyncRelayCommand : ICommand {
		private readonly Func<object, Task> _handler;
		private bool _canExecute = true;

		public AsyncRelayCommand(Func<object, Task> task)
		{
			_handler = task;
		}


		private void SetCanExecute(bool canExecute)
		{
			if (_canExecute != canExecute) {
				_canExecute = canExecute;
				RaiseCanExecuteChanged();
			}
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) => _canExecute;

		public async void Execute(object parameter)
		{
			try {
				if (CanExecute(null) == false) { return; }
				SetCanExecute(false);
				await _handler(parameter);
			}
			catch (Exception) {
				// TODO: 예외처리 해줄게 있으면 해준다.
			}
			finally {
				SetCanExecute(true);
			}
		}

		private static void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
	}
}
