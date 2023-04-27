using _5NET.Model;
using _5NET.View;
using _5NET.View.Helpers;
using Prism.Commands;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Xml.Linq;

namespace _5NET.ViewModel
{
    internal class MainViewModel : BindingHelper
    {
        #region 
        public string Ip { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public DelegateCommand NP 
        {
            get 
            {
                return new DelegateCommand(() =>
                {
                    if (Validate.EnterValidate(Ip, Name))
                    {
                        MetodLogic.NewChat(new EnterModel(Ip, Name));
                        Window1 window1 = new Window1();
                        window1.ShowDialog();
                    }
                });
            } 
        }
        public DelegateCommand CP
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Validate.EnterValidate(Ip, Name))
                    {
                        MetodLogic.ConectChat(new EnterModel(Ip, Name));
                        Window1 window1 = new Window1();
                        window1.ShowDialog();
                    }
                });
            }
        }
        #endregion

        public MainViewModel()
        {
            Ip = "127.0.0.1:8888";
            Name = "noname";
            Height = 450;
            Width = 800;
        }
    }
}
