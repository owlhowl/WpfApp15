using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using Mvvm1125;

namespace WpfApp15
{
    class MainVM : MvvmNotify
    {
        Model model;

        public Page CurrentPage { get; set; }

        public MvvmCommand OpenGroupList { get; set; }
        public MvvmCommand OpenStudentList { get; set; }

        public MainVM()
        {
            model = new Model();
            PageManager.SetModel(model);
            CurrentPage = PageManager.GetPageByType(PageType.StudentList);
            PageManager.CurrentPageChanged += PageManager_CurrentPageChanged;

            OpenGroupList = new MvvmCommand(() => PageManager.ChangePageTo(PageType.GroupList), () => true);
            OpenStudentList = new MvvmCommand(() => PageManager.ChangePageTo(PageType.StudentList), () => true);
        }

        void PageManager_CurrentPageChanged(object sender, PageType e)
        {
            CurrentPage = PageManager.GetPageByType(e);
            NotifyPropertyChanged("CurrentPage");
        }
    }
}
