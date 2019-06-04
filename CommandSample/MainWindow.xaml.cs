using System.Windows;

namespace CommandSample {
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 만약 MainWindow의 ViewModel에 접근해야한다면 사용
		/// </summary>
		public MainWindowViewModel ViewModel {
			get { return this.DataContext as MainWindowViewModel; }
		}
	}
}
