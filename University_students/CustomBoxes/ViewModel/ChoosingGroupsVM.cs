using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using University_students.Models;

namespace University_students.CustomBoxes.ViewModel
{
    class ChoosingGroupsVM : ViewModelBase, INotifyPropertyChanged
    {
        private USDbContext db;
        private User _currentTeacher;
        private int teacherUniversity;
        public User CurrentTeacher
        {
            get => _currentTeacher;
            set
            {
                _currentTeacher = value;
                OnPropertyChanged("SelectedTeacherDG"); 
            }
        }

        public ChoosingGroupsVM(User teacher)
        {
            db = new USDbContext();
            teacherUniversity = (int)teacher.Pulpit.Faculty.UniversityId;
            CurrentTeacher = teacher;
            TeacherGroups = teacher.Teaching.Groups.ToList();
            ListGroups = (from gr in db.Groups
                          join sp in db.Specialities on gr.SpecialityId equals sp.Id
                          join f in db.Faculties on sp.FacultyId equals f.Id
                          join te in db.Teachings on teacher.Id equals te.Id
                          where f.UniversityId == teacherUniversity && te.Groups.All(g => g.Id != gr.Id)
                          select gr).ToList();
        }
        
        private Group _SelectedTeacherGroup;
        public Group SelectedTeacherGroup
        {
            get => _SelectedTeacherGroup;
            set
            {
                if (value != null)
                {
                    var tech = db.Users.FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                    var gr = db.Groups.FirstOrDefault(g => g.Id == value.Id);
                    tech.Teaching.Groups.Remove(gr);
                    db.SaveChanges();
                    CurrentTeacher = tech;
                    TeacherGroups = CurrentTeacher.Teaching.Groups.ToList();
                    ListGroups.Add(value);
                    ListGroups = ListGroups.ToList();
                }
                _SelectedTeacherGroup = null;
                OnPropertyChanged("SelectedTeacherGroup");
            }
        }

        private Group _SelectedFromListGroups;
        public Group SelectedFromListGroups
        {
            get => _SelectedFromListGroups;
            set
            {
                if(value != null)
                {
                    var tech = db.Users.FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                    var gr = db.Groups.FirstOrDefault(g => g.Id == value.Id);
                    tech.Teaching.Groups.Add(value);
                    db.Groups.Attach(gr);
                    db.SaveChanges();
                    CurrentTeacher = tech;
                    TeacherGroups = tech.Teaching.Groups.ToList();
                    ListGroups.Remove(value);
                    ListGroups = ListGroups.ToList();
                }
                _SelectedFromListGroups = null;
                OnPropertyChanged("SelectedFromListGroups");
            }
        }

        private List<Group> _ListGroups;
        public List<Group> ListGroups
        {
            get => _ListGroups;
            set
            {
                _ListGroups = value;
                OnPropertyChanged("ListGroups");
            }
        }

        private List<Group> _TeacherGroups;
        public List<Group> TeacherGroups
        {
            get => _TeacherGroups;
            set
            {
                _TeacherGroups = value;
                OnPropertyChanged("TeacherGroups");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
