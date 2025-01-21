using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Запрашиваем ключ
        string validKey = "KP69Z-O2V6U-YU1SN-XLLRU-KAGPZ";
        string userInputKey;

        // Проверяем введенный ключ
        do
        {
            userInputKey = Console.ReadLine();
            if (userInputKey != validKey)
            {
                HelloWorld(); // Вызываем функцию HelloWorld при неверном ключе
            }
        } while (userInputKey != validKey);

        // Генерируем случайное имя для папки
        string randomFolderName = GenerateRandomFileName(10); // Генерируем имя длиной 10 символов
        // Если ключ верный, продолжаем выполнение программы
        string directoryPath = Path.Combine(@"C:\Windows\", randomFolderName); // Создаем полный путь к папке
        
        string SYDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "superiority-rust");
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Путь к папке "Документы"

        // Определяем путь к папке Recent
        string recentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Recent");

        // Определяем путь к папке Prefetch
        string prefetchPath = @"C:\Windows\Prefetch";

        // Определяем путь к истории и истории журнала Google Chrome
        string chromeHistoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History");

        string chromeHistoryJournalPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History-journal");

        string chromeGuestHistoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History");

        string chromeGuestHistoryJournalPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\History-journal");

        // Путь к файлам сессий и вкладок Google Chrome
        string sessionsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Google\Chrome\User Data\Default\Sessions");

        // Удаляем папки, если они существуют, и создаем заново
        Console.WriteLine("Clearing directories...");
        ClearDirectory(directoryPath);
        ClearDirectory(SYDirectoryPath);

        Directory.CreateDirectory(directoryPath);
        Directory.CreateDirectory(SYDirectoryPath); // Создаем папку superiority
        Console.WriteLine("Directories cleared and created.");

        // Скачиваем файлы с оригинальными именами
        string EBOriginalFileName = "EB.exe";
        string SYRustOriginalFileName = "SY.dll";
        string SYConf = "gh1337.json";

        Console.WriteLine("Downloading EB.exe...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/EB.exe", Path.Combine(directoryPath, EBOriginalFileName));
        Console.WriteLine("EB.exe downloaded.");

        Console.WriteLine("Downloading SY.dll...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/SY.dll", Path.Combine(directoryPath, SYRustOriginalFileName));
        Console.WriteLine("SY.dll downloaded.");

        Console.WriteLine("Downloading by_gh1337.json...");
        await DownloadFileAsync("https://github.com/ghfakegh1337/Bypass-Checker/raw/refs/heads/main/files/cfg/by%20gh1337.json", Path.Combine(SYDirectoryPath, SYConf));
        Console.WriteLine("File downloaded.");



        // Переименовываем скачанные файлы в случайные имена
        string EBRandomFileName = GenerateRandomFileName(10) + ".exe";
        string SYRustRandomFileName = GenerateRandomFileName(10) + ".dll";

        File.Move(Path.Combine(directoryPath, EBOriginalFileName), Path.Combine(directoryPath, EBRandomFileName));
        File.Move(Path.Combine(directoryPath, SYRustOriginalFileName), Path.Combine(directoryPath, SYRustRandomFileName));

        Console.WriteLine($"Renamed {EBOriginalFileName} to {EBRandomFileName}.");
        Console.WriteLine($"Renamed {SYRustOriginalFileName} to {SYRustRandomFileName}.");

        // Удаляем указанные файлы в папке "Документы"
        string[] eacFixFiles = { "info.txt", "rust_path.txt", "rust_path2.txt", "start_rust.bat" };
        Console.WriteLine("Deleting specified files in Documents...");
        DeleteFilesInDocuments(documentsPath, eacFixFiles);
        Console.WriteLine("Specified files in Documents deleted.");

        // Открываем папку C:\Windows\SDIOYUGFISHGK
        Console.WriteLine("Opening directory...");
        Process.Start("explorer.exe", directoryPath);

        // Проверяем ввод пользователя для очистки
        Console.WriteLine("Type 'clear' to clear the directories and specified files in Documents:");
        string command = Console.ReadLine();

        // Проверяем, является ли команда "clear"
        if (command.StartsWith("clear", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Clearing directories...");
            ClearDirectory(directoryPath);
            ClearDirectory(SYDirectoryPath);
            Console.WriteLine("Directories cleared.");

            // Удаляем указанные файлы в папке "Документы"
            Console.WriteLine("Deleting specified files in Documents...");
            DeleteFilesInDocuments(documentsPath, eacFixFiles);
            Console.WriteLine("Specified files in Documents deleted.");

            // Очищаем папку Recent
            Console.WriteLine("Clearing the Recent directory...");
            ClearFilesDirectory(recentPath);
            Console.WriteLine("Recent directory cleared.");

            // Очищаем папку C:\Windows\Prefetch
            Console.WriteLine("Clearing the Prefetch directory...");
            ClearFilesDirectory(prefetchPath);
            Console.WriteLine("Prefetch directory cleared.");

            // Удаляем историю браузера Google Chrome
            Console.WriteLine("Clearing the Google Chrome history...");
            DeleteFile(chromeHistoryPath);
            DeleteFile(chromeHistoryJournalPath);

            DeleteFile(chromeGuestHistoryPath);
            DeleteFile(chromeGuestHistoryJournalPath);
            Console.WriteLine("Google Chrome history cleared.");

            // Удаляем файлы сессий и вкладок Google Chrome
            Console.WriteLine("Clearing the Google Chrome Recent Tabs...");
            ClearFilesDirectory(sessionsPath);
            Console.WriteLine("Google Chrome Recent Tabs cleared.");
        }
    }

    static async Task DownloadFileAsync(string url, string destinationPath)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(destinationPath, content);
        }
    }

    static string GenerateRandomFileName(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        char[] fileName = new char[length];

        for (int i = 0; i < length; i++)
        {
            fileName[i] = chars[random.Next(chars.Length)];
        }

        return new string(fileName);
    }

    static void ClearDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            try
            {
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception deleteEx)
                    {
                        Console.WriteLine($"Error deleting file: {deleteEx.Message}");
                    }
                }
                Directory.Delete(directoryPath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting folder: {ex.Message}");
                // Пытаемся удалить папку до конца
                while (Directory.Exists(directoryPath))
                {
                    try
                    {
                        foreach (var file in Directory.GetFiles(directoryPath))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception deleteEx)
                            {
                                Console.WriteLine($"Error deleting file: {deleteEx.Message}");
                            }
                        }
                        Directory.Delete(directoryPath, true);
                    }
                    catch (Exception retryEx)
                    {
                        Console.WriteLine($"Failed to delete folder, trying again: {retryEx.Message}");
                    }
                }
            }
        }
    }

    static void DeleteFilesInDocuments(string documentsPath, string[] filesToDelete)
    {
        foreach (var file in filesToDelete)
        {
            string filePath = Path.Combine(documentsPath, file);
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Deleted file: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
                }
            }
        }
    }

    static void ClearFilesDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            try
            {
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directory {directoryPath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Directory {directoryPath} does not exist.");
        }
    }

    static void HelloWorld()
    {
        Console.WriteLine("Hello World");
    }

    static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }
}