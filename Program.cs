using Newtonsoft.Json;
using To_Do_List;

internal class Program
{
    public static string path = AppContext.BaseDirectory + "data.json";
    private static void Main(string[] args)
    {
        List<MyTask> taskManager = new List<MyTask>();
        if (File.Exists(path))
        {
            var data = JsonConvert.DeserializeObject<List<MyTask>>(File.ReadAllText(path)) ?? new List<MyTask>();
            taskManager = data;
        }

        UserInput(taskManager);
    }
    private static void UserInput(List<MyTask> taskManager)
    {
        Console.WriteLine(new string('-', 50));
        Console.WriteLine("A - добавить задачу\nD - удалить задачу\nS - показать все задачи\nС - отметить задачу выполненной\nQ - выйти из программы");
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.A:
                AddTask(taskManager);
                break;
            case ConsoleKey.D:
                DeleteTask(taskManager);
                break;
            case ConsoleKey.S:
                ShowAllTask(taskManager);
                break;
            case ConsoleKey.C:
                SetTaskCompleted(taskManager);
                break;
            case ConsoleKey.Q:
                Quit(taskManager);
                break;
        }
        UserInput(taskManager);
    }
    public static void ShowAllTask(List<MyTask> taskManager)
    {
        Console.Clear();
        Console.WriteLine("Ваши задачи:");
        for (int i = 0; i < taskManager.Count; i++)
        {
            if (taskManager[i].IsCompleted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(i + 1 + "." + taskManager[i]);
            Console.ResetColor();
        }
    }
    public static void AddTask(List<MyTask> taskManager)
    {
        Console.Clear();
        Console.Write("Введите название задачи: ");
        string name = "";
        while (true)
        {
            name = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Console.Clear();
                Console.WriteLine("Введите корректное название! Название задачи не должно быть пустой строкой!");
            }
            else
            {
                break;
            }
        }
        taskManager.Add(new MyTask(name));
        Console.WriteLine("Задача успешно добавлена!");
    }
    public static void DeleteTask(List<MyTask> taskManager)
    {
        Console.Clear();
        ShowAllTask(taskManager);
        Console.Write("Введите номер задачи, которую хотите удалить: ");

        int number = 0;
        while (number <= 0)
        {
            var success = int.TryParse(Console.ReadLine(), out int result);
            if (success && result > 0 && result <= taskManager.Count)
            {
                number = result;
                taskManager.RemoveAt(number - 1);
                ShowAllTask(taskManager);
                Console.WriteLine("Задача успешно удалена");
            }
            else
            {
                Console.WriteLine("Введите число, которое больше 0 и не больше количества задач!");
            }
        }
    }
    public static void SetTaskCompleted(List<MyTask> taskManager)
    {
        Console.Clear();
        ShowAllTask(taskManager);
        Console.Write("Введите номер задачи, которую хотите отметить выполненной: ");

        int number = 0;
        while (number <= 0)
        {
            var success = int.TryParse(Console.ReadLine(), out int result);
            if (success && result > 0 && result <= taskManager.Count)
            {
                number = result;
                taskManager[number - 1].IsCompleted = true;
                ShowAllTask(taskManager);
                Console.WriteLine("Задача успешно выполнена");
            }
            else
            {
                Console.WriteLine("Введите число, которое больше 0 и не больше количества задач!");
            }
        }
    }
    public static void Quit(List<MyTask> taskManager)
    {
        var data = JsonConvert.SerializeObject(taskManager);
        File.WriteAllText(path, data);
        Environment.Exit(Environment.ExitCode);
    }

}
