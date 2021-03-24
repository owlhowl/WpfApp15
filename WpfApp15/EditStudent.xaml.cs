using System.Windows.Controls;

namespace WpfApp15
{
    /// <summary>
    /// Interaction logic for EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Page, IPage
    {
        public EditStudent()
        {
            InitializeComponent();
        }

        public void SetVM(IPageVM vm)
        {
            DataContext = vm;
        }
    }
}
