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
            CurrentTeacher = db.Users.FirstOrDefault(t => t.Id == teacher.Id);
            ListSubs = CurrentTeacher.Subjects.ToList();
            TeacherGroups = CurrentTeacher.Teaching.TaughtGroups.ToList();
            ListGroups = (from gr in db.Groups
                          join sp in db.Specialities on gr.SpecialityId equals sp.Id
                          join f in db.Faculties on sp.FacultyId equals f.Id
                          join te in db.Teachings on teacher.Id equals te.Id
                          where f.UniversityId == teacherUniversity
                          select gr).ToList();
        }
        
        private TaughtGroups _SelectedTeacherGroup;
        public TaughtGroups SelectedTeacherGroup
        {
            get => _SelectedTeacherGroup;
            set
            {
                if (value != null)
                {
                    var tech = db.Users.Include("Teaching").FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                    var gr = db.TaughtGroups.FirstOrDefault(g => g.Id == value.Id);
                    new CustomMessageBox(gr.ToString() + " was deleted").Show();
                    db.TaughtGroups.Remove(gr);
                    db.SaveChanges();
                    tech = db.Users.FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                    CurrentTeacher = tech;
                    TeacherGroups = CurrentTeacher.Teaching.TaughtGroups.ToList();
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
                if(value != null && SelectedSub != null)
                {
                    var tech = db.Users.Include("Teaching").FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                    var teaching = db.Teachings.FirstOrDefault(t => t.Id == tech.Teaching.Id);
                    var gr = db.Groups.FirstOrDefault(g => g.Id == value.Id);
                    var sub = db.Subjects.FirstOrDefault(s => s.Id == SelectedSub.Id);
                    var taughtGroups = db.TaughtGroups.FirstOrDefault(gtg => (gtg.Group.Id == gr.Id && gtg.Subject.Id == sub.Id));
                    if (taughtGroups == null)
                    {
                        var tg = new TaughtGroups()
                        {
                            Subject = sub,
                            Group = gr,
                            Teaching = teaching
                        };
                        tech.Teaching.TaughtGroups.Add(tg);
                        db.SaveChanges();
                        tech = db.Users.FirstOrDefault(t => t.Id == CurrentTeacher.Id);
                        CurrentTeacher = tech;
                        TeacherGroups = tech.Teaching.TaughtGroups.ToList();
                    } else new CustomBoxes.CustomMessageBox(taughtGroups.ToString() + " already exist").Show();
                }
                _SelectedFromListGroups = value;
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

        private List<Subject> _ListSubs;
        public List<Subject> ListSubs
        {
            get => _ListSubs;
            set
            {
                _ListSubs = value;
                OnPropertyChanged("ListSubs");
            }
        }

        private Subject _SelectedSub;
        public Subject SelectedSub
        {
            get => _SelectedSub;
            set
            {
                _SelectedSub = value;
                OnPropertyChanged("SelectedSub");
            }
        }

        private List<TaughtGroups> _TeacherGroups;
        public List<TaughtGroups> TeacherGroups
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
