using System; // Пространство имен System, содержащее основные классы и структуры для работы с памятью, вводом-выводом, обработкой исключений и т.д.
using System.Collections.Generic; // Пространство имен для работы с обобщенными коллекциями, такими как списки, словари и стеки.
using System.Linq; // Пространство имен для работы с запросами LINQ, позволяющими выполнять операции над коллекциями данных.
using System.Threading.Tasks; // Пространство имен для работы с параллельным программированием и асинхронными операциями.
using System.Windows.Forms; // Пространство имен для работы с графическим интерфейсом пользователя.

namespace CourseWork // Пространство имен CourseWork
{
    static class Program // Статический класс Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        [STAThread] // Атрибут, указывающий, что поток должен быть однопоточным.
        static void Main()
        {
            Application.EnableVisualStyles(); // Включение визуальных стилей для приложения.
            Application.SetCompatibleTextRenderingDefault(false); // Установка совместимости отображения текста по умолчанию.
            Application.Run(new MainForm()); // Запуск главного окна приложения.
        }
    }
}