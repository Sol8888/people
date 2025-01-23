using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using People.Models;

namespace People.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Person> People { get; set; } = new();
    public string NewName { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    public Command AddPersonCommand { get; }
    public Command<Person> DeletePersonCommand { get; }

    public MainViewModel()
    {
        AddPersonCommand = new Command(AddPerson);
        DeletePersonCommand = new Command<Person>(DeletePerson);

        LoadPeople();
    }

    private void AddPerson()
    {
        if (!string.IsNullOrWhiteSpace(NewName))
        {
            App.PersonRepo.AddNewPerson(NewName);
            People.Add(new Person { Name = NewName });
            NewName = string.Empty;
            OnPropertyChanged(nameof(NewName));
        }
    }

    private void DeletePerson(Person person)
    {
        if (person != null)
        {
            App.PersonRepo.DeletePerson(person.Id);
            People.Remove(person);

            // Obtener la página actual y mostrar la alerta
            var currentPage = Application.Current.Windows[0].Page as Page;
            currentPage?.DisplayAlert(
                "Registro Eliminado",
                "Soledad Cabrera acaba de eliminar un registro.",
                "OK"
            );
        }
    }


    private void LoadPeople()
    {
        People.Clear();
        var allPeople = App.PersonRepo.GetAllPeople();
        foreach (var person in allPeople)
        {
            People.Add(person);
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

