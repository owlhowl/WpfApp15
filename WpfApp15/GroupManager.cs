using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace WpfApp15
{
    class GroupManager
    {
        public List<Group> Groups { get; set; }

        public GroupManager()
        {
            LoadGroupList();
        }

        internal void RemoveStudent(Group selectedGroup, Student selectedStudent)
        {
            selectedGroup.Students.Remove(selectedStudent);
            SaveGroupList();
        }

        internal Student CreateStudent(Group selectedGroup)
        {
            Student newStudent = new Student();
            selectedGroup.Students.Add(newStudent);
            return newStudent;
        }

        internal void RemoveGroup(Group selectedGroup)
        {
            Groups.Remove(selectedGroup);
            SaveGroupList();
        }

        internal void CreateGroup(string createGroupName)
        {
            Groups.Add(new Group { GroupName = createGroupName });
            SaveGroupList();
        }

        const string path = "groups.db";

        public void SaveGroupList()
        {
            var json = JsonSerializer.Serialize(Groups, typeof(List<Group>));
            File.WriteAllText(path, json);
        }

        public void LoadGroupList()
        {
            string file = path;
            if (!File.Exists(file) || new FileInfo(file).Length == 0)
            {
                Groups = new List<Group>();
                return;
            }
            string json = File.ReadAllText(file);
            Groups = (List<Group>)JsonSerializer.Deserialize(json, typeof(List<Group>));
            Groups.Sort((s1, s2) => s1.GroupName.CompareTo(s2.GroupName));
        }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
