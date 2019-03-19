using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Messaging;
using University_students.Messager;
using University_students.Models;
using GalaSoft.MvvmLight.CommandWpf;

namespace University_students.ViewModel
{
    public class AdminViewModel : ViewModelBase, INotifyPropertyChanged
    {
        USDbContext db;

        private Page UniversitiesPage;
        private Page FacultiesPage;
        private Page GroupsPage;
        private Page SubjectsPage;
        private Page TeachersPage;
        private Page StudentsPage;
        private Page SpecialitiesPage;

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        private Page _currentAdminPage;
        public Page CurrentAdminPage
        {
            get => _currentAdminPage;
            set
            {
                _currentAdminPage = value;
                OnPropertyChanged("CurrentAdminPage");
            }
        }


        public AdminViewModel()
        {
            UniversitiesPage = new View.AdminPages.Universities();
            FacultiesPage = new View.AdminPages.Faculties();
            GroupsPage = new View.AdminPages.Groups();
            SubjectsPage = new View.AdminPages.Subjects();
            TeachersPage = new View.AdminPages.Teachers();
            StudentsPage = new View.AdminPages.Students();
            SpecialitiesPage = new View.AdminPages.Specialities();
            CurrentAdminPage = UniversitiesPage;
            Messenger.Default.Register<SendCurrentUserMessage>(this, (action) => ReceiveMessage(action));
        }

        public string Login
        {
            get => _currentUser?.Login;
        }

        private object ReceiveMessage(SendCurrentUserMessage action)
        {
            CurrentUser = action.CurrentUser;
            return null;
        }

        public ICommand ChooseAdminPageCommand
        {
            get
            {
                return new RelayCommand<object>(
                    (param) => {
                        switch ((string)param)
                        {
                            case "university":
                                CanRedirectUniversities();
                                break;
                            case "faculty":
                                CanRedirectFaculties();
                                break;
                            case "group":
                                CanRedirectGroups();
                                break;
                            case "student":
                                CanRedirectStudents();
                                break;
                            case "teacher":
                                CanRedirectTeachers();
                                break;
                            case "subject":
                                CanRedirectSubjects();
                                break;
                            case "speciality":
                                CanRedirectSpecialities();
                                break;
                        }
                    }
                );
            }
        }

        private void CanRedirectUniversities()
        {
            CurrentAdminPage = UniversitiesPage;
        }

        private void CanRedirectFaculties()
        {
            CurrentAdminPage = FacultiesPage;
        }

        private void CanRedirectStudents()
        {
            CurrentAdminPage = StudentsPage;
        }

        private void CanRedirectGroups()
        {
            CurrentAdminPage = GroupsPage;
        }

        private void CanRedirectTeachers()
        {
            CurrentAdminPage = TeachersPage;
        }

        private void CanRedirectSubjects()
        {
            CurrentAdminPage = SubjectsPage;
        }

        private void CanRedirectSpecialities()
        {
            CurrentAdminPage = SpecialitiesPage;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
