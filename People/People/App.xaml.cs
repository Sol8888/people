namespace People;

public partial class App : Application
{
    public static PersonRepository PersonRepo { get; private set; }

    public App(PersonRepository repo)
    {
        InitializeComponent();

        // Asignar el repositorio recibido al campo estático
        PersonRepo = repo;

        // Establecer la página principal
        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell());
    }
}
