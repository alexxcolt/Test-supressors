using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public interface IBusinessContext
    {
        ObservableCollection<Device> GetDevices();
        ObservableCollection<Measurement> GetMeasuremets();
        ObservableCollection<User> GetUsers();
        double GetUmax(Device b);
        double GetUmin(Device b);
        double GetUnom(Device b);
        Device GetDeviceForAddNewDeviceAndParameter(Device b);
        void AddDeviceInDb(Device b);
        void SaveDb();
        User GetUserForSaveToUser(string ComboBoxSurnameInDialog);
        void AddUsersInDb(User user);
        void AddMeasurementsInDb(Measurement measure);
    }
}
