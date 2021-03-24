using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp15
{
    public class Model
    {
        GroupManager groupManager;

        public List<Group> Groups { get => groupManager.Groups; }
        
        public event EventHandler StudentsChanged;
        public event EventHandler GroupsChanged;
        public event EventHandler<Student> SelectedStudentChanged;

        internal void SaveStudent()
        {
            groupManager.SaveGroupList();
            StudentsChanged?.Invoke(this, null);
        }

        internal void NoSaveStudent()
        {
            groupManager.LoadGroupList();
            StudentsChanged?.Invoke(this, null);
        }

        public Model()
        {
            groupManager = new GroupManager();
        }

        internal bool CanSave(Student student)
        {
            return student != null && !(string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName));
        }

        internal void CreateGroup(string createGroupName)
        {
            if (Groups.Contains(Groups.FirstOrDefault(g => g.GroupName == createGroupName)))
                return;
            groupManager.CreateGroup(createGroupName);
            groupManager.LoadGroupList();
            GroupsChanged?.Invoke(this, null);
        }

        internal void RemoveGroup(Group selectedGroup)
        {
            groupManager.RemoveGroup(selectedGroup);
            GroupsChanged?.Invoke(this, null);
        }

        internal void EditStudent(Student selectedStudent)
        {
            SelectedStudentChanged?.Invoke(this, selectedStudent);
        }

        internal void RemoveStudent(Group selectedGroup, Student selectedStudent)
        {
            groupManager.RemoveStudent(selectedGroup, selectedStudent);
            StudentsChanged?.Invoke(this, null);
        }

        internal Student CreateStudent(Group selectedGroup)
        {
            return groupManager.CreateStudent(selectedGroup);
            //StudentsChanged?.Invoke(this, null);
        }

        internal void LoadGroups()
        {
            groupManager.LoadGroupList();
            GroupsChanged?.Invoke(this, null);
        }
    }
}