using SQLite;
using People.Models;

namespace People;

public class PersonRepository
{
    string _dbPath;
    private SQLiteConnection conn;  // Campo para la conexión a la base de datos

    public string StatusMessage { get; set; }

    // Inicialización de la base de datos
    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>(); // Crear la tabla para almacenar personas
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    // Insertar una nueva persona en la base de datos
    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init(); // Llamar a Init para asegurarse de que la base de datos esté inicializada

            // Validación básica para asegurar que se ingrese un nombre
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // Insertar la nueva persona en la base de datos
            result = conn.Insert(new Person { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }
    }

    // Obtener todas las personas de la base de datos
    public List<Person> GetAllPeople()
    {
        try
        {
            Init(); // Inicializar la base de datos

            // Recuperar una lista de objetos Person de la base de datos
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>(); // Retornar una lista vacía en caso de error
    }

    public void DeletePerson(int id)
    {
        try
        {
            Init();

            var personToDelete = conn.Find<Person>(id);
            if (personToDelete != null)
            {
                conn.Delete(personToDelete);
                StatusMessage = $"Registro eliminado: {personToDelete.Name}";
            }
            else
            {
                StatusMessage = "No se encontró el registro.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al eliminar el registro: {ex.Message}";
        }
    }
}


