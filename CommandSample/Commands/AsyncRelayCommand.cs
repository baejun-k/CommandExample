using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandSample.Commands {
	/// <summary>
	/// 비동기 작업을 위한 Command
	/// </summary>
	public class AsyncRelayCommand : ICommand {
		private readonly Func<object, Task> _handler;
		private bool _canExecute = true;

		/// <summary>
		/// 특정 행동을 하는 핸들러를 받아 생성
		/// </summary>
		/// <param name="handler"></param>
		public AsyncRelayCommand(Func<object, Task> handler)
		{
			_handler = handler;
		}

		#region CanExecute를 다시 확인하도록하는 다른 방법
		//private void SetCanExecute(bool canExecute)
		//{
		//	if (_canExecute != canExecute) {
		//		_canExecute = canExecute;
		//		RaiseCanExecuteChanged();
		//	}
		//}

		//public event EventHandler CanExecuteChanged {
		//	add { CommandManager.RequerySuggested += value; }
		//	remove { CommandManager.RequerySuggested -= value; }
		//}

		///// <summary>
		///// 비동기 작업 상태가 바뀌면 CanExecute를 다시 확인하도록...
		///// </summary>
		//private static void RaiseCanExecuteChanged()
		//{
		//	CommandManager.InvalidateRequerySuggested();
		//}
		#endregion

		private void SetCanExecute(bool canExecute)
		{
			if (_canExecute != canExecute) {
				_canExecute = canExecute;
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

		public async void Execute(object parameter)
		{
			if (CanExecute(null) == false) { return; }
			if (_handler != null) {
				SetCanExecute(false);
				await _handler(parameter);
				SetCanExecute(true);
			}
		}
	}
}
