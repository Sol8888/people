using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections.ObjectModel;
using System.Windows.Input;
using People.Models;
using People;

namespace People.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Person> People { get; set; }
        public string StatusMessage { get; set; }
        public ICommand AddPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }

        public MainViewModel()
        {
            People = new ObservableCollection<Person>();
            AddPersonCommand = new Command<string>(AddPerson);
            DeletePersonCommand = new Command<int>(DeletePerson);

            LoadPeople();
        }

        private void AddPerson(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                StatusMessage = "Debe ingresar un nombre válido.";
                return;
            }

            App.PersonRepo.AddNewPerson(name);
            StatusMessage = App.PersonRepo.StatusMessage;
            LoadPeople();
        }

        private void DeletePerson(int id)
        {
            var person = People.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                App.PersonRepo.DeletePerson(person.Id);
                StatusMessage = "Soledad Cabrera acaba de eliminar un registro.";
                People.Remove(person);
            }
        }

        private void LoadPeople()
        {
            var people = App.PersonRepo.GetAllPeople();
            People.Clear();
            foreach (var person in people)
            {
                People.Add(person);
            }
        }
    }
}
