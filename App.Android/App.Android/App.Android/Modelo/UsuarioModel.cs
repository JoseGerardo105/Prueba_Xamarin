using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using static Xamarin.Essentials.Permissions;

namespace App.Android.Modelo
{
    public class UsuarioModel:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value;
                OnPropertyChange();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChange();
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value;
                OnPropertyChange();
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value;
                OnPropertyChange();
            }
        }

        private string email;



        public string Email
        {
            get { return email; }
            set { email = value;
                OnPropertyChange();
            }
        }

        
    }
}
