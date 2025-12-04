using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class Role : ObservableObject
    {
        private int _id;
        private string _title;
        private ObservableCollection<User> _students;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ObservableCollection<User> Users
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }
    }
}
