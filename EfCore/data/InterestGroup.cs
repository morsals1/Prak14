using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class InterestGroup : ObservableObject
    {
        private int _id;
        private string _title;
        private string _description;

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

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private ObservableCollection<UserInterestGroup> _userInterestGroups;
        public ObservableCollection<UserInterestGroup> UserInterestGroups
        {
            get => _userInterestGroups;
            set => SetProperty(ref _userInterestGroups, value);
        }
    }
}
