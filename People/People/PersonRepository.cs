using SQLite; // Necesario para la funcionalidad SQLite
using People.Models; // Necesario para el modelo `Person`
using System.Collections.Generic; // Para List<T>

namespace People;

public class PersonRepository
{
    private SQLiteConnection conn; // Variable privada para la conexión SQLite
    private readonly string _dbPath; // Ruta de la base de datos

    public string StatusMessage { get; set; } // Mensajes de estado para el usuario

    // Constructor que recibe la ruta de la base de datos
    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    // Método para inicializar la conexión y crear la tabla si no existe
    private void Init()
    {
        if (conn != null)
            return; // La conexión ya está inicializada, no hacer nada

        // Inicializa la conexión y crea la tabla
        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();
    }

    // Método para agregar una nueva persona
    public void AddNewPerson(string name)
    {
        int result = 0; // Número de filas afectadas
        try
        {
            // Inicializa la conexión
            Init();

            // Validación básica
            if (string.IsNullOrEmpty(name))
                throw new Exception("Debe ingresar un nombre válido.");

            // Inserta la nueva persona en la base de datos
            result = conn.Insert(new Person { Name = name });

            StatusMessage = $"{result} registro(s) agregado(s) (Nombre: {name})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al agregar {name}. Detalles: {ex.Message}";
        }
    }

    // Método para recuperar todas las personas
    public List<Person> GetAllPeople()
    {
        try
        {
            // Inicializa la conexión
            Init();

            // Recupera todas las filas de la tabla como una lista
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al recuperar datos. Detalles: {ex.Message}";
            return new List<Person>(); // Retorna una lista vacía en caso de error
        }
    }

    // Método para eliminar una persona por ID
    public void DeletePerson(int id)
    {
        try
        {
            // Inicializa la conexión
            Init();

            // Validación del ID
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            // Intenta eliminar el registro por ID
            int rowsAffected = conn.Delete<Person>(id);

            // Mensaje según el resultado
            if (rowsAffected == 0)
            {
                StatusMessage = $"No se encontró ningún registro con el ID {id} para eliminar.";
            }
            else
            {
                StatusMessage = $"Registro con ID {id} eliminado correctamente.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al eliminar el registro: {ex.Message}";
        }
    }
}
