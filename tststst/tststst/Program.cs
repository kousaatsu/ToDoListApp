using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList
{
    class Program
    {
        static List<Task> tasks = new List<Task>();
        const string filePath = "tasks.txt";

        static void Main(string[] args)
        {
            LoadTasks();
            while (true)
            {
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Удалить задачу");
                Console.WriteLine("3. Просмотреть список задач");
                Console.WriteLine("4. Фильтрация задач по приоритету");
                Console.WriteLine("5. Поиск задач");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        RemoveTask();
                        break;
                    case "3":
                        ViewTasks();
                        break;
                    case "4":
                        FilterTasks();
                        break;
                    case "5":
                        SearchTasks();
                        break;
                    case "6":
                        SaveTasks();
                        return;
                    default:
                        Console.WriteLine("Введите нужный номер!");
                        break;
                }
            }
        }

        static void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 2)
                    {
                        tasks.Add(new Task(parts[0], (Priority)Enum.Parse(typeof(Priority), parts[1])));
                    }
                }
            }
        }

        static void SaveTasks()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Task task in tasks)
                {
                    writer.WriteLine($"{task.Description};{(int)task.TaskPriority}");
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Введите описание задачи.");
            string description = Console.ReadLine();
            Console.Write("Укажите приоритет: 1 - Высокий, 2 - Средний, 3 - Низкий.");
            string priorityInput = Console.ReadLine();
            Priority priority;

            switch (priorityInput)
            {
                case "1":
                    priority = Priority.High;
                    break;
                case "2":
                    priority = Priority.Medium;
                    break;
                case "3":
                    priority = Priority.Low;
                    break;
                default:
                    Console.WriteLine("Приоритет не распознан.");
                    return;
            }

            tasks.Add(new Task(description, priority));
            Console.WriteLine("Задача добавлена!");
        }

        static void RemoveTask()
        {
            Console.Write("Введите номер задачи для удаления.");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            {
                tasks.RemoveAt(index - 1);
                Console.WriteLine("Задача удалена.");
            }
            else
            {
                Console.WriteLine("Номер не распознан.");
            }
        }

        static void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Нет активных задач.");
                return;
            }

            Console.WriteLine("Список задач:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. [{tasks[i].TaskPriority}] {tasks[i].Description}");
            }
        }

        static void FilterTasks()
        {
            Console.Write("Введите приоритет для фильтрации: 1 - Высокий, 2 - Средний, 3 - Низкий.");
            if (int.TryParse(Console.ReadLine(), out int priorityInput))
            {
                Priority priority = (Priority)priorityInput;
                List<Task> filteredTasks = tasks.Where(t => (int)t.TaskPriority == priorityInput).ToList();

                if (filteredTasks.Count == 0)
                {
                    Console.WriteLine("Нет задач с таким приоритетом.");
                    return;
                }

                Console.WriteLine($"Задачи с приоритетом {priority}:");
                for (int i = 0; i < filteredTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. [{filteredTasks[i].TaskPriority}] {filteredTasks[i].Description}");
                }
            }
            else
            {
                Console.WriteLine("Приоритет не распознан.");
            }
        }

        static void SearchTasks()
        {
            Console.Write("Введите ключевое слово для поиска.");
            string keyword = Console.ReadLine().ToLower();
            List<Task> foundTasks = tasks.Where(t => t.Description.ToLower().Contains(keyword)).ToList();

            if (foundTasks.Count == 0)
            {
                Console.WriteLine("Нет таких задач.");
                return;
            }

            Console.WriteLine($"Найденные задачи с ключевым словом {keyword}:");
            for (int i = 0; i < foundTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. [{foundTasks[i].TaskPriority}] {foundTasks[i].Description}");
            }
        }
    }
}