using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Annotations;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _i = 2333;
        private string _a = "qwe";
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    I++;
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            });
        }

        public int I
        {
            get => _i + 100;
            set
            {
                if (value == _i) return;
                _i = value;
                OnPropertyChanged();
            }
        }

        public string A
        {
            get => _a;
            set => _a = value;
        }
    }

    internal class Test

    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Test()
        {
        }

        public static readonly DependencyProperty DataContextTypeProperty = DependencyProperty.RegisterAttached(
            "DataContextType", typeof(Type), typeof(Test),
            new PropertyMetadata(default(Type), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = (FrameworkElement) d;
            frameworkElement.DataContext = Activator.CreateInstance((Type) e.NewValue);
        }

        public static void SetDataContextType(DependencyObject element, Type value)
        {
            element.SetValue(DataContextTypeProperty, value);
        }

        public static Type GetDataContextType(DependencyObject element)
        {
            return (Type) element.GetValue(DataContextTypeProperty);
        }
    }
}