using Mvvm1125;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace WpfApp15
{
    class GroupListVM : MvvmNotify, IPageVM
    {
        Model model;

        public ObservableCollection<Group> Groups { get; set; }
        public Group SelectedGroup { get; set; }
        public string CreateGroupName { get; set; }

        public MvvmCommand CreateGroup { get; set; }
        public MvvmCommand RemoveGroup { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;
            UpdateGroups();

            CreateGroup = new MvvmCommand(
                () => model.CreateGroup(CreateGroupName),
                () => !string.IsNullOrEmpty(CreateGroupName));

            RemoveGroup = new MvvmCommand(
                () => model.RemoveGroup(SelectedGroup),
                () => true);
            
            model.GroupsChanged += Model_GroupsChanged;
        }

        private void Model_GroupsChanged(object sender, EventArgs e)
        {
            UpdateGroups();
        }

        private void UpdateGroups()
        {
            Groups = new ObservableCollection<Group>(model.Groups);
            NotifyPropertyChanged("Groups");
        }
    }
}
