﻿using _5NET.Model;
using _5NET.View;
using _5NET.View.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace _5NET.ViewModel
{
    internal class MainViewModel1 : BindingHelper
    {
        #region 
        public static int span { get; set; }
        public static ObservableCollection<string> MessageStory { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> Clients { get; set; }= new ObservableCollection<string>();
        public static ObservableCollection<string> Log {get; set; }=new ObservableCollection<string>();
        public string Message { get; set; }
        public DelegateCommand Send
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Message == "/disconnect")
                        Application.Current.Shutdown();
                    else if (Validate.MessageValidate(Message))
                        _ = TcpClient.SendMessage(Message);
                });
            }
        }

        #endregion
        public MainViewModel1()
        { 
            Message = "";
        }
    }
}
