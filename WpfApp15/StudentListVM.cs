using Mvvm1125;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WpfApp15
{
    internal class StudentListVM : MvvmNotify, IPageVM
    {
        Model model;
        private Group selectedGroup;

        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        public Group SelectedGroup
        {
            get => selectedGroup;
            set { selectedGroup = value; UpdateStudents(); }
        }
        public Student SelectedStudent { get; set ; }

        public MvvmCommand EditSelectedStudent { get; set; }
        public MvvmCommand RemoveSelectedStudent { get; set; }
        public MvvmCommand CreateStudent { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;
            UpdateGroups();

            EditSelectedStudent = new MvvmCommand(() => 
                { 
                    PageManager.ChangePageTo(PageType.EditStudent); 
                    model.EditStudent(SelectedStudent);
                },
                () => SelectedStudent != null);

            RemoveSelectedStudent = new MvvmCommand(
                () => model.RemoveStudent(SelectedGroup, SelectedStudent),
                () => SelectedStudent != null);

            CreateStudent = new MvvmCommand(
                () => 
                { 
                    model.EditStudent(model.CreateStudent(SelectedGroup)); 
                    PageManager.ChangePageTo(PageType.EditStudent); 
                },
                () => SelectedGroup != null);

            model.StudentsChanged += Model_StudentsChanged;
            model.GroupsChanged += Model_GroupsChanged;
            PageManager.CurrentPageChanged += PageManager_CurrentPageChanged;
        }

        private void PageManager_CurrentPageChanged(object sender, PageType e)
        {
            model.LoadGroups();
        }

        private void Model_GroupsChanged(object sender, EventArgs e)
        {
            UpdateGroups();
        }

        private void Model_StudentsChanged(object sender, EventArgs e)
        {
            UpdateStudents(); 
        }

        private void UpdateStudents()
        {
            if (SelectedGroup != null)
                Students = new ObservableCollection<Student>(SelectedGroup.Students);
            else
                Students = new ObservableCollection<Student>();
            //Students.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));
            NotifyPropertyChanged("Students");
        }

        private void UpdateGroups()
        {
            Groups = new ObservableCollection<Group>(model.Groups);
            NotifyPropertyChanged("Groups");
        }
    }
}