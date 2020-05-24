using Todo.Data;
using Todo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Todo
{
    public partial class App : Application
    {
        private static TodoItemDatabase database;

        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new TodoListPage())
            {
                BarBackgroundColor = (Color) Current.Resources["primaryGreen"], BarTextColor = Color.White
            };

            MainPage = nav;
        }

        public static TodoItemDatabase Database => database ?? (database = new TodoItemDatabase());

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

